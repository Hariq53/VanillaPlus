using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Common.GameplayTweaks.GlobalProjectiles
{
    class InfluxWaver : GlobalProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true; //.InfluxWaverTweakToggle;
        }

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.InfluxWaver;
        }

        public override void SetDefaults(Projectile projectile)
        {
            projectile.timeLeft = 70;
        }
    }
}
