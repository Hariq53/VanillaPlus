using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Models.ModProjectiles;
using VanillaPlus.Content.Buffs;

namespace VanillaPlus.Content.Projectiles.Minions
{
    class CursedSkullMinion : Minion
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 3;
        }

        protected override int MinionBuff => ModContent.BuffType<CursedSkullMinionBuff>();

        public override void SetDefaults()
        {
            base.SetDefaults();

            // GFX and animation
            Projectile.frame = 0;

            // Hitbox and collision
            Projectile.width = 26;
            Projectile.height = 28;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            // Damage
            Projectile.damage = 0;

            // Other
            Projectile.netImportant = true;
        }

        protected override void GeneralBehavior(Player owner, out Vector2 idlePosition)
        {
            base.GeneralBehavior(owner, out idlePosition);

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

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner)
                {
                    if (MathF.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
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
            }

            if (shadowflamesDamage == 0)
                shadowflamesDamage = Projectile.damage;

            Projectile.damage = 0;
        }

        int shadowflamesDamage = 0;

        protected override Vector2 GetIdlePosition(Player owner)
        {
            int index = 0;
            int totalIndexesInGroup = 0;
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.owner == Projectile.owner && projectile.type == Projectile.type && (projectile.type != 759 || projectile.frame == Main.projFrames[projectile.type] - 1))
                {
                    if (Projectile.whoAmI > i)
                        index++;

                    totalIndexesInGroup++;
                }
            }

            radius = 20f + totalIndexesInGroup * 5f;
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= (48 + radius);

            idlePosition.X += MathF.Cos(MathHelper.TwoPi / totalIndexesInGroup * index + CursedSkullTimer.angle) * radius;
            idlePosition.Y += MathF.Sin(MathHelper.TwoPi / totalIndexesInGroup * index + CursedSkullTimer.angle) * radius;
            return idlePosition;
        }

        float radius = 0f;

        protected override void IdleBehaviour(Vector2 idlePosition)
        {
            ShootTimer = 0;

            Vector2 vectorToIdlePos = idlePosition - Projectile.Center;
            float distanceFromIdlePos = vectorToIdlePos.Length();

            float speed = 4f, inertia = 20f;

            if (distanceFromIdlePos < 20f)
                inertia = 1f;

            if (distanceFromIdlePos < 5f)
                isSpinning = true;

            if (isSpinning)
                Projectile.velocity = vectorToIdlePos;
            else
            {
                if (distanceFromIdlePos > 600f)
                {
                    // Speed up the minion if it's away from the player
                    speed *= 2f;
                    inertia *= 2f;
                }

                vectorToIdlePos.Normalize();
                vectorToIdlePos *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePos) / inertia;
            }
        }

        public bool movingUpOverlap = false;

        public int ShootTimer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public bool isSpinning
        {
            get => Projectile.ai[1] == 1f;
            set => Projectile.ai[1] = value ? 1f : 0f;
        }

        protected override void AttackBehaviour(NPC target)
        {
            isSpinning = false;

            Vector2 directionToTarget = Vector2.Normalize(target.Center - Projectile.Center);
            float distanceFromTarget = Vector2.Distance(target.Center, Projectile.Center);

            float speed = 4f, inertia = 20f, slowDownSpeed = 3f;

            ShootTimer++;
            if (ShootTimer >= 60f + 10f * Projectile.minionPos)
            {
                int projectileIndex = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, directionToTarget * speed, ProjectileID.Shadowflames, shadowflamesDamage, Projectile.knockBack, Main.myPlayer);
                Main.projectile[projectileIndex].hostile = false;
                Main.projectile[projectileIndex].friendly = true;
                ShootTimer = 0;
            }

            if (distanceFromTarget > 100f + speed)
            {
                directionToTarget *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + directionToTarget) / inertia;
            }
            else if (distanceFromTarget >= 100f)
            {
                Projectile.velocity = new Vector2(directionToTarget.X, 0) * (distanceFromTarget - 100f);
            }
            else
            {
                Rectangle rectangle = new(Projectile.Hitbox.X, target.Hitbox.Y, Projectile.Hitbox.Width, Projectile.Hitbox.Height);

                if (target.Hitbox.Intersects(rectangle))
                    Projectile.velocity.X = slowDownSpeed * Projectile.direction;
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

                        if (((closest && inRange) || !foundTarget))
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

        protected override void Visuals()
        {
            Player owner = Main.player[Projectile.owner];
            Vector2 idlePosition = GetIdlePosition(owner);
            float distanceFromIdlePos = Projectile.Center.Distance(idlePosition);
            Vector2 vectorToIdlePos = idlePosition - Projectile.Center;

            if (Target.active && FoundTarget)
            {
                Vector2 vectorToTarget = Target.Center - Projectile.Center;

                if (vectorToTarget.X > 0f)
                {
                    Projectile.spriteDirection = -1;
                    Projectile.rotation = MathF.Atan2(vectorToTarget.Y, vectorToTarget.X);
                }

                if (vectorToTarget.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                    Projectile.rotation = MathF.Atan2(vectorToTarget.Y, vectorToTarget.X) + MathHelper.Pi;
                }
            }
            else if (!isSpinning)
            {
                Projectile.spriteDirection = -1;
                if (distanceFromIdlePos > 5f)
                    Projectile.rotation = Projectile.velocity.ToRotation() + (vectorToIdlePos.X > 0f ? 0f : MathHelper.Pi);
            }
            else
            {
                Projectile.localAI[0] += 0.05f;
                if (Projectile.localAI[0] <= 1f)
                    Projectile.rotation = Projectile.rotation.AngleLerp(0f, Projectile.localAI[0]);
                else
                    Projectile.rotation = 0f;
                Projectile.spriteDirection = -1;
            }

            int frame = Projectile.frame + 1;
            int frameSpeed = 3;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                if (frame < Main.projFrames[Projectile.type])
                    Projectile.frame = frame;
                else
                    Projectile.frame = 0;
            }

            Lighting.AddLight(Projectile.Center, Color.Yellow.ToVector3() * 0.78f);
        }
    }

    class CursedSkullTimer : ModSystem
    {
        public static float angle = 0f;

        public override void PreUpdateProjectiles() { angle += 0.017f; }
    }
}
