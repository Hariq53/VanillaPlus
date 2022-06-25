using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Global
{
    public class GlobalItemsConfig : ItemConfig
    {
        [Label("$Mods.VanillaPlus.Config.ItemConfig.SoftDisableAll.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.ItemConfig.SoftDisableAll.Tooltip")]
        [ReloadRequired]
        public override bool IsSoftDisabled { get => base.IsSoftDisabled; set => base.IsSoftDisabled = value; }

        [Label("$Mods.VanillaPlus.Config.ItemConfig.HardDisableAll.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.ItemConfig.HardDisableAll.Tooltip")]
        [ReloadRequired]
        public override bool IsHardDisabled { get => base.IsHardDisabled; set => base.IsHardDisabled = value; }
    }
}
