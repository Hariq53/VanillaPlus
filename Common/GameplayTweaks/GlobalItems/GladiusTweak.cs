using Terraria;
using Terraria.ID;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.GameplayTweaks.GlobalItems
{
    class GladiusTweak : ConfigurableGlobalItem
    {
        protected override TweakConfig? Config => VanillaPlus.ServerSideConfig?.GameplayTweaks.GladiusTweak;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.Gladius;
        }

        public override void SetDefaults(Item item)
        {
            item.autoReuse = true;
            item.reuseDelay = 0;
        }
    }
}
