using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Items.Weapons
{
    public class RottenMace : FleshMace
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().EvilMaceToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.damage = 30;
            Item.shoot = ModContent.ProjectileType<Projectiles.RottenBit>();
            SwingDust = DustID.CorruptGibs;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ZombieArm, 1)
                .AddIngredient(ItemID.RottenChunk, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
    class RottenMaceModPlayer : ModPlayer
    {
        public override void PostItemCheck()
        {
            if (Player.itemTime == 1 && Player.HeldItem.type == ModContent.ItemType<FleshMace>())
            {
                if (!Player.JustDroppedAnItem)
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana, Player.position);
                PlayerInput.TryEndingFastUse();
            }
            base.PostItemCheck();
        }
    }
}
