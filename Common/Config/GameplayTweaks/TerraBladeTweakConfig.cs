using System;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.GameplayTweaks
{
    public class TerraBladeTweakConfig : TweakConfig
    {
        [Label("$Mods.VanillaPlus.Config.GameplayTweaks.TerraBladeTweak.LifeTime.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GameplayTweaks.TerraBladeTweak.LifeTime.Tooltip")]
        [Range(0, int.MaxValue)]
        [ReloadRequired]
        public virtual int LifeTime
        {
            get;
            set;
        } = 65;

        public override bool Equals(object? obj)
        {
            if (!base.Equals(obj))
                return false;

            if (obj is TerraBladeTweakConfig other) return LifeTime == other.LifeTime;
            else return false;
        }

        public override int GetHashCode() => new { IsDisabled, LifeTime }.GetHashCode();
    }
}
