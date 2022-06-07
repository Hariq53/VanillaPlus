using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Content.Changes
{
    class FlyingDragon : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().FlyingDragonTweakToggle;
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.DD2SquireBetsySword;
        }

        public override void SetDefaults(Item item)
        {
            item.damage = 200;
        }
    }
}
