using Terraria;
using Terraria.ModLoader;

namespace VanillaPlus.Content.Buffs
{
    class MotorcycleMount : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Motorcycle Mount");
            Description.SetDefault("Bruuuuuum Brummmm");
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Mount.Motorcycle>(), player);
            player.buffTime[buffIndex] = 10; // reset buff time
        }

    }
}
