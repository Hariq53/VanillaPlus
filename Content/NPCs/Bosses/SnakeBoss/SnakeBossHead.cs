using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossHead : ModNPC
    {
		public override void SetDefaults()
        {
			NPC.npcSlots = 5f;
			NPC.width = 56;
			NPC.height = 32;
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
		}

        public override void AI()
        {
			NPC owner = Main.npc[(int)NPC.ai[0]];
			int ownerDirection = owner.direction;
			Vector2 offset = new(-25f * -ownerDirection, -76f);
			NPC.Center = owner.Center + offset;
			NPC.direction = ownerDirection;
			NPC.spriteDirection = NPC.direction;
		}
    }
}
