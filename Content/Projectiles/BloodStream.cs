using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.Projectiles
{
    class BloodStream : ModProjectile
    {
        public override void SetDefaults()
        {
            // GFX and animation
            Projectile.alpha = 255;

            // Hitbox and collision
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.ignoreWater = true;

            // Damage
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;

            // AI
            Projectile.timeLeft = 30;

            // Other
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Common.ProjectilesUtilities.ApplyGravity(Projectile, 0.2f);
            if (Projectile.timeLeft > 5)
                for (int i = 0; i < 5; i++)
                    Dust.NewDust(Projectile.position + Vector2.Normalize(Projectile.velocity) * 44, Projectile.width / 2, Projectile.height / 2, DustID.Blood, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, 50);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.CthulhusMalediction>(), 300);
        }
    }
}
