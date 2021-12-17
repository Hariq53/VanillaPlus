using Terraria;
using Terraria.ModLoader;

namespace VanillaPlus.Common.BannerSlot
{
    internal class BannerSlotPlayer : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().BannerSlotToggle;
        }

        public bool banner;
        public int equippedBanner = -1;

        public override void ResetEffects()
        {
            banner = false;
        }

        public override void PreUpdate()
        {
            if (banner)
            {
                if (equippedBanner != -1)
                {
                    Main.SceneMetrics.hasBanner = true;
                    Main.SceneMetrics.NPCBannerBuff[VanillaPlus.Banners.GetBannerIDFromItem(equippedBanner)] = true;
                }
            }
        }
    }
}
