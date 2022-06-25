using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons.EOCDrops
{
    [Label("$Mods.VanillaPlus.ItemName.Tear")]
    public class TearConfig : WeaponConfig
    {
        protected override int DefaultDamage => 9;

        protected override int DefaultUseTime => 25;
    }
}
