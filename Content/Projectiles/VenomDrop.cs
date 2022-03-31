using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Projectiles
{
    class VenomDrop : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            ProjectilesUtilities.ApplyGravity(Projectile, 0.1f);
            ProjectilesUtilities.FaceForwardHorizontalSprite(Projectile);
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PoisonStaff, Projectile.velocity.X, Projectile.velocity.Y, 100, Scale: 1.2f);
            dust.noGravity = true;
            dust.velocity *= 0.3f;
            dust.velocity -= Projectile.velocity * 0.4f;
        }

        public override void Kill(int timeLeft)
        {

            for (int num398 = 0; num398 < 15; num398++)
            {
                int num399 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PoisonStaff, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num399].noGravity = true;
                Dust dust160 = Main.dust[num399];
                Dust dust310 = dust160;
                dust310.velocity *= 1.2f;
                dust160 = Main.dust[num399];
                dust310 = dust160;
                dust310.velocity -= Projectile.oldVelocity * 0.3f;
            }
        }
    }
}
