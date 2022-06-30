﻿using System.ComponentModel;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons.EOCDrops
{
    [Label("$Mods.VanillaPlus.ItemName.GoblinsBlade")]
    public class GoblinsBladeConfig : WeaponConfig
    {
        [DefaultValue(25)]
        public override int Damage { get => base.Damage; set => base.Damage = value; }

        [DefaultValue(26)]
        public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
    }
}
