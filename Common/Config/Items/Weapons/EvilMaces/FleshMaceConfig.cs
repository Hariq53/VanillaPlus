using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons.EvilMaces
{
    [Label("$Mods.VanillaPlus.ItemName.FleshMace")]
    public class FleshMaceConfig : WeaponConfig
    {
        protected override int DefaultDamage => 25;

        protected override int DefaultUseTime => 45;
    }
}
