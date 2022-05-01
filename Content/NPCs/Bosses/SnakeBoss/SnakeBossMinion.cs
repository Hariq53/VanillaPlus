using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossMinion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 6;
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 34;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.netAlways = true;
            NPC.dontCountMe = true;

            NPC.aiStyle = -1;
        }

        public enum ActionState
        {
            Slither,
            Climb
        }

        public int BodyID
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public ActionState AIState
        {
            get => (ActionState)NPC.ai[1];
            set => NPC.ai[1] = (float)value;
        }

        SnakeBossBody Body
        {
            get
            {
                NPC npc = Main.npc[BodyID];
                if (npc.active &&
                    npc.type == ModContent.NPCType<SnakeBossBody>() &&
                    npc.ModNPC is SnakeBossBody body)
                    return body;
                else
                {
                    NPCsUtilities.Kill(NPC.whoAmI);
                    return null;
                }
            }
        }

        bool SetupFlag
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : -1f;
        }

        Player Target
        {
            get => Main.player[NPC.target];
        }

        private bool Setup()
        {
            return true;
            if (Body == null)
            {
                NPCsUtilities.Kill(NPC.whoAmI);
                return false;
            }
            if (NPC.target < 0 || NPC.target == 255 || Target.dead || !Target.active)
                NPC.TargetClosest();
            return true;
        }

        public override bool PreAI()
        {
            if (!SetupFlag)
            {
                SetupFlag = Setup();
                return SetupFlag;
            }
            return true;
        }

        public override void AI()
        {
            if (!NPC.HasValidTarget)
                NPC.TargetClosest();

            AIState = DetermineState(AIState);

            switch (AIState)
            {
                case ActionState.Slither:
                    SlitherAI();
                    break;
                case ActionState.Climb:
                    break;
                default:
                    break;
            }
        }

        private void SlitherAI()
        {
            NPC.direction = NPC.spriteDirection = (NPC.position.X - Target.position.X > 0 ? 1 : -1);
            float speed = 2f;
            if (NPC.Hitbox.Intersects(Target.Hitbox))
                return;
            else
            {
                NPC.velocity.X = speed * -NPC.direction;
                if (NPC.collideX)
                    NPC.velocity.Y = -2f;
            }
        }

        ActionState DetermineState(ActionState previousState)
        {
            return ActionState.Slither;
        }
    }
}
