using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Common.GlobalItems
{
    class Gladius : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().GladiusTweakToggle;
        }

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Gladius)
            {
                item.autoReuse = true;
                item.reuseDelay = 0;
            }
            base.SetDefaults(item);
        }
    }
}
