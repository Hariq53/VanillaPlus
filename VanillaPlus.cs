using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus
{
    public class VanillaPlus : Mod
    {
        public static VanillaPlusServerConfig? ServerSideConfig => ModContent.GetInstance<VanillaPlusServerConfig>();
        public static VanillaPlusClientConfig? ClientSideConfig => ModContent.GetInstance<VanillaPlusClientConfig>();
    }
}