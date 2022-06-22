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
        public override bool NeedsReload(ModConfig pendingConfig)
        {
            if (pendingConfig is VanillaPlusServerConfig otherConfig)
            {
                PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object? thisValue = property.GetValue(this);
                    object? otherValue = property.GetValue(otherConfig);
                    if (!(thisValue)?.Equals(otherValue) ?? true)
                        return true;
                }
            }
            return base.NeedsReload(pendingConfig);
        }

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.VanillaPlus.Config.ItemsHeader")]

        [Label("$Mods.VanillaPlus.Config.GlobalConfig.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GlobalConfig.Tooltip")]
        [ReloadRequired]
        public GlobalItemsConfig GlobalItems { get => _globalItems; set => _globalItems.Assign(value); }
        readonly GlobalItemsConfig _globalItems = new();

        [ReloadRequired]
        public ItemsConfig Items { get; } = new();

        public bool TestBool = false;

        static void AssignSuperConfig(Func<ItemConfig?> superConfigHandler, IEnumerable<ItemConfig> subConfigs)
        {
            foreach (ItemConfig itemConfig in subConfigs)
                if (itemConfig.SuperConfigHandler is null)
                    itemConfig.SuperConfigHandler = superConfigHandler;
        }

        static void AssignSuperConfig(Func<ItemConfig?> superConfigHandler, params ItemConfig[] subConfigs)
        {
            AssignSuperConfig(superConfigHandler, subConfigs.AsEnumerable());
        }

        public VanillaPlusServerConfig()
            : base()
        {
            AssignSuperConfig(() => GlobalItems, Items.TestItem);
        }
    }
}
