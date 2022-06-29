using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config.Items.Weapons;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Common.NPCLoots
{
    class EOCNewDrops : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.EyeofCthulhu;
        }

        static int[]? EnabledDrops
        {
            get
            {
                EOCDropsConfig? config = VanillaPlus.ServerSideConfig?.Items.EOCDrops;
                List<int> itemsToAdd = new();

                if (config is not null)
                {
                    if (config.Tear?.IsEnabled() ?? false)
                        itemsToAdd.Add(ModContent.ItemType<Tear>());

                    if (config.FangOfCthulhu?.IsEnabled() ?? false)
                        itemsToAdd.Add(ModContent.ItemType<FangOfCthulhu>());

                    if (config.TheOcularMenace?.IsEnabled() ?? false)
                        itemsToAdd.Add(ModContent.ItemType<TheOcularMenace>());

                    if (config.EyeballOnAStick?.IsEnabled() ?? false)
                        itemsToAdd.Add(ModContent.ItemType<EyeballOnAStick>());

                }
                else
                {
                    itemsToAdd = new List<int>()
                    {
                        ModContent.ItemType<Tear>(),
                        ModContent.ItemType<FangOfCthulhu>(),
                        ModContent.ItemType<TheOcularMenace>(),
                        ModContent.ItemType<EyeballOnAStick>()
                    };
                }

                if (itemsToAdd.Count == 0)
                    return null;

                return itemsToAdd.ToArray();
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            int[]? enabledDrops = EnabledDrops;

            if (enabledDrops is null)
                return;

            IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, enabledDrops));
            npcLoot.Add(notExpertRule);
        }

        class EOCNewDropsBossBag : GlobalItem
        {
            public override void OpenVanillaBag(string context, Player player, int arg)
            {
                if (context == "bossBag" && arg == ItemID.EyeOfCthulhuBossBag)
                {
                    int[]? enabledDrops = EnabledDrops;

                    if (enabledDrops is null)
                        return;

                    int ItemType = enabledDrops[Main.rand.Next(enabledDrops.Length)];

                    player.QuickSpawnItem(player.GetSource_OpenItem(ItemID.EyeOfCthulhuBossBag), ItemType);
                }
            }
        }
    }
}
