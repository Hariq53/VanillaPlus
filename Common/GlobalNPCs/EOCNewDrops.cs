using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Common.GlobalNPCs
{
    class EOCNewDrops : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().EOCDropsToggle;
        }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.EyeofCthulhu;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1,
                ModContent.ItemType<Tear>(),
                ModContent.ItemType<FangOfCthulhu>(),
                ModContent.ItemType<TheOcularMenace>(),
                ModContent.ItemType<EyeballOnAStick>()
                ));
            npcLoot.Add(notExpertRule);
        }

        class EOCNewDropsBossBag : GlobalItem
        {
            public override void OpenVanillaBag(string context, Player player, int arg)
            {
                if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag)
                {
                    int ItemType = Main.rand.Next(4) switch
                    {
                        0 => ModContent.ItemType<Tear>(),
                        1 => ModContent.ItemType<FangOfCthulhu>(),
                        2 => ModContent.ItemType<TheOcularMenace>(),
                        _ => ModContent.ItemType<EyeballOnAStick>(),
                    };
                    player.QuickSpawnItem(ItemType);
                }
            }
        }
    }
}
