using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Content.Projectiles
{
    class TearEye : ModProjectile
    {
        public override void SetDefaults()
        {
            // HitBox
            Projectile.width = 12;
            Projectile.height = 12;

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        bool modified = false;
        int ownerIndex = 0;
        float distance = 0f;
        float rotation = 0f;
        readonly static float rotationSpeed = 0.3f;

        public override void AI()
        {
            if (!modified)
            {
                ownerIndex = (int)Projectile.ai[0];
                if (Main.projectile[ownerIndex].ModProjectile is TearProjectile tearProjectile)
                {
                    distance = tearProjectile.distance;
                    rotation = MathHelper.ToDegrees(Projectile.velocity.ToRotation());
                }
                modified = true;
            }
            Projectile owner = Main.projectile[ownerIndex];
            Projectile.velocity = Vector2.Zero;
            ProjectilesUtilities.ApplyRotation(Projectile, rotationSpeed);
            Projectile.Center = owner.Center + MathHelper.ToRadians(rotation++).ToRotationVector2() * distance;
        }
    }
}