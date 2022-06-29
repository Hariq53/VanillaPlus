using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    [Label("$Mods.VanillaPlus.Config.TileConfig.Label")]
    public class TileConfig : SHConfig
    {
        [Label("$Mods.VanillaPlus.Config.TileConfig.SoftDisable.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.TileConfig.SoftDisable.Tooltip")]
        public override bool IsSoftDisabled { get => base.IsSoftDisabled; set => base.IsSoftDisabled = value; }


        [Label("$Mods.VanillaPlus.Config.TileConfig.HardDisable.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.TileConfig.HardDisable.Tooltip")]
        public override bool IsHardDisabled { get => base.IsHardDisabled; set => base.IsHardDisabled = value; }

        public static explicit operator ItemConfig?(TileConfig? tileConfig)
        {
            if (tileConfig is not null)
                return new(tileConfig.IsSoftDisabled, tileConfig.IsHardDisabled);
            return null;
        }

        public TileConfig()
            : base()
        { }

        public TileConfig(bool softDisabled = false, bool hardDisabled = false)
            : base(softDisabled, hardDisabled)
        { }
    }
}
