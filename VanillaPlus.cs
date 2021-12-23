using Terraria.ModLoader;
using VanillaPlus.Common.BannerSlot;
using VanillaPlus.Content.Buffs;

namespace VanillaPlus
{
    public class VanillaPlus : Mod
    {
        internal static BannerDatabase Banners = new();

        public override void Unload()
        {
            Banners = null;
            Aquaflame.affectedNpcs.Clear();
            CthulhusMalediction.affectedNpcs.Clear();
        }
    }
}