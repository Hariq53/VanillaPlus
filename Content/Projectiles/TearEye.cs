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

        public int OwnerID
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        float CurrentRotation
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        bool SetupFlag
        {
            get => Projectile.localAI[0] == 1f;
            set => Projectile.localAI[0] = value ? 1f : -1f;
        }

        const float DISTANCE = 20f;
        const float ROTATION_SPEED = 0.3f;

        public override void AI()
        {
            if (!SetupFlag)
            {
                if (Main.projectile[OwnerID].type == ModContent.ProjectileType<TearProjectile>())
                    CurrentRotation = MathHelper.ToDegrees(Projectile.velocity.ToRotation());
                else
                    Projectile.Kill();
                SetupFlag = true;
            }

            Projectile.velocity = Vector2.Zero;

            // Rotate on projectile's axis
            ProjectilesUtilities.ApplyRotation(Projectile, ROTATION_SPEED);

            // Orbit around the owner projectile
            Projectile.Center = Main.projectile[OwnerID].Center + MathHelper.ToRadians(CurrentRotation++).ToRotationVector2() * DISTANCE;

            if (CurrentRotation > 360)
                CurrentRotation -= 360;
        }
    }
}