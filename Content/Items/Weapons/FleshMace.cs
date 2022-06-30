using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus.Content.Items.Weapons
{
    public class FleshMace : ConfigurableWeapon
    {
        protected override WeaponConfig? Config => VanillaPlus.ServerSideConfig?.Items.FleshMace;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            // GFX
            Item.width = 34;
            Item.height = 34;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useTime = 44;
            Item.useAnimation = 22;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;

            // Weapon Specific
            Item.damage = 25;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<FleshBall>();
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Melee;

            // Other
            Item.value = Item.sellPrice(gold: 1, silver: 50);
            Item.rare = ItemRarityID.Green;
        }

        protected override void SetConfigurableDefaults(WeaponConfig config)
        {
            Item.useTime *= 2;
        }

        protected override void AddRecipesWithConfig()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ZombieArm, 1)
                .AddIngredient(ItemID.Vertebrae, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        internal static int SwingDust { get; set; } = DustID.Blood;

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