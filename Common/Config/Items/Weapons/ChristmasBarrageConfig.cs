using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.ItemName.ChristmasBarrage")]
    public class ChristmasBarrageConfig : WeaponConfig
    {
        protected override int DefaultDamage => 125;

        protected override int DefaultUseTime => 17;
    }
}
