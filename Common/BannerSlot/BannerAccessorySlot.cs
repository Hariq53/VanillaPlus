using Terraria;
using Terraria.ModLoader;

namespace VanillaPlus.Common.BannerSlot
{
    class BannerAccessorySlot : ModAccessorySlot
    {
        public override bool DrawVanitySlot => false;
        public override bool DrawDyeSlot => false;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;//ModContent.GetInstance<VanillaPlusConfig>().BannerSlotToggle;
        }

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return VanillaPlus.Banners.ContainsItem(checkItem.type);
        }

        public override void ApplyEquipEffects()
        {
            if (!IsEmpty)
            {
                BannerSlotPlayer bannerSlotPlayer = Main.player[Main.myPlayer].GetModPlayer<BannerSlotPlayer>();
                bannerSlotPlayer.equippedBanner = FunctionalItem.type;
                bannerSlotPlayer.banner = true;
            }
        }
    }
}
