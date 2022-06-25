using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons.EOCDrops
{
    [Label("$Mods.VanillaPlus.ItemName.EyeballOnAStick")]
    public class EyeballOnAStickConfig : WeaponConfig
    {
        protected override int DefaultDamage => 10;

        protected override int DefaultUseTime => 36;
    }
}
