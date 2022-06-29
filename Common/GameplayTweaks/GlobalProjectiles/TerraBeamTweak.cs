using Terraria;
using Terraria.ID;
using VanillaPlus.Common.Config.GameplayTweaks;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.GlobalProjectiles;

namespace VanillaPlus.Common.GameplayTweaks.GlobalProjectiles
{
    class TerraBeamTweak : ConfigurableGlobalProjectile
    {
        protected override TweakConfig? Config => VanillaPlus.ServerSideConfig?.GameplayTweaks.TerraBladeTweak;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.TerraBeam;
        }

        public override void SetDefaults(Projectile projectile)
        {
            projectile.timeLeft = 65;
            if (Config is TerraBladeTweakConfig config)
                projectile.timeLeft = config.LifeTime;
        }
    }
}
