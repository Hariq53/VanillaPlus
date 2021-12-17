using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace VanillaPlus.Content.Buffs
{
    class CthulhusMalediction : ModBuff
    {
        public static List<int> affectedNpcs = new(Main.maxNPCs);

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.pvpBuff[Type] = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (!affectedNpcs.Contains(npc.whoAmI))
                affectedNpcs.Add(npc.whoAmI);
        }
    }

    class CthulhusMaledictionGlobalNPC : GlobalNPC
    {
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (CthulhusMalediction.affectedNpcs.Contains(npc.whoAmI))
                if (npc.HasBuff(ModContent.BuffType<CthulhusMalediction>()))
                {
                    npc.color = new Color(100, 10, 10, 100);
                }
            base.DrawEffects(npc, ref drawColor);
        }

        public override bool PreAI(NPC npc)
        {
            if (CthulhusMalediction.affectedNpcs.Contains(npc.whoAmI))
            {
                if (npc.HasBuff(ModContent.BuffType<CthulhusMalediction>()))
                    npc.defense -= 5;
                else
                    CthulhusMalediction.affectedNpcs.Remove(npc.whoAmI);
            }
            return base.PreAI(npc);
        }

        public override void ResetEffects(NPC npc)
        {
            if (CthulhusMalediction.affectedNpcs.Contains(npc.whoAmI))
            {
                npc.color = new();
                npc.defense += 5;
            }
            base.ResetEffects(npc);
        }
    }
}
