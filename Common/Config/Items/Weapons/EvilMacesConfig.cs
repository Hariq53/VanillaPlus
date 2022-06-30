using Terraria.ModLoader.Config;
using VanillaPlus.Common.Config.Items.Weapons.EvilMaces;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.EvilMaces")]
    public class EvilMacesConfig
    {
        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.FleshMace.Tooltip")]
        public FleshMaceConfig? FleshMace
        {
            get => _fleshMace;
            set => ElementConfigSetter(ref _fleshMace, value);
        }
        FleshMaceConfig? _fleshMace;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.RottenMace.Tooltip")]
        public RottenMaceConfig? RottenMace
        {
            get => _rottenMace;
            set => ElementConfigSetter(ref _rottenMace, value);
        }
        RottenMaceConfig? _rottenMace;

        public override bool Equals(object? obj)
        {
            if (obj is EvilMacesConfig other)
            {
                if (!Equals(FleshMace, other.FleshMace))
                    return false;

                if (!Equals(RottenMace, other.RottenMace))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
