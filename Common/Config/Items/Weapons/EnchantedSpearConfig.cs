using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.ItemName.EnchantedSpear")]
    public class EnchantedSpearConfig : WeaponConfig
    {
        protected override int DefaultDamage => 20;

        protected override int DefaultUseTime => 31;
    }
}