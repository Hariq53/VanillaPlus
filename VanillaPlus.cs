using Terraria.ModLoader;
using VanillaPlus.Common.BannerSlot;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;
using VanillaPlus.Content.Items.Accessories;
using VanillaPlus.Content.Items.Weapons;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus
{
    public class VanillaPlus : Mod
    {
        public static VanillaPlusServerConfig? ServerSideConfig => ModContent.GetInstance<VanillaPlusServerConfig>();
        public static VanillaPlusClientConfig? ClientSideConfig => ModContent.GetInstance<VanillaPlusClientConfig>();
        public static VanillaPlusExperimentalConfig? ExperimentalConfig => ModContent.GetInstance<VanillaPlusExperimentalConfig>();

        internal static BannerDatabase Banners = new();

        public override void Load()
        {
            base.Load();
        }

        public override void Unload()
        {
            Banners = null!;
        }
    }
}