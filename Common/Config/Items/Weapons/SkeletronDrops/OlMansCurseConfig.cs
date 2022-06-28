using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons.EOCDrops
{
    [Label("$Mods.VanillaPlus.ItemName.OlMansCurse")]
    public class OlMansCurseConfig : WeaponConfig
    {
        [DefaultValue(22)]
        public override int Damage { get => base.Damage; set => base.Damage = value; }

        [DefaultValue(36)]
        public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
    }
}
