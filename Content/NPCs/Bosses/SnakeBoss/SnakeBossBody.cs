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
			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
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
			NPC.width = 76;
			NPC.height = 184;
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

		int JumpTimer
		{
			get => (int)NPC.ai[0];
			set => NPC.ai[0] = value;
		}

        public override void AI()
        {
			if (NPC.ai[1] == 0f)
			{
				Main.NewText("Spawning head");
				Point headPoint = NPC.Center.ToPoint();
                NPC.NewNPC(headPoint.X, headPoint.Y, ModContent.NPCType<SnakeBossHead>(), NPC.whoAmI, NPC.whoAmI);
				NPC.ai[1] = 1f;
			}

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                NPC.TargetClosest();

            Player player = Main.player[NPC.target];
			Vector2 vectorToTarget = player.Center - NPC.Center;

			if (player.dead)
			{
				// If the targeted player is dead, flee
				if (JumpTimer++ > 40f)
				{
					JumpAI(new(), 20f, 0f, 4f);
					JumpTimer = 0;
				}

				// This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
				NPC.EncourageDespawn(10);
                return;
            }

			NPC.direction = (vectorToTarget.X > 0f ? 1 : -1);
			NPC.spriteDirection = NPC.direction;

			if (NPC.collideY)
            {
				JumpTimer++;
				NPC.velocity.X = 0f;
			}

			if (JumpTimer > 40f)
			{
				if (vectorToTarget.X * NPC.direction > NPC.width / 2 + 100f)
					JumpAI(player.Center, 10f, 5f, 5f, vectorToTarget);
				JumpTimer = 0;
			}
		}

		/// <summary>
		/// Modifies the NPC velocity to make it shoot upwards, moving towards the desired target, it jumps with a minimum height, adding height depending on the target Y position
		/// </summary>
		/// <param name="targetCenter">The target's center</param>
		/// <param name="minJumpHeight">The minimum height the NPC will jump</param>
		/// <param name="maxJumpHeight">The max height the NPC will jump</param>
		/// <param name="maxSideSpeed">The max X distance the NPC will cover with a jump</param>
		/// <param name="vectorToTarget">If not given, it will be calculated</param>
		void JumpAI(Vector2 targetCenter, float minJumpHeight, float maxJumpHeight, float maxSideSpeed, Vector2 vectorToTarget = new())
		{
			if (vectorToTarget == Vector2.Zero)
				vectorToTarget = targetCenter - NPC.Center;
			// Jumps as high as the minimum jump height plus the distance from the top of the NPC to the player cetner (capped at maxJumpHeight)
			float velocityY = -minJumpHeight + MathHelper.Clamp(NPC.height / 2 + vectorToTarget.Y, -maxJumpHeight, 0f);

			// Side movement speed relative to distance from target
			float velocityX = MathHelper.Clamp(vectorToTarget.X * 0.01f, -maxSideSpeed, maxSideSpeed);

			NPC.velocity = new(velocityX, velocityY);
		}
    }
}
