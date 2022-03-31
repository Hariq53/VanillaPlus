using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossNeck : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.npcSlots = 5f;
            NPC.width = 40;
            NPC.height = 22;
            NPC.aiStyle = -1;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.netAlways = true;
            NPC.dontCountMe = true;
        }

        int BodyID
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
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

        ref float AttackDirection => ref NPC.ai[1];

        int SectionNumber
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        bool SetupFlag
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : -1f;
        }

        private bool Setup()
        {
            if (Body == null)
            {
                NPCsUtilities.Kill(NPC.whoAmI);
                return false;
            }
            NPC.realLife = BodyID;
            NPC.direction = Body.NPC.direction;
            NPC.rotation = AttackDirection;
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
            if (Body == null)
                return;

            if (NPC.direction == 1)
                NPC.Left = Body.HeadPosition;
            else
                NPC.Right = Body.HeadPosition;

            NPC.position += OffsetPosition(NPC.direction) + NPC.rotation.ToRotationVector2() * (SectionNumber + (NPC.direction == 1 ? 0f : 1f)) * NPC.width;
        }

        Vector2 OffsetPosition(int direction)
        {
            Vector2 pointToRotate = (direction == 1f ? NPC.Left : NPC.Right);
            Vector2 realPos = pointToRotate.RotatedBy(NPC.rotation, NPC.Center);

            Vector2 offset = pointToRotate - realPos;

            return offset;
        }
    }
}
