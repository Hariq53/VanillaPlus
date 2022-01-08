using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace VanillaPlus.Common.BannerSlot
{
    class BannerGlobals
    {
        class BannerGlobalItem : GlobalItem
        {
            public override bool IsLoadingEnabled(Mod mod)
            {
                return true;// ModContent.GetInstance<VanillaPlusConfig>().BannerSlotToggle;
            }

            public override bool AppliesToEntity(Item entity, bool lateInstantiation)
            {
                return VanillaPlus.Banners.ContainsItem(entity.type);
            }

            public override void SetDefaults(Item item)
            {
                item.accessory = true;
            }
        }

        class BannerGlobalNPC : GlobalNPC
        {
            public override bool IsLoadingEnabled(Mod mod)
            {
                return true;//ModContent.GetInstance<VanillaPlusConfig>().BannerSlotToggle;
            }

            public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
            {
                int bannerID = Item.NPCtoBanner(npc.BannerID());
                if (bannerID != 0)
                    VanillaPlus.Banners.AddEntry(bannerID, npc.netID);
                base.SetBestiary(npc, database, bestiaryEntry);
            }
        }
    }
}
