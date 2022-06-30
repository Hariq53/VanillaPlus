using Terraria;
using Terraria.Audio;
using Terraria.ID;
using VanillaPlus.Common.Config.GameplayTweaks;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.GlobalProjectiles;

namespace VanillaPlus.Common.GameplayTweaks.GlobalProjectiles
{
    class InfluxWaverTweak : ConfigurableGlobalProjectile
    {
        protected override TweakConfig? Config => VanillaPlus.ServerSideConfig?.GameplayTweaks.InfluxWaverTweak;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.InfluxWaver;
        }

        public override void SetDefaults(Projectile projectile)
        {
            projectile.timeLeft = 70;
            if (Config is InfluxWaverTweakConfig config)
                projectile.timeLeft = config.LifeTime;
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            base.Kill(projectile, timeLeft);
            if (timeLeft > 0)
                return;
            SoundEngine.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height,
                                                DustID.InfluxWaver, -projectile.velocity.X,
                                                -projectile.velocity.Y, 100, Scale: 2f);
                dust.noGravity = true;
                dust.velocity *= Main.rand.NextFloat(0.1f, 1.5f);
            }
        }
    }
}
