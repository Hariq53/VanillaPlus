using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;

namespace VanillaPlus.Content.Items.Weapons
{
    public class WarriorsMallet : ConfigurableWeapon
    {
        protected override WeaponConfig? Config => VanillaPlus.ServerSideConfig?.Items.WarriorsMallet;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            // GFX
            Item.width = 32;
            Item.height = 32;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useTime = Item.useAnimation = 33;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;

            // Weapon Specific
            Item.damage = 40;
            Item.knockBack = 10f;
            Item.crit = 5;
            Item.DamageType = DamageClass.Melee;

            // Other
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
        }
    }
}