using System.ComponentModel;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Items.Weapons.EvilMaces
{
    [Label("$Mods.VanillaPlus.ItemName.RottenMace")]
    public class RottenMaceConfig : WeaponConfig
    {
        [DefaultValue(30)]
        public override int Damage { get => base.Damage; set => base.Damage = value; }

        [DefaultValue(45)]
        public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
    }
}
