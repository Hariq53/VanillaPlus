using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.GameplayTweaks.GlobalItems
{
    public abstract class ConfigurableGlobalItem : GlobalItem
    {
        protected virtual TweakConfig? Config => new(disabled: true);

        public override sealed bool IsLoadingEnabled(Mod mod)
        {
            return !Config?.IsDisabled ?? true;
        }
    }
}
