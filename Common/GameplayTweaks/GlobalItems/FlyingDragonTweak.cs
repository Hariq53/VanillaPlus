using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Config.GameplayTweaks;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.GameplayTweaks.GlobalItems
{
    class FlyingDragonTweak : ConfigurableGlobalItem
    {
        protected override TweakConfig? Config => VanillaPlus.ServerSideConfig?.GameplayTweaks.FlyingDragonTweak;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ItemID.DD2SquireBetsySword;
        }

        public override void SetDefaults(Item item)
        {
            item.damage = 200;
            if (Config is FlyingDragonTweakConfig config)
                item.damage = config.Damage;
        }
    }
}
