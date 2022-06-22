using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Content.Items.Weapons
{
    public class FangOfCthulhu : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true; //.EOCDropsToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 27;
            Item.useTime = 27;
            Item.width = 22;
            Item.height = 44;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item5;
            Item.damage = 15;
            Item.shootSpeed = 6.7f;
            Item.knockBack = 1f;
            Item.alpha = 30;
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.value = 18000;
            Item.DamageType = DamageClass.Ranged;
        }
    }
}
