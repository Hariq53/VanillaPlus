using Microsoft.Xna.Framework;
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
        // Define the range of the Spear Projectile. These are overrideable properties, in case you'll want to make a class inheriting from Projectile one.
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 96f;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.  
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for Projectile
            int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

            player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
                Projectile.timeLeft = duration;

            Projectile.velocity.Normalize(); // Velocity isn't used in Projectile spear implementation, but we use the field to store the spear's attack direction.

            float halfDuration = duration * 0.5f;
            float progress;

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation. 
            if (Projectile.timeLeft < halfDuration)
                progress = Projectile.timeLeft / halfDuration;
            else
                progress = (duration - Projectile.timeLeft) / halfDuration;

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.ToRadians(135f);
            }

            if (!Main.dedServ)
            {
                // These dusts are added later, for the 'ExampleMod' effect
                if (Main.rand.NextBool(3))
                {
                    int num31 = Main.rand.Next(3);
                    int num = Dust.NewDust(new Vector2((float)Projectile.Center.X, (float)Projectile.Center.Y), Projectile.width, Projectile.height, num31 switch
                    {
                        0 => 15,
                        1 => 57,
                        _ => 58,
                    }, Projectile.direction * 2, 0f, 150, default(Color), 1.3f);
                    Dust obj = Main.dust[num];
                    obj.velocity *= 0.2f;
                }
            }
            return false; // Don't execute vanilla AI.
        }

        bool shot = false;

        public override void PostAI()
        {
            if (!shot)
            {
                shot = true;
                Vector2 SpearTip = Projectile.Center + (Projectile.velocity * Projectile.width / 2);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), SpearTip, Projectile.velocity * 9.5f, ProjectileID.EnchantedBeam, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
        }
    }

}
