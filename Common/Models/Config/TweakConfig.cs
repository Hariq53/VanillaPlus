using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    [Label("$Mods.VanillaPlus.Config.TweakConfig.Label")]
    public class TweakConfig : ElementConfig
    {
        public override bool IsEnabled() => !IsDisabled!;

        private bool _isDisabled;

        [Label("$Mods.VanillaPlus.Config.TweakConfig.Disable.Label")]
        [BackgroundColor(127, 0, 0)]
        [ReloadRequired]
        public virtual bool IsDisabled
        {
            get
            {
                if (SuperConfig is TweakConfig superConfig && superConfig.CanOverride(this))
                    return superConfig.IsDisabled;

                return _isDisabled;
            }

            set
            {
                if (SuperConfig is TweakConfig superConfig && superConfig.CanOverride(this))
                    return;

                _isDisabled = value;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is TweakConfig other) return other.IsDisabled == this.IsDisabled;
            else return false;
        }

        public override int GetHashCode() => new { IsDisabled }.GetHashCode();

        public TweakConfig(bool disabled = false) => _isDisabled = disabled;
    }
}
