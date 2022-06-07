
using Terraria.ModLoader;
using VanillaPlus.Common.Models.ModBuffs;
using VanillaPlus.Content.Projectiles.Minions;

namespace VanillaPlus.Content.Buffs
{
    class CursedSkullMinionBuff : MinionBuff
    {
        public override bool IsLoadingEnabled(Mod mod) { return true; }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            minionProjectileIndex = ModContent.ProjectileType<CursedSkullMinion>();
        }
    }
}
