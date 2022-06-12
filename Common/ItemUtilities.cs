using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace VanillaPlus.Common
{
    static class ItemUtilities
    {
        // Code based on the function of the same name from "Player" class (tModLoader/Terraria source code)
        static public void EmitMaxManaEffect(Player player)
        {
            SoundEngine.PlaySound(SoundID.MaxMana, player.position);
            for (int i = 0; i < 5; i++)
            {
                float scale = Main.rand.NextFloat(20, 26) * 0.1f;
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height, 
                                              DustID.ManaRegeneration, Alpha: 255, Scale: scale);
                dust.noLight = true;
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
    }
}
