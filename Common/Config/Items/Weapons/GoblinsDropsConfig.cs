using Terraria.ModLoader.Config;
using VanillaPlus.Common.Config.Items.Weapons.EOCDrops;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.GoblinsDrops")]
    public class GoblinsDropsConfig
    {
        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.GoblinsBlade.Tooltip")]
        public GoblinsBladeConfig? GoblinsBlade
        {
            get => _goblinsBlade;
            set => ElementConfigSetter(ref _goblinsBlade, value);
        }
        GoblinsBladeConfig? _goblinsBlade;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.ThiefsDagger.Tooltip")]
        public ThiefsDaggerConfig? ThiefsDagger
        {
            get => _thiefsDagger;
            set => ElementConfigSetter(ref _thiefsDagger, value);
        }
        ThiefsDaggerConfig? _thiefsDagger;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.WarriorsMallet.Tooltip")]
        public WarriorsMalletConfig? WarriorsMallet
        {
            get => _warriorsMallet;
            set => ElementConfigSetter(ref _warriorsMallet, value);
        }
        WarriorsMalletConfig? _warriorsMallet;

        public override bool Equals(object? obj)
        {
            if (obj is GoblinsDropsConfig other)
            {
                if (!Equals(GoblinsBlade, other.GoblinsBlade))
                    return false;

                if (!Equals(ThiefsDagger, other.ThiefsDagger))
                    return false;

                if (!Equals(WarriorsMallet, other.WarriorsMallet))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
