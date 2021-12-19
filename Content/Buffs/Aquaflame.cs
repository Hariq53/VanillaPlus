using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

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

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AquaFlamePlayer>().onFire4 = true;
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
                    Lighting.AddLight((int)(npc.position.X / 16f), (int)(npc.position.Y / 16f + 1f), 0.5f, 0.9f, 1f);
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

    public class AquaFlamePlayer : ModPlayer
    {
        public bool onFire4;

        public override void ResetEffects()
        {
            onFire4 = false;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (onFire4)
            {
                if (Main.rand.Next(4) < 3)
                {
                    Dust dust13 = Dust.NewDustDirect(new Vector2(Player.position.X - 2f, Player.position.Y - 2f), Player.width + 4, Player.height + 4, DustID.Flare_Blue, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, Scale: 3.5f);
                    dust13.noGravity = true;
                    dust13.velocity *= 1.8f;
                    dust13.velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        dust13.noGravity = false;
                        dust13.scale *= 0.5f;
                    }
                }
                Lighting.AddLight((int)(Player.position.X / 16f), (int)(Player.position.Y / 16f + 1f), 0.5f, 0.9f, 1f);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (onFire4)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                // Player.lifeRegenTime uses to increase the speed at which the player reaches its maximum natural life regeneration
                // So we set it to 0, and while this debuff is active, it never reaches it
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second
                Player.lifeRegen -= 8;
            }
        }
    }
}
