using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace VanillaPlus.Content.Buffs
{
    class Aquaflame : ModBuff
    {
        public static List<int> affectedNpcs = new(Main.maxNPCs);

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.pvpBuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (!affectedNpcs.Contains(npc.whoAmI))
                affectedNpcs.Add(npc.whoAmI);
        }
    }

	class AquaFlameGlobalNPC : GlobalNPC
    {
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Aquaflame.affectedNpcs.Contains(npc.whoAmI))
            {
                if (npc.HasBuff(ModContent.BuffType<Aquaflame>()))
                {
                    if (Main.rand.Next(4) < 3)
                    {
                        Dust dust13 = Dust.NewDustDirect(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, DustID.Flare_Blue, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Scale: 3.5f);
                        dust13.noGravity = true;
                        dust13.velocity *= 1.8f;
                        dust13.velocity.Y -= 0.5f;
                        if (Main.rand.Next(4) == 0)
                        {
                            dust13.noGravity = false;
                            dust13.scale *= 0.5f;
                        }
                    }
                    Lighting.AddLight((int)(npc.position.X / 16f), (int)(npc.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
                }
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (Aquaflame.affectedNpcs.Contains(npc.whoAmI))
            {
                if (npc.HasBuff(ModContent.BuffType<Aquaflame>()))
                {
                    if (npc.lifeRegen > 0)
                        npc.lifeRegen = 0;

                    npc.lifeRegen -= 8;
                }
                else
                    Aquaflame.affectedNpcs.Remove(npc.whoAmI);
            }
        }
    }
}
