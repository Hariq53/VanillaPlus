using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus.Content.Items.Weapons
{
    class ChristmasBarrage : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().ChristmasBarrageToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 28;
            Item.height = 30;
            Item.UseSound = SoundID.Item21;

            // Animation
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;

            // Weapon Specific
            Item.damage = 125;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<ChristmasBarragePlane>();
            Item.shootSpeed = 8f;
            Item.mana = 22;
            Item.DamageType = DamageClass.Magic;

            // Other
            Item.value = Item.sellPrice(gold: 9);
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
