using Terraria;
using Terraria.ID;

namespace VanillaPlus.Common
{
    class ItemUtilities
    {
        // Code based on the function of the same name from "Player" class (tModLoader/Terraria source code)
        static public void EmitMaxManaEffect(Player player)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana, player.position);
            for (int i = 0; i < 5; i++)
            {
                Dust obj = Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.ManaRegeneration, 0f, 0f, 255, default, Main.rand.NextFloat(20, 26) * 0.1f)];
                obj.noLight = true;
                obj.noGravity = true;
                obj.velocity *= 0.5f;
            }
        }
    }
}
