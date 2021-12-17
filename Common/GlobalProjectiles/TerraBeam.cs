using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Common.GlobalProjectiles
{
    class TerraBeam : GlobalProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().TerraBeamTweakToggle;
        }

        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.TerraBeam)
                projectile.timeLeft = 65;
            base.SetDefaults(projectile);
        }
    }
}
