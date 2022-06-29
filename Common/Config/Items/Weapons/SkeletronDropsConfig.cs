using Terraria.ModLoader.Config;
using VanillaPlus.Common.Config.Items.Weapons.EOCDrops;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.SkeletronDrops")]
    public class SkeletronDropsConfig
    {
        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.SkullOfBoom.Tooltip")]
        public SkullOfBoomConfig? SkullOfBoom
        {
            get => _skullOfBoom;
            set => ElementConfigSetter(ref _skullOfBoom, value);
        }
        SkullOfBoomConfig? _skullOfBoom;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.SkeletronsFinger.Tooltip")]
        public SkeletronsFingerConfig? SkeletronsFinger
        {
            get => _skeletronsFinger;
            set => ElementConfigSetter(ref _skeletronsFinger, value);
        }
        SkeletronsFingerConfig? _skeletronsFinger;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.OlMansCurse.Tooltip")]
        public OlMansCurseConfig? OlMansCurse
        {
            get => _olMansCurse;
            set => ElementConfigSetter(ref _olMansCurse, value);
        }
        OlMansCurseConfig? _olMansCurse;

        public override bool Equals(object? obj)
        {
            if (obj is SkeletronDropsConfig other)
            {
                if (!Equals(SkullOfBoom, other.SkullOfBoom))
                    return false;

                if (!Equals(SkeletronsFinger, other.SkeletronsFinger))
                    return false;

                if (!Equals(OlMansCurse, other.OlMansCurse))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
