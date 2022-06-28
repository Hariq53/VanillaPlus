using System.ComponentModel;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.ItemName.EnchantedSpear")]
    public class EnchantedSpearConfig : WeaponConfig
    {
        [DefaultValue(20)]
        public override int Damage { get => base.Damage; set => base.Damage = value; }

        [DefaultValue(31)]
        public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
    }
}