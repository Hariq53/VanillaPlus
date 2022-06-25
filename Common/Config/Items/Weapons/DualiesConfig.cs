using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.ItemName.Dualies")]
    public class DualiesConfig : WeaponConfig
    {
        protected override int DefaultDamage => 15;

        protected override int DefaultUseTime => 11;
    }
}
