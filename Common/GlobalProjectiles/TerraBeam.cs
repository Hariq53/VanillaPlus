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

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ProjectileID.TerraBeam;
        }

        public override void SetDefaults(Projectile projectile)
        {
            projectile.timeLeft = 65;
        }
    }
}
