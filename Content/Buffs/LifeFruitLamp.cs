using Terraria;
using Terraria.ModLoader;

namespace VanillaPlus.Content.Buffs
{
    class LifeFruitLamp : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; // Add this so the nurse doesn't remove the buff when healing
            Main.buffNoSave[Type] = true;
        }

        // Allows you to make this buff give certain effects to the given player
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LifeFruitRegenPlayer>().lifeRegenBuff = true;
        }
    }

    public class LifeFruitRegenPlayer : ModPlayer
    {
        public bool lifeRegenBuff;

        public override void ResetEffects()
        {
            lifeRegenBuff = false;
        }

        public override void UpdateLifeRegen()
        {
            if (lifeRegenBuff)
                Player.lifeRegen += 4;
        }
    }
}
