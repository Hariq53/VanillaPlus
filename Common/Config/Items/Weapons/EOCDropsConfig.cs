using Terraria.ModLoader.Config;
using VanillaPlus.Common.Config.Items.Weapons.EOCDrops;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.EOCDrops")]
    public class EOCDropsConfig
    {
        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.EyeballOnAStick.Tooltip")]
        public EyeballOnAStickConfig? EyeballOnAStick
        {
            get => _eyeballOnAStick;
            set => ElementConfigSetter(ref _eyeballOnAStick, value);
        }
        EyeballOnAStickConfig? _eyeballOnAStick;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.FangOfCthulhu.Tooltip")]
        public FangOfCthulhuConfig? FangOfCthulhu
        {
            get => _fangOfCthulhu;
            set => ElementConfigSetter(ref _fangOfCthulhu, value);
        }
        FangOfCthulhuConfig? _fangOfCthulhu;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.Tear.Tooltip")]
        public TearConfig? Tear
        {
            get => _tear;
            set => ElementConfigSetter(ref _tear, value);
        }
        TearConfig? _tear;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.TheOcularMenace.Tooltip")]
        public TheOcularMenaceConfig? TheOcularMenace
        {
            get => _theOcularMenace;
            set => ElementConfigSetter(ref _theOcularMenace, value);
        }
        TheOcularMenaceConfig? _theOcularMenace;

        public override bool Equals(object? obj)
        {
            if (obj is EOCDropsConfig other)
            {
                if (!Equals(EyeballOnAStick, other.EyeballOnAStick))
                    return false;

                if (!Equals(FangOfCthulhu, other.FangOfCthulhu))
                    return false;

                if (!Equals(Tear, other.Tear))
                    return false;

                if (!Equals(TheOcularMenace, other.TheOcularMenace))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
