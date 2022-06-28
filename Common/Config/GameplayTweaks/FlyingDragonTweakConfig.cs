using System.ComponentModel;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config.GameplayTweaks
{
    public class FlyingDragonTweakConfig : TweakConfig
    {
        [Label("$Mods.VanillaPlus.Config.GameplayTweaks.FlyingDragonTweak.Damage.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GameplayTweaks.FlyingDragonTweak.Damage.Tooltip")]
        [Range(0, int.MaxValue)]
        [ReloadRequired]
        public virtual int Damage
        {
            get;
            set;
        } = 180;

        public override bool Equals(object? obj)
        {
            if (!base.Equals(obj))
                return false;

            if (obj is FlyingDragonTweakConfig other) return Damage == other.Damage;
            else return false;
        }

        public override int GetHashCode() => new { IsDisabled, Damage }.GetHashCode();
    }
}
