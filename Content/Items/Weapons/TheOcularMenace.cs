using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus.Content.Items.Weapons
{
    class TheOcularMenace : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().EOCDropsToggle;
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
