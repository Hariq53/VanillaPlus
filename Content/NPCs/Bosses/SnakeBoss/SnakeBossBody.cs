using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetDefaults()
        {
            NPC.width = 84;
            NPC.height = 186;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 5f; // Take up open spawn slots, preventing random NPCs from spawning during the fight

            NPC.aiStyle = -1;

            // The following code assigns a music track to the boss in a simple way.
            if (!Main.dedServ)
                Music = MusicID.Boss1;
        }

        internal enum ActionState
        {
            Jump,
            HeadAttack,
            Fleeing
        }

        public ref float AI_State => ref NPC.ai[0];

        bool HeadSpawned
        {
            get => NPC.ai[1] == 1f;
            set => NPC.ai[1] = value ? 1f : 0f;
        }

        int JumpTimer
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        public override void AI()
        {
            if (!HeadSpawned)
            {
                Point headPoint = NPC.Center.ToPoint();
                int headWhoAmI = NPC.NewNPC(headPoint.X, headPoint.Y, ModContent.NPCType<SnakeBossHead>(), NPC.whoAmI, NPC.whoAmI);
                Main.npc[headWhoAmI].ai[0] = Main.npc[headWhoAmI].realLife = NPC.whoAmI;
                HeadSpawned = true;
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                NPC.TargetClosest();

            Player player = Main.player[NPC.target];
            Vector2 vectorToTarget = player.Center - NPC.Center;
            float minJumpHeight = 10f, maxJumpHeight = 15f, maxSideSpeed = 5f;

            if (player.dead)
            {
                AI_State = (float)ActionState.Fleeing;
                minJumpHeight = 20f;
                maxJumpHeight = 30f;
                maxSideSpeed = 5f;
                vectorToTarget = -NPC.Center;

                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
            }

            switch (AI_State)
            {
                case (float)ActionState.Jump:
                case (float)ActionState.Fleeing:
                    {
                        JumpAI(vectorToTarget, minJumpHeight, maxJumpHeight, maxSideSpeed);
                        break;
                    }
                case (float)ActionState.HeadAttack:
                    {
                        if (NPC.collideY)
                            NPC.velocity = Vector2.Zero;
                        break;
                    }
            }

            SetDirectionTowardsTarget(vectorToTarget);
        }

        void SetDirectionTowardsTarget(Vector2 vectorToTarget)
        {
            NPC.direction = (vectorToTarget.X > 0f ? 1 : -1);
            NPC.spriteDirection = NPC.direction;
        }

        void JumpAI(Vector2 vectorToTarget, float minJumpHeight, float maxJumpHeight, float maxSideSpeed)
        {
            if (NPC.collideY)
            {
                JumpTimer++;
                NPC.velocity.X = 0f;
            }

            if (JumpTimer > 40f)
            {
                #region Jump Code
                if (vectorToTarget.X * NPC.direction > NPC.width / 2 + 100f)
                {
                    // Jumps as high as the minimum jump height plus the distance from the top of the NPC to the player center (capped at maxJumpHeight)
                    float velocityY = -minJumpHeight + MathHelper.Clamp(NPC.height / 2 + vectorToTarget.Y, -(maxJumpHeight-minJumpHeight), 0f);

                    // Side movement speed relative to distance from target
                    float velocityX = MathHelper.Clamp(vectorToTarget.X * 0.01f, -maxSideSpeed, maxSideSpeed);

                    NPC.velocity = new(velocityX, velocityY);
                }
                #endregion

                JumpTimer = 0;
            }
        }
    }
}
