using Terraria.ModLoader;
using VanillaPlus.Common.BannerSlot;

namespace VanillaPlus
{
    public class VanillaPlus : Mod
    {
        internal static BannerDatabase Banners = new();

        public override void Unload()
        {
            Banners = null;
        }
    }
}