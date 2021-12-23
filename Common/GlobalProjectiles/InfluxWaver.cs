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
