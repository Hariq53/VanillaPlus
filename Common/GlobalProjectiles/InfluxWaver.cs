using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Common.GlobalProjectiles
{
    class InfluxWaver : GlobalProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().InfluxWaverTweakToggle;
        }

        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.InfluxWaver)
                projectile.timeLeft = 70;
            base.SetDefaults(projectile);
        }
    }
}
