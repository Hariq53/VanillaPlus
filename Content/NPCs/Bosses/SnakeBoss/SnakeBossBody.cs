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
            NPC.width = 90;
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

        public enum ActionState
        {
            Idle,
            BiteAttack,
            Jump
        }

        public Vector2 HeadPosition
        {
            get
            {
                Vector2 headPosition = NPC.Center;
                headPosition -= new Vector2(7f * -NPC.direction, 96f);
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

            NPC.direction = (NPC.position.X - Target.position.X > 0 ? -1 : 1);
            NPC.spriteDirection = NPC.direction;
        }

        ActionState DetermineState(ActionState previousState)
        {
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
            float xDistance = MathF.Abs(vectorToTarget.X), yDistance = MathF.Abs(vectorToTarget.Y);

            float attackRange = 400f;
            int attackFrequency = 60;

            if (xDistance > attackRange)
                return ActionState.Jump;
            else if (AttackTimer++ > attackFrequency)
            {
                SetupAttack();
                return ActionState.BiteAttack;
            }

            return ActionState.Idle;
        }

        void SetupAttack()
        {
            AttackTimer = 0;
            Head.StartAttack();
        }
    }
}
