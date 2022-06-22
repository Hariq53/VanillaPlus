using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.ModProjectiles;
using VanillaPlus.Content.Buffs;

namespace VanillaPlus.Content.Items.Weapons
{
    class SkullOfBoom : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true; //.SkeletronDropsToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 20;
            Item.height = 28;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;

            // Weapon Specific
            Item.damage = 40;
            Item.shoot = ModContent.ProjectileType<SkullOfBoomProjectile>();
            Item.shootSpeed = 8f;
            Item.DamageType = DamageClass.Ranged;

            // Other
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
        }
    }

    class SkullOfBoomProjectile : ExplosiveProjectileFriendly
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 20;
            Projectile.height = 22;
            Projectile.timeLeft = 240;
            ExplosionDuration = 20;
            ExplosionSound = SoundID.Item74;
            ExplodeOnNPCCollision = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X;

            if (Projectile.velocity.Y != oldVelocity.Y)
                Projectile.velocity.Y = -oldVelocity.Y;

            Projectile.velocity *= 0.5f;
            return false;
        }

        public override void ExplosionEffects()
        {
            SoundEngine.PlaySound(ExplosionSound, Projectile.position);
            ExplosionDamage = Projectile.damage / 5;
        }

        public override void ExplosionLogic()
        {
            ProjectilesUtilities.InfernoForkVisualEffect(Projectile, DustID.UnusedWhiteBluePurple, 2.5f);
            base.ExplosionLogic();
        }

        public override void RegularAI()
        {
            if (Main.rand.Next(2) == 0)
            {
                Dust smokeDust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, Alpha: 100, Scale: Main.rand.NextFloat(1f, 1.5f));
                smokeDust.fadeIn = Main.rand.NextFloat(1.5f, 2f);
                smokeDust.noGravity = true;
                Vector2 projectileCenter = Projectile.Center;
                Vector2 spinningPoint = new(0f, (float)(-Projectile.height / 2));
                double radians = Projectile.rotation;
                smokeDust.position = projectileCenter + Utils.RotatedBy(spinningPoint, radians) * 1.1f;

                Dust fireDust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare_Blue, Alpha: 100, Scale: Main.rand.NextFloat(1.5f, 2f));
                fireDust.noGravity = true;
                spinningPoint = new(0f, (float)(-Projectile.height / 2 - 6));
                fireDust.position = projectileCenter + Utils.RotatedBy(spinningPoint, radians) * 1.1f;
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 5f)
            {
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X *= 0.97f;
                    if ((double)Projectile.velocity.X > -0.01 && (double)Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                Projectile.velocity.Y += 0.2f;
            }
            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
            target.AddBuff(ModContent.BuffType<Aquaflame>(), 60);
        }
    }
}