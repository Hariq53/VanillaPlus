﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Content.Items.Weapons
{
    class Dualies : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().DualiesToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 58;
            Item.height = 42;
            Item.UseSound = SoundID.Item41;

            // Animation
            Item.useTime = 11;
            Item.useAnimation = 22;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            // Weapon Specific
            Item.damage = 15;
            Item.knockBack = 4f;
            Item.crit = 5;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.DamageType = DamageClass.Ranged;

            // Other
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 20);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Revolver, 2)
                .Register();
        }

        bool shot = false;
        int secondRevolverIndex;
        public static readonly Vector2 offset = new(5f, 3f);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 direction = Vector2.Normalize(velocity);
            if (player.direction == -1)
                position += direction * offset.X;
            SoundEngine.PlaySound(SoundID.Item41);
            if (!shot)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DualiesRevolver>(), 0, 0, player.whoAmI, player.itemAnimation);
                secondRevolverIndex = Projectile.NewProjectile(source, position.X - offset.X * player.direction, position.Y - offset.Y, velocity.X, velocity.Y, ModContent.ProjectileType<DualiesRevolver>(), 0, 0, player.whoAmI, player.itemAnimation);
                shot = true;
            }
            else
            {
                Main.projectile[secondRevolverIndex].velocity = velocity;
                shot = false;
            }
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
        }
    }

    class DualiesRevolver : ModProjectile
    {
        public override void SetDefaults()
        {
            // GFX
            Projectile.scale = 0.85f;

            // Hitbox
            Projectile.width = 52;
            Projectile.height = 20;

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.damage = 0;

            // Movement
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        private bool changed = false;
        private Vector2 playerOffset;

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (!changed)
            {
                Projectile.timeLeft = (int)Projectile.ai[0];
                playerOffset = player.position - Projectile.position;
                changed = true;
            }
            Projectile.position = player.position - playerOffset;
            Projectile.velocity.Normalize();
            Projectile.velocity *= (float)(Projectile.width / 2);
            ProjectilesUtilities.FaceForwardHorizontalSprite(Projectile);
            return base.PreAI();
        }
    }
}
