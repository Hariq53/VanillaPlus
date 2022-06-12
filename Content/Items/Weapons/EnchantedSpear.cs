using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Content.Items.Weapons
{
    class EnchantedSpear : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().EnchantedSpearToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 32;
            Item.height = 32;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useAnimation = 31;
            Item.useTime = 31;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            // Weapon Specific
            Item.damage = 20;
            Item.knockBack = 6.5f;
            Item.shoot = ModContent.ProjectileType<EnchantedSpearProjectile>();
            Item.shootSpeed = 3.7f;
            Item.DamageType = DamageClass.Melee;

            // Other
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Spear, 1)
                .AddIngredient(ItemID.FallenStar, 1)
                .Register();
        }
    }

    class EnchantedSpearProjectile : ModProjectile
    {
        // Define the range of the Spear.
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 96f;

        public override void SetDefaults()
        {
            // Clone the default values for a vanilla spear.
            Projectile.CloneDefaults(ProjectileID.Spear);
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = Projectile.whoAmI;

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
                Projectile.timeLeft = duration;

            // Velocity isn't used in the projectile, so it stores the spear's attack direction.
            Projectile.velocity.Normalize();

            float halfDuration = duration * 0.5f;
            float progress;

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation. 
            if (Projectile.timeLeft < halfDuration)
                progress = Projectile.timeLeft / halfDuration;
            else
                progress = (duration - Projectile.timeLeft) / halfDuration;

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Vector2 progressVector = Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin,
                                                        Projectile.velocity * HoldoutRangeMax, progress);
            Projectile.Center = player.MountedCenter + progressVector;

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.PiOver4;
            }
            else
            {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.Pi - MathHelper.PiOver4;
            }

            if (!Main.dedServ)
            {
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height,
                                                   Main.rand.Next(3) switch
                                                   {
                                                       0 => DustID.MagicMirror,
                                                       1 => DustID.Enchanted_Gold,
                                                       _ => DustID.Enchanted_Pink,
                                                   }, Projectile.direction * 2, 0f, 150, Scale: 1.3f);
                    
                    dust.velocity *= 0.2f;
                }
            }
            return false; // Don't execute vanilla AI.
        }

        bool ProjectileShotFlag
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : -1f;
        }

        public override void PostAI()
        {
            // Shoot a projectile once in this projectile's lifetime
            if (!ProjectileShotFlag)
            {
                ProjectileShotFlag = true;
                
                Vector2 spearTip = Projectile.Center + (Projectile.velocity * Projectile.width / 2);
                float projectileSpeed = 9.5f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spearTip,
                                         Projectile.velocity * projectileSpeed, ProjectileID.EnchantedBeam,
                                         Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }

}
