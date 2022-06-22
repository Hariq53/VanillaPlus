using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Models.ModItems
{
    public abstract class ConfigurableWeapon : ConfigurableItem
    {
        /// <summary>
        /// The config instance this weapon should pull data from
        /// </summary>
        protected override WeaponConfig? Config => new(hardDisabled: true);

        public override bool ShouldLoad()
        {
            // If all weapons are disabled, do not load

            return base.ShouldLoad();
        }

        protected override sealed void SetConfigurableDefaults(ItemConfig config)
        {
            if (config is WeaponConfig weaponConfig)
            {
                Item.damage = weaponConfig.Damage;
                Item.useTime = Item.useAnimation = weaponConfig.UseTime;
                SetConfigurableDefaults(weaponConfig);
            }
        }

        /// <summary>
        /// This is where you set all your weapon's properties that depend on the config,
        /// this method gets called only if <c>Config</c> is not null
        /// </summary>
        /// <param name="config">See <see cref="Config" /></param>
        protected virtual void SetConfigurableDefaults(WeaponConfig config)
        {
        }
    }
}
