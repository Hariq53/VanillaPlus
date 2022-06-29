using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Common.NPCLoots
{
    internal class GoblinsNewDrops : GlobalNPC
    {
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.type == NPCID.GoblinWarrior ||
                   entity.type == NPCID.GoblinThief ||
                   entity.type == NPCID.GoblinPeon;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            switch (npc.type)
            {
                case NPCID.GoblinWarrior:
                    if (VanillaPlus.ServerSideConfig?.Items.WarriorsMallet?.IsEnabled() ?? false)
                        npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WarriorsMallet>(), 100, 40));
                    break;
                case NPCID.GoblinThief:
                    if (VanillaPlus.ServerSideConfig?.Items.ThiefsDagger?.IsEnabled() ?? false)
                        npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ThiefsDagger>(), 100, 40));
                    break;
                case NPCID.GoblinPeon:
                    if (VanillaPlus.ServerSideConfig?.Items.GoblinsBlade?.IsEnabled() ?? false)
                        npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<GoblinsBlade>(), 100, 40));
                    break;
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}
