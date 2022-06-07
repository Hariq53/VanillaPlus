using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus.Content.Items.Weapons
{
    class TheOcularMenace : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().EOCDropsToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 44;
            Item.height = 18;
            Item.UseSound = SoundID.Item13;

            // Animation
            Item.useAnimation = 18;
            Item.useTime = 18;
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
