using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Models.ModItems
{
    abstract class ConfigurableWeapon : ConfigurableItem
    {
        protected override WeaponConfig Config => new(hardDisabled: true);

        protected override void SetConfigurableDefaults()
        {
            base.SetConfigurableDefaults();

            Item.damage = Config.Damage;
            Item.useTime = Item.useAnimation = Config.UseTime;
        }
    }
}
