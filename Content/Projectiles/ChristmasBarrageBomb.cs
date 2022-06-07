using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Models.ModProjectiles;

namespace VanillaPlus.Content.Projectiles
{
    class ChristmasBarrageBomb : ExplosiveProjectileFriendly
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            // Hitbox
            Projectile.width = 24;
            Projectile.height = 10;

            // Damage
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;

            // AI
            Projectile.timeLeft = 120;

            ExplodeOnTileCollision = true;
            ExplodeOnNPCCollision = true;
            ExplosionHitBoxDimensions = new(100, 100);
        }

        public override void RegularAI()
        {
            ProjectilesUtilities.ApplyGravity(Projectile, 0.3f);
            ProjectilesUtilities.FaceForwardHorizontalSprite(Projectile);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Projectile.velocity.X * 0.3f, Projectile.velocity.Y * 0.3f, 150);
        }
    }
}
