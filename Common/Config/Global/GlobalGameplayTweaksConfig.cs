using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.Global
{
    public class GlobalGameplayTweaksConfig : TweakConfig
    {
        [Label("$Mods.VanillaPlus.Config.TweakConfig.DisableAll.Label")]
        [ReloadRequired]
        public override bool IsDisabled { get => base.IsDisabled; set => base.IsDisabled = value; }
    }
}
