using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Config
{
    [Label("$Mods.VanillaPlus.Config.ClientSideConfig")]
    public class VanillaPlusClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.VanillaPlus.Config.Items.Header")]

        [Label("$Mods.VanillaPlus.Config.TearAlt.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.TearAlt.Tooltip")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool TearAlt;
    }
}
