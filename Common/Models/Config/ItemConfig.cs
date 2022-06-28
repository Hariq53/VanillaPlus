using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    [Label("$Mods.VanillaPlus.Config.ItemConfig.Label")]
    public class ItemConfig : SHConfig
    {
        [Label("$Mods.VanillaPlus.Config.ItemConfig.SoftDisable.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.ItemConfig.SoftDisable.Tooltip")]
        public override bool IsSoftDisabled { get => base.IsSoftDisabled; set => base.IsSoftDisabled = value; }


        [Label("$Mods.VanillaPlus.Config.ItemConfig.HardDisable.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.ItemConfig.HardDisable.Tooltip")]
        public override bool IsHardDisabled { get => base.IsHardDisabled; set => base.IsHardDisabled = value; }

        public ItemConfig()
            : base()
        { }
        
        public ItemConfig(bool softDisabled = false, bool hardDisabled = false)
            : base(softDisabled, hardDisabled)
        { }
    }
}
