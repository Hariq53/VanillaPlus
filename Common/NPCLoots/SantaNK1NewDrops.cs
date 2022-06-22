using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Common.NPCLoots
{
    class SantaNK1NewDrops : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true; //.ChristmasBarrageToggle;
        }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.SantaNK1;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            var entries = npcLoot.Get(false);
            foreach (var entry in entries)
            {
                if (entry is LeadingConditionRule leadingRule)
                {
                    foreach (var chainedRule in entry.ChainedRules)
                    {
                        if (chainedRule.RuleToChain is OneFromOptionsDropRule oneFromOptionsRule)
                        {
                            var tempList = oneFromOptionsRule.dropIds.ToList();
                            int ItemTypeToAdd = ModContent.ItemType<ChristmasBarrage>();
                            if (tempList.Contains(ItemID.EldMelter) && tempList.Contains(ItemID.ChainGun) && !tempList.Contains(ItemTypeToAdd))
                                tempList.Add(ItemTypeToAdd);
                            oneFromOptionsRule.dropIds = tempList.ToArray();
                            break;
                        }
                    }
                }
            }
        }
    }
}
