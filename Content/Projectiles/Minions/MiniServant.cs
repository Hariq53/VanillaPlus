using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Models.ModProjectiles;
using VanillaPlus.Content.Buffs;

namespace VanillaPlus.Content.Projectiles.Minions
{
    class MiniServant : Minion
    {
        public override bool IsLoadingEnabled(Mod mod) { return true; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Type] = 6;
        }

        protected override int MinionBuff => ModContent.BuffType<MiniServantMinion>();

        public override void SetDefaults()
        {
            base.SetDefaults();

            // GFX and animation
            Projectile.frame = 0;

            // Hitbox and collision
            Projectile.width = Projectile.height = 18;
            Projectile.tileCollide = true;
            Projectile.shouldFallThrough = true;
            Projectile.ignoreWater = true;

            // Damage
            Projectile.idStaticNPCHitCooldown = 24;

            // Other
            Projectile.netImportant = true;
            Projectile.usesIDStaticNPCImmunity = true;
        }

        protected override Vector2 GetIdlePosition(Player owner)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= 48;
            return idlePosition;
        }

        bool berserk = false;

        public override bool PreAI()
        {
            if (berserk)
                Projectile.idStaticNPCHitCooldown = 12;
            else
                Projectile.idStaticNPCHitCooldown = 24;
            return true;
        }

        protected override void GeneralBehavior(Player owner, out Vector2 idlePosition)
        {
            base.GeneralBehavior(owner, out idlePosition);
            // Teleport to player if distance is too big
            Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            // If your minion is flying, you want to do this independently of any conditions
            float overlapVelocity = 0.04f;

            // Fix overlap with other minions
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X)
                        Projectile.velocity.X -= overlapVelocity;
                    else
                        Projectile.velocity.X += overlapVelocity;

                    if (Projectile.position.Y < other.position.Y)
                        Projectile.velocity.Y -= overlapVelocity;
                    else
                        Projectile.velocity.Y += overlapVelocity;
                }
            }

            if (owner.statLife <= owner.statLifeMax * 0.25f)
            {
                berserk = true;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.netUpdate = true;
            }
            else if (berserk)
            {
                berserk = true;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.netUpdate = true;
            }
        }

        protected override void SearchForTargets(Player owner, out bool foundTarget, out NPC target)
        {
            // Starting search distance
            float distanceFromTarget = MaxSearchDistance;
            Vector2 targetCenter = Projectile.position;
            target = new();
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    target = npc;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            target = npc;
                            foundTarget = true;
                        }
                    }
                }
            }

            Projectile.friendly = foundTarget;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                Projectile.velocity.X = -oldVelocity.X;

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                Projectile.velocity.Y = -oldVelocity.Y;

            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            // if the projectile is not directly inside a tile
            bool isInAir = !Collision.SolidCollision(Projectile.TopLeft, Projectile.width, Projectile.height);
            Projectile.tileCollide = isInAir;
            fallThrough = true;
            return isInAir;
        }

        protected override void IdleBehaviour(Vector2 idlePosition)
        {
            float speed = 8f * (berserk ? 2f : 1f);
            float inertia = 20f * (berserk ? 0.5f : 1f);

            float distanceFromIdlePos = Vector2.Distance(Projectile.Center, idlePosition);
            Vector2 vectorToIdlePos = idlePosition - Projectile.Center;

            // Minion doesn't have a target: return to player and idle
            if (distanceFromIdlePos > 600f)
            {
                // Speed up the minion if it's away from the player
                speed *= 1.5f;
                inertia *= 3f;
                Projectile.tileCollide = false;
            }
            else
            {
                // Slow down the minion if closer to the player
                speed *= 0.5f;
                inertia *= 4f;
                Projectile.tileCollide = true;
            }

            if (distanceFromIdlePos > 20f)
            {
                // The immediate range around the player (when it passively floats about)

                // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                vectorToIdlePos.Normalize();
                vectorToIdlePos *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePos) / inertia;
            }
            else if (Projectile.velocity == Vector2.Zero)
            {
                // If there is a case where it's not moving at all, give it a little "poke"
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }
        }

        protected override void AttackBehaviour(NPC target)
        {
            float speed = 8f * (berserk ? 2f : 1f);
            float inertia = 20f * (berserk ? 0.5f : 1f);

            float distanceFromTarget = Vector2.Distance(Projectile.Center, target.Center);

            if (distanceFromTarget > 40f)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
            }
            else
            {
                if (Projectile.velocity.X + Projectile.velocity.Y < 2f)
                    Projectile.velocity *= 1.05f;
            }
        }

        protected override void Visuals()
        {
            ProjectilesUtilities.FaceForwardHorizontalSprite(Projectile);

            int animationLength = 5;

            if (Projectile.frameCounter++ < animationLength)
                return;

            Projectile.frameCounter = 0;
            Projectile.frame++;

            if (berserk)
            {
                if (Projectile.frame < 3) Projectile.frame += 3;

                if (Projectile.frame > 5) Projectile.frame = 3;
            }
            else
            {
                if (Projectile.frame > 2) Projectile.frame -= 3;

                if (Projectile.frame > 2) Projectile.frame = 0;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(berserk);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            berserk = reader.ReadBoolean();
        }
    }
}
