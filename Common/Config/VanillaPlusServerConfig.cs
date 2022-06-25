using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
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
            VanillaPlusServerConfig newConfig = (pendingConfig as VanillaPlusServerConfig)!;

            if (!Equals(Items, newConfig.Items))
                return true;

            return base.NeedsReload(pendingConfig);
        }

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.VanillaPlus.Config.ItemsHeader")]

        [Label("$Mods.VanillaPlus.Config.GlobalConfig.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GlobalConfig.Tooltip")]
        [ReloadRequired]
        public GlobalItemsConfig GlobalItems
        {
            get
            {
                foreach (Func<ItemConfig?> getItem in Items.AllSubs)
                    AssignSuperConfig(_globalItems, getItem());
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

		static void AssignSuperConfig(ItemConfig? superConfig, IEnumerable<ItemConfig?> subConfigs)
        {
            foreach (ItemConfig? itemConfig in subConfigs)
                if (itemConfig is not null)
                    itemConfig.SuperConfig ??= superConfig;
        }

        static void AssignSuperConfig(ItemConfig? superConfig, params ItemConfig?[] subConfigs)
        {
            if (subConfigs.Length == 1)
            {
                ItemConfig? subConfig = subConfigs[0];
                if (subConfig is not null)
                    subConfig.SuperConfig ??= superConfig;
            }
            AssignSuperConfig(superConfig, subConfigs.AsEnumerable());
        }
    }
}
