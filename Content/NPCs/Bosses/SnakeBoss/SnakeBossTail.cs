using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossTail : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.npcSlots = 5f;
            NPC.width = 38;
            NPC.height = 30;
            NPC.aiStyle = -1;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.netAlways = true;
            NPC.dontCountMe = true;
            NPC.alpha = 255;
        }
    }
}
