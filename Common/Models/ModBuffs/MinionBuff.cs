using Terraria;
using Terraria.ModLoader;

namespace VanillaPlus.Common.Models.ModBuffs
{
    class MinionBuff : ModBuff
    {
        public override bool IsLoadingEnabled(Mod mod) { return false; }

        protected int minionProjectileIndex = 0;

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[minionProjectileIndex] > 0)
                player.buffTime[buffIndex] = 18000;
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
