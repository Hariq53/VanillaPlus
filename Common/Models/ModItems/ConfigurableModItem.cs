using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Models.ModItems
{
    public abstract class ConfigurableModItem : ModItem
    {
        /// <summary>
        /// The config instance this item should pull data from
        /// </summary>
        protected virtual ItemConfig? Config => new(hardDisabled: true);

        /// <summary>
        /// This is where you define the logic that is used in <see cref="IsLoadingEnabled"/>, do
        /// not override unless you know what you're doing
        /// </summary>
        /// <returns>Whether or not this item should be loaded</returns>
        public virtual bool ShouldLoad()
        {
            // If all items are hard disabled, do not load (for when the Config is null)
            

            // If there is no config, the item should be loaded
            if (Config is null)
                return true;

            // If there is a config and the item is hard disabled, the item shouldn't be loaded
            return !Config.IsHardDisabled;
        }

        public virtual bool ShouldAddRecipes()
        {
            // If there is no config, recipes should be added
            if (Config is null)
                return true;

            // If there is a config and the item is soft disabled, recipes shouldn't be added
            return !Config.IsSoftDisabled;
        }
        
        public override sealed bool IsLoadingEnabled(Mod mod)
        {
            return ShouldLoad();
        }

        public override sealed void SetDefaults()
        {
            SetRegularDefaults();
            if (Config is not null)
                SetConfigurableDefaults(Config);
        }

        /// <summary>
        /// This is where you set all your item's default properties, this method gets called
        /// regardless of config, before <see cref="SetConfigurableDefaults" />
        /// </summary>
        protected virtual void SetRegularDefaults() { }

        /// <summary>
        /// This is where you set all your item's properties that depend on the config,
        /// this method gets called only if <c>Config</c> is not null, override only if you want to
        /// add special cases and always call the <c>base</c> method if you don't wish to undo all
        /// the regular config
        /// </summary>
        /// <param name="config">See <see cref="Config" /></param>
        protected virtual void SetConfigurableDefaults(ItemConfig config) { }

        /// <summary>
        /// Do not override unless you want to add recipes regardless of Config, in every other
        /// case, use <see cref="AddRecipesWithConfig"/>
        /// </summary>
        public override sealed void AddRecipes()
        {
            if (!ShouldAddRecipes())
                return;
            AddRecipesWithConfig();
        }

        /// <summary>
        /// This is where you add all your item's recipes,
        /// this method gets called only if <see cref="ShouldAddRecipes" /> returns true
        /// </summary>
        protected virtual void AddRecipesWithConfig() { }
    }
}
