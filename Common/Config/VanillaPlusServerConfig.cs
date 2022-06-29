using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Config.Global;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config
{
    [Label("$Mods.VanillaPlus.Config.ServerSideConfig")]
    public class VanillaPlusServerConfig : ModConfig
    {
        public static void ElementConfigSetter<T>(ref T config, T newValue) where T : ElementConfig?
        {
            ElementConfig? superConfig = config?.SuperConfig;
            config = newValue;
            if (config is null)
                return;
            config.SuperConfig = superConfig;
        }

        public override bool NeedsReload(ModConfig pendingConfig)
        {
            VanillaPlusServerConfig other = (pendingConfig as VanillaPlusServerConfig)!;

            if (!Equals(Items, other.Items))
                return true;

            if (!Equals(Tiles, other.Tiles))
                return true;

            if (!Equals(GameplayTweaks, other.GameplayTweaks))
                return true;

            return base.NeedsReload(pendingConfig);
        }

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.VanillaPlus.Config.Items.Header")]

        [Label("$Mods.VanillaPlus.Config.GlobalConfig.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GlobalConfig.Tooltip")]
        [ReloadRequired]
        public GlobalItemsConfig GlobalItems
        {
            get
            {
                AssignSuperConfig(_globalItems, Items.AllItems);
                return _globalItems;
            }

            set => _globalItems = value;
        }
        GlobalItemsConfig _globalItems = new();

        public ItemsConfig Items
        {
            get;
            set;
        } = new();

        [Header("$Mods.VanillaPlus.Config.Tiles.Header")]

        [Label("$Mods.VanillaPlus.Config.GlobalConfig.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GlobalConfig.Tooltip")]
        [ReloadRequired]
        public GlobalTilesConfig GlobalTiles
        {
            get
            {
                AssignSuperConfig(_globalTiles, Tiles.AllTiles);
                return _globalTiles;
            }

            set => _globalTiles = value;
        }
        GlobalTilesConfig _globalTiles = new();

        public TilesConfig Tiles
        {
            get;
            set;
        } = new();

        [Header("$Mods.VanillaPlus.Config.GameplayTweaks.Header")]

        [Label("$Mods.VanillaPlus.Config.GlobalConfig.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GlobalConfig.Tooltip")]
        public GlobalGameplayTweaksConfig GlobalGameplayTweak
        {
            get
            {
                AssignSuperConfig(_globalGameplayTweak, GameplayTweaks.AllTweaks);
                return _globalGameplayTweak;
            }

            set => _globalGameplayTweak = value;
        }
        GlobalGameplayTweaksConfig _globalGameplayTweak = new();

        public GameplayTweaksConfig GameplayTweaks
        {
            get;
            set;
        } = new();

        static void AssignSuperConfig(ElementConfig? superConfig, IEnumerable<ElementConfig?> subConfigs)
        {
            foreach (ElementConfig? config in subConfigs)
                if (config is not null)
                    config.SuperConfig ??= superConfig;
        }

        static void AssignSuperConfig(ElementConfig? superConfig, params ElementConfig?[] subConfigs)
        {
            if (subConfigs.Length == 1)
            {
                ElementConfig? subConfig = subConfigs[0];
                if (subConfig is not null)
                    subConfig.SuperConfig ??= superConfig;
            }
            AssignSuperConfig(superConfig, subConfigs.AsEnumerable());
        }
    }
}
