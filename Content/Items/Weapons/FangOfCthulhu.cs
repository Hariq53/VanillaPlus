using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;

namespace VanillaPlus.Content.Items.Weapons
{
    public class FangOfCthulhu : ConfigurableWeapon
    {
        protected override WeaponConfig? Config => VanillaPlus.ServerSideConfig?.Items.FangOfCthulhu;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            // GFX
            Item.width = 22;
            Item.height = 44;
            Item.UseSound = SoundID.Item5;
            Item.alpha = 30;

            // Animation
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = Item.useTime = 27;

            // Weapon Specific
            Item.damage = 15;
            Item.knockBack = 1f;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 6.7f;
            Item.useAmmo = AmmoID.Arrow;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;

            // Other
            Item.value = Item.sellPrice(gold: 1, silver: 8);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
