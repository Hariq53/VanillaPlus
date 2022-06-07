using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Common.NPCLoots
{
    class SkeletronNewDrops : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().SkeletronDropsToggle;
        }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.SkeletronHead;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            var entries = npcLoot.Get(false);
            foreach (var entry in entries)
            {
                if (entry is ItemDropWithConditionRule drop)
                {
                    if (drop.itemId == ItemID.SkeletronMask)
                    {
                        npcLoot.Remove(entry);
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.SkeletronMask, 7)).OnFailedRoll(ItemDropRule.Common(ItemID.SkeletronHand, 7));

                        IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
                        notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1,
                            ModContent.ItemType<SkeletronsFinger>(),
                            ModContent.ItemType<SkullOfBoom>(),
                            ModContent.ItemType<OlMansCurse>(),
                            ItemID.BookofSkulls
                            ));
                        npcLoot.Add(notExpertRule);
                    }
                }
            }
        }

        class SkeletronNewDropsBossBag : GlobalItem
        {
            public override bool PreOpenVanillaBag(string context, Player player, int arg)
            {
                if (context == "bossBag" && arg == ItemID.SkeletronBossBag)
                {
                    var source = player.GetSource_OpenItem(ItemID.SkeletronBossBag);

                    if (Main.tenthAnniversaryWorld)
                        player.TryGettingDevArmor(source);

                    player.QuickSpawnItem(source, 3245);
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            player.QuickSpawnItem(source, 1281);
                            break;
                        default:
                            player.QuickSpawnItem(source, 1273);
                            break;
                    }
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            player.QuickSpawnItem(source, ModContent.ItemType<SkeletronsFinger>());
                            break;
                        case 1:
                            player.QuickSpawnItem(source, ModContent.ItemType<SkullOfBoom>());
                            break;
                        case 2:
                            player.QuickSpawnItem(source, ModContent.ItemType<OlMansCurse>());
                            break;
                        default:
                            player.QuickSpawnItem(source, ItemID.BookofSkulls);
                            break;
                    }
                    return false;
                }

                return base.PreOpenVanillaBag(context, player, arg);
            }
        }
    }
}
