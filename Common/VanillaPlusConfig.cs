using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common
{
    [Label("$Mods.VanillaPlus.Config.ServerSideConfig")]
    public class VanillaPlusConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.VanillaPlus.Config.ItemHeader")]

        [Label("$Mods.VanillaPlus.Config.EvilMaceToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.EvilMaceToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool EvilMaceToggle;

        [Label("$Mods.VanillaPlus.Config.EnchantedSpearToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.EnchantedSpearToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnchantedSpearToggle;

        [Label("$Mods.VanillaPlus.Config.ChristmasBarrageToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.ChristmasBarrageToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ChristmasBarrageToggle;

        [Label("$Mods.VanillaPlus.Config.DualiesToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.DualiesToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool DualiesToggle;

        [Label("$Mods.VanillaPlus.Config.ShinyCharmToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.ShinyCharmToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ShinyCharmToggle;

        [Label("$Mods.VanillaPlus.Config.EOCDropsToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.EOCDropsToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool EOCDropsToggle;

        [Label("$Mods.VanillaPlus.Config.GoblinDropsToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GoblinDropsToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool GoblinDropsToggle;

        [Label("$Mods.VanillaPlus.Config.SkeletonDropsToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.SkeletonDropsToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool SkeletronDropsToggle;

        [Header("$Mods.VanillaPlus.Config.TilesHeader")]

        [Label("$Mods.VanillaPlus.Config.LifeFruitLanternToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.LifeFruitLanternToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool LifeFruitLanternToggle;

        [Header("$Mods.VanillaPlus.Config.TweaksHeader")]

        [Label("$Mods.VanillaPlus.Config.GladiusTweakToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GladiusTweakToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool GladiusTweakToggle;

        [Label("$Mods.VanillaPlus.Config.FlyingDragonTweakToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.FlyingDragonTweakToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool FlyingDragonTweakToggle;

        [Label("$Mods.VanillaPlus.Config.TerraBeamTweakToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.TerraBeamTweakToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool TerraBeamTweakToggle;

        [Label("$Mods.VanillaPlus.Config.InfluxWaverTweakToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.InfluxWaverTweakToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool InfluxWaverTweakToggle;
    }

    [Label("$Mods.VanillaPlus.Config.ClientSideConfig")]
    public class VanillaPlusClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.VanillaPlus.Config.ItemHeader")]

        [Label("$Mods.VanillaPlus.Config.TearAltToggle.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.TearAltToggle.Tooltip")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool TearAltToggle;
    }
}
