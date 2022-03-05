using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Presets;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Content.Projectiles
{
    class ChristmasBarragePlane : ExplosiveProjectileFriendly
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            // HitBox
            Projectile.width = 32;
            Projectile.height = 32;

            // Damage
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;

            // AI
            Projectile.timeLeft = 120;

            ExplodeOnTileCollision = true;
            ExplodeOnNPCCollision = true;
            ExplosionHitBoxDimensions = new(100, 100);
        }

        int currentFrame = 0;

        public override bool PreAI()
        {
            if (!ShouldExplode())
            {
                if (++Projectile.frameCounter >= 5)
                {
                    Projectile.frameCounter = 0;
                    currentFrame += 2;
                    if (currentFrame >= Main.projFrames[Projectile.type])
                        currentFrame = 0;
                }
                if (Projectile.timeLeft % 40 == 0)
                {
                    currentFrame++;
                    
                    IEntitySource source = Projectile.GetProjectileSource_FromThis();
                    if (Projectile.owner == Main.myPlayer)
                        if (Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<ChristmasBarrage>())
                            source = Main.player[Main.myPlayer].GetProjectileSource_Item(Main.player[Main.myPlayer].HeldItem);
                    Projectile.NewProjectile(source, Projectile.Center + Projectile.velocity, Projectile.velocity, ModContent.ProjectileType<ChristmasBarrageBomb>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
            return true;
        }

        public override void RegularAI()
        {
            ProjectilesUtilities.FaceForwardHorizontalSprite(Projectile);
            if (Projectile.spriteDirection == 1)
            {
                this.DrawOffsetX = -32;
                this.DrawOriginOffsetX = 16;
                this.DrawOriginOffsetY = 0;
            }
            else
            {
                this.DrawOffsetX = 0;
                this.DrawOriginOffsetX = -16;
                this.DrawOriginOffsetY = 0;
            }
            Projectile.frame = currentFrame;
        }
    }
}
