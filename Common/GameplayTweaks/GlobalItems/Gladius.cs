using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Common.GameplayTweaks.GlobalItems
{
    class Gladius : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true; //.GladiusTweakToggle;
        }

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
