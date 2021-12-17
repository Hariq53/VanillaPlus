using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Changes
{
    class FlyingDragon : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().FlyingDragonTweakToggle;
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.DD2SquireBetsySword)
                item.damage = 200;
            base.SetDefaults(item);
        }
    }
}
