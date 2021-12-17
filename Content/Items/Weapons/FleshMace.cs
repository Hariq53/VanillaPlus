using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Items.Weapons
{
    public class FleshMace : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().EvilMaceToggle;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 34;
            Item.height = 34;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useAnimation = 21;
            Item.useTime = 45;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;

            // Weapon Specific
            Item.damage = 25;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<Projectiles.FleshBall>();
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Melee;

            // Other
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ZombieArm, 1)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        private protected int SwingDust = DustID.Blood;

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, SwingDust);
        }

        class FleshMaceModPlayer : ModPlayer
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
}