using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Models.ModItems
{
    abstract class ConfigurableItem : ModItem
    {
        protected virtual ItemConfig Config => new(hardDisabled: true);


        public bool ShouldLoad()
        {
            // If all items are disabled, do not load (used when the Config is null)
            if (ItemConfig.ForceDisableAllItems)
                return false;

            // If there is a config and the item is disabled, do not load
            return Config is null || !Config.IsHardDisabled;
        }

        public bool ShouldAddRecipes()
        {
            // If there is no config or the item is disabled, do not add recipes
            return Config is null || !Config.IsSoftDisabled;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShouldLoad();
        }

        public override void SetDefaults()
        {
            if (Config is not null)
                SetConfigurableDefaults();
        }

        protected virtual void SetConfigurableDefaults() { }

        public override void AddRecipes()
        {
            if (!ShouldAddRecipes())
                return;
        }
    }
}
