using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons.EvilMaces
{
    [Label("$Mods.VanillaPlus.ItemName.RottenMace")]
    public class RottenMaceConfig : WeaponConfig
    {
        protected override int DefaultDamage => 30;

        protected override int DefaultUseTime => 45;
    }
}
