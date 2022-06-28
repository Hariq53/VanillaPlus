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
        [DefaultValue(15)]
        public override int Damage { get => base.Damage; set => base.Damage = value; }

        [DefaultValue(11)]
        public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
    }
}
