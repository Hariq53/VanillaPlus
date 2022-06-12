using Terraria.ModLoader;
using VanillaPlus.Common.BannerSlot;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;
using VanillaPlus.Content.Items.Weapons;
using VanillaPlus.Content.Projectiles;
using static VanillaPlus.Common.Config.VanillaPlusExperimentalConfig.ItemsConfig.EvilMacesConfig;

namespace VanillaPlus
{
    public class VanillaPlus : Mod
    {
        public static VanillaPlusServerConfig ServerSideConfig => ModContent.GetInstance<VanillaPlusServerConfig>();
        public static VanillaPlusClientConfig ClientSideConfig => ModContent.GetInstance<VanillaPlusClientConfig>();
        public static VanillaPlusExperimentalConfig ExperimentalConfig => ModContent.GetInstance<VanillaPlusExperimentalConfig>();

        internal static BannerDatabase Banners = new();

        public override void Load()
        {
            ApplyConfig();
            base.Load();
        }

        private static void ApplyConfig()
        {
            #region Items
            ItemConfig.ForceDisableAllItems = ExperimentalConfig.ItemsDisabled;

            #region Flesh Mace
            FleshMaceConfig fleshMaceConfig = ExperimentalConfig.Items.FleshMace!;
            
            if (fleshMaceConfig is not null)
            {
                FleshBall.StickingLifeTime = fleshMaceConfig.StickingLifeTime;
            }
            #endregion

            #endregion

        }

        public override void Unload()
        {
            Banners = null!;
        }
    }
}