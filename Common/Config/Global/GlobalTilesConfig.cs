using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Global
{
    public class GlobalTilesConfig : ItemConfig
    {
        [Label("$Mods.VanillaPlus.Config.TileConfig.SoftDisableAll.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.TileConfig.SoftDisableAll.Tooltip")]
        [ReloadRequired]
        public override bool IsSoftDisabled { get => base.IsSoftDisabled; set => base.IsSoftDisabled = value; }

        [Label("$Mods.VanillaPlus.Config.TileConfig.HardDisableAll.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.TileConfig.HardDisableAll.Tooltip")]
        [ReloadRequired]
        public override bool IsHardDisabled { get => base.IsHardDisabled; set => base.IsHardDisabled = value; }
    }
}
