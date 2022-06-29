using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus.Content.Items.Weapons
{
    class TheOcularMenace : ConfigurableWeapon
    {
        protected override WeaponConfig? Config => VanillaPlus.ServerSideConfig?.Items.TheOcularMenace;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            // GFX
            Item.width = 44;
            Item.height = 18;
            Item.UseSound = SoundID.Item13;

            // Animation
            Item.useAnimation = Item.useTime = 18;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;

            // Weapon Specific
            Item.damage = 12;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<BloodStream>();
            Item.shootSpeed = 12.5f;
            Item.mana = 4;
            Item.DamageType = DamageClass.Magic;

            // Other
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 80);
        }
    }
}
