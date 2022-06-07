using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Models.ModItems
{
    class ConfigurableItem : ModItem
    {
        protected virtual ItemConfig Config => new(hardDisabled: true);

        public bool ShouldLoad()
        {
            return !Config.IsHardDisabled;
        }

        public bool ShouldAddRecipes()
        {
            return !Config.IsSoftDisabled;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShouldLoad();
        }

        public override void SetDefaults()
        {
            Type t = Config.GetType();
            ItemConfig conf = Config;
            if (conf != null)
                if (conf is WeaponConfig weaponConfig)
                {
                    Item.useTime = 11;
                    Item.useAnimation = 22;
                    Item.autoReuse = false;
                    Item.useStyle = ItemUseStyleID.Shoot;
                    Item.DamageType = DamageClass.Melee;

                    int dmg = weaponConfig.Damage;
                    Item.damage = dmg;
                    //Item.accessory = true;
                }
        }

        public override void AddRecipes()
        {
            if (!ShouldAddRecipes())
                return;
        }
    }
}
