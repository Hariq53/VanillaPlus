using Terraria.ModLoader;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Models.GlobalProjectiles
{
    public class ConfigurableGlobalProjectile : GlobalProjectile
    {
        protected virtual TweakConfig? Config => new(disabled: true);

        public override sealed bool IsLoadingEnabled(Mod mod)
        {
            return !Config?.IsDisabled ?? true;
        }
    }
}