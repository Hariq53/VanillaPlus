using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using HeadState = VanillaPlus.Content.NPCs.Bosses.SnakeBoss.SnakeBossHead.ActionState;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossBody : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

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
            Main.npcFrameCount[Type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.width = 70;
            NPC.height = 210;
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

        public enum ActionState
        {
            Idle,
            BiteAttack,
            Jump
        }

        private enum Frame
        {
            Phase1Idle,
            Phase1Jump
        }

        public bool SecondStage
        {
            get => NPC.ai[0] == 1f;
            set => NPC.ai[0] = value ? 1f : -1f;
        }

        public ActionState AIState
        {
            get => (ActionState)NPC.ai[1];
            set => NPC.ai[1] = (float)value;
        }

        int AttackTimer
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        int HeadID
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        SnakeBossHead Head
        {
            get
            {
                NPC npc = Main.npc[HeadID];
                if (npc.active &&
                    npc.type == ModContent.NPCType<SnakeBossHead>() &&
                    npc.ModNPC is SnakeBossHead head)
                    return head;
                else
                    return null;
            }
        }

        bool SetupFlag
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : -1f;
        }

        bool JumpAnimFlag
        {
            get => NPC.localAI[1] == 1f;
            set => NPC.localAI[1] = value ? 1f : -1f;
        }

        public Vector2 HeadPosition
        {
            get
            {
                Vector2 headPosition = NPC.Center;
                headPosition -= new Vector2(7f * NPC.direction, 93f);
                if (JumpAnimFlag)
                    headPosition.Y -= 8f;
                return headPosition;
            }
        }

        Player Target
        {
            get => Main.player[NPC.target];
        }

        private void Setup()
        {
            if (NPC.target < 0 || NPC.target == 255 || Target.dead || !Target.active)
                NPC.TargetClosest();
            AIState = ActionState.Idle;
            Point HeadSpawnPoint = HeadPosition.ToPoint();
            HeadID = NPC.NewNPC(NPC.GetBossSpawnSource(NPC.target), HeadSpawnPoint.X, HeadSpawnPoint.Y, ModContent.NPCType<SnakeBossHead>(), NPC.whoAmI, NPC.whoAmI);
        }

        public override bool PreAI()
        {
            if (!SetupFlag)
            {
                Setup();
                SetupFlag = true;
            }
            return true;
        }

        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Target.dead || !Target.active)
                NPC.TargetClosest();

            AIState = DetermineState(AIState);

            switch (AIState)
            {
                case ActionState.BiteAttack:
                    StationaryAI();
                    break;
                case ActionState.Jump:
                    float minJumpHeight = 10f, maxJumpHeight = 15f, maxSideSpeed = 5f;
                    JumpAI(minJumpHeight, maxJumpHeight, maxSideSpeed);
                    break;
                case ActionState.Idle:
                    // TO DO: add minion spawn
                    StationaryAI();
                    break;
                default:
                    StationaryAI();
                    break;
            }
        }

        ActionState DetermineState(ActionState previousState)
        {
            if (previousState == ActionState.Jump)
                if (!NPC.collideY)
                    return ActionState.Jump;

            int maxAttackDuration = 30;

            if (previousState == ActionState.BiteAttack)
            {
                if (AttackTimer++ > maxAttackDuration || Head.AIState != HeadState.Attack)
                {
                    AttackTimer = 0;
                    return ActionState.Idle;
                }
                return ActionState.BiteAttack;
            }

            Vector2 vectorToTarget = Target.Center - NPC.Center;
            float xDistance = MathF.Abs(vectorToTarget.X);

            float attackRange = 400f;
            int attackFrequency = 60;

            if (xDistance > attackRange)
                return ActionState.Jump;

            // Charge attack even when out of range
            if (AttackTimer <= attackFrequency)
                AttackTimer++;

            if (IsInBiteAttackRange(Target.Center))
            {
                // if attack charged
                if (AttackTimer > attackFrequency)
                {
                    SetupAttack();
                    return ActionState.BiteAttack;
                }
            }
            else
            {
                if (IsInDirectHitRange(Target.Hitbox))
                    return ActionState.Idle;

                return ActionState.Jump;
            }

            return ActionState.Idle;
        }

        void SetupAttack()
        {
            AttackTimer = 0;
            Head.StartAttack();
        }

        void StationaryAI()
        {
            if (NPC.collideY)
                NPC.velocity.X = 0f;
            TurnTowardsPlayer();
        }

        void JumpAI(float minJumpHeight, float maxJumpHeight, float maxSideSpeed)
        {
            Vector2 vectorToTarget = Target.Center - NPC.Center;
            if (NPC.collideY)
            {
                NPC.velocity.X = 0f;
                #region Jump Code
                // Jumps as high as the minimum jump height plus the distance from the top of the NPC to the player center (capped at maxJumpHeight)
                float velocityY = -minJumpHeight + MathHelper.Clamp(NPC.height / 2 + vectorToTarget.Y, -(maxJumpHeight - minJumpHeight), 0f);

                // Side movement speed relative to distance from target
                float velocityX = MathHelper.Clamp(vectorToTarget.X * 0.01f, -maxSideSpeed, maxSideSpeed);

                NPC.velocity = new(velocityX, velocityY);
                #endregion
            }
            TurnTowardsPlayer();
        }

        private bool IsInBiteAttackRange(Vector2 targetCenter)
        {
            float rotationToTarget = (HeadPosition - targetCenter).ToRotation();

            // Check for angle: the max angle for a bite attack is 35 degrees up and 45 degrees down
            if (NPC.direction == 1)
            {
                if (rotationToTarget < -0.8f || rotationToTarget > 0.6f)
                    return false;
            }
            else
            {
                if (rotationToTarget > 0f)
                {
                    if (rotationToTarget < 2.5f)
                        return false;
                }
                else if (rotationToTarget > -2.4f)
                    return false;
            }

            return true;
        }

        bool IsInDirectHitRange(Rectangle targetHitbox)
        {
            if (NPC.Hitbox.Intersects(targetHitbox))
                return true;

            if (IsInXRange(targetHitbox))
                if (NPC.Center.Y < targetHitbox.Y)
                    return true;

            return false;
        }

        bool IsInXRange(Rectangle targetHitbox)
        {
            if (NPC.Hitbox.BottomLeft().X > targetHitbox.BottomLeft().X && NPC.Hitbox.BottomLeft().X < targetHitbox.BottomRight().X)
                return true;

            if (NPC.Hitbox.BottomRight().X > targetHitbox.BottomLeft().X && NPC.Hitbox.BottomRight().X < targetHitbox.BottomRight().X)
                return true;

            if (NPC.Hitbox.Center.X > targetHitbox.BottomLeft().X && NPC.Hitbox.Center.X < targetHitbox.BottomRight().X)
                return true;

            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            int frameY = 0;
            JumpAnimFlag = AIState == ActionState.Jump && !NPC.collideY && NPC.velocity.Y < 0;
            if (JumpAnimFlag)
                frameY = frameHeight * (int)Frame.Phase1Jump;
            NPC.frame.Y = frameY;
        }

        private void TurnTowardsPlayer()
        {
            NPC.direction = NPC.spriteDirection = (NPC.position.X - Target.position.X > 0 ? 1 : -1);
        }
    }
}
