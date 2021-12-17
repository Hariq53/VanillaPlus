using System.Collections.Generic;
using Terraria;

namespace VanillaPlus.Common.BannerSlot
{
    public class BannerDatabase
    {
        class BannerEntry
        {
            public int BannerID { get; set; }
            public int NPCNetID { get; set; }

            public BannerEntry(int bannerID, int NPCID)
            {
                BannerID = bannerID;
                this.NPCNetID = NPCID;
            }
        }

        private List<BannerEntry> bannerEntries = new();

        public void AddEntry(int bannerID, int NPCNetID)
        {
            bannerEntries.Add(new BannerEntry(bannerID, NPCNetID));
        }

        public bool ContainsNPCNetID(int NPCNetID)
        {
            foreach (var bannerEntry in bannerEntries)
                if (NPCNetID == bannerEntry.NPCNetID)
                    return true;
            return false;
        }

        public bool ContainsBanner(int bannerID)
        {
            foreach (var bannerEntry in bannerEntries)
                if (bannerID == bannerEntry.BannerID)
                    return true;
            return false;
        }

        public bool ContainsItem(int type)
        {
            foreach (var bannerEntry in bannerEntries)
                if (type == Item.BannerToItem(bannerEntry.BannerID))
                    return true;
            return false;
        }

        public int GetNPCNetIDFromItem(int type)
        {
            foreach (var bannerEntry in bannerEntries)
                if (type == Item.BannerToItem(bannerEntry.BannerID))
                    return bannerEntry.NPCNetID;

            return 0;
        }

        public int GetBannerIDFromItem(int type)
        {
            foreach (var bannerEntry in bannerEntries)
                if (type == Item.BannerToItem(bannerEntry.BannerID))
                    return bannerEntry.BannerID;

            return 0;
        }
    }
}
