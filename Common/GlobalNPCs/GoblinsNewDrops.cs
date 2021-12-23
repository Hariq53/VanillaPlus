using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Common.GlobalNPCs
{
    internal class GoblinsNewDrops : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().GoblinDropsToggle;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            switch (npc.type)
            {
                case NPCID.GoblinWarrior:
                    npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WarriorsMallet>(), 100, 40));
                    break;
                case NPCID.GoblinThief:
                    npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ThiefsDagger>(), 100, 40));
                    break;
                case NPCID.GoblinPeon:
                    npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<GoblinsBlade>(), 100, 40));
                    break;
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}
