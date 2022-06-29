using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Models.ModItems;

namespace VanillaPlus.Content.Items.Weapons
{
    class ConfigurableSword : ConfigurableWeapon
    {
        protected override void SetRegularDefaults()
        {
            Item.damage = 20;
            Item.width = Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }
    }
}
