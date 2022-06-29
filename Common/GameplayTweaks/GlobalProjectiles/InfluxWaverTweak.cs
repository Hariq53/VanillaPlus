using Terraria;
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
    }
}
