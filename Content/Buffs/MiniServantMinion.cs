using Terraria.ModLoader;
using VanillaPlus.Common.Presets.SummonWeapon;
using VanillaPlus.Content.Projectiles.Minions;

namespace VanillaPlus.Content.Buffs
{
    class MiniServantMinion : MinionBuff
    {
        public override bool IsLoadingEnabled(Mod mod) { return true; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            minionProjectileIndex = ModContent.ProjectileType<MiniServant>();
        }
    }
}