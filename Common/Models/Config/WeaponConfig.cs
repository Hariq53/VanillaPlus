using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    public class WeaponConfig : ItemConfig
    {

        [Label("$Mods.VanillaPlus.Config.WeaponConfig.Damage.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.WeaponConfig.Damage.Tooltip")]
        [Range(0, int.MaxValue)]
        [DefaultValue(1)]
        [ReloadRequired]
        public virtual int Damage
        {
            get;
            set;
        }

        [Label("$Mods.VanillaPlus.Config.WeaponConfig.UseTime.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.WeaponConfig.UseTime.Tooltip")]
        [Range(0, int.MaxValue)]
        [DefaultValue(1)]
        [ReloadRequired]
        public virtual int UseTime
        {
            get;
            set;
        }

        public override bool Equals(object? obj)
        {
            if (!base.Equals(obj))
                return false;

            if (obj is WeaponConfig other)
                return other.Damage == this.Damage && other.UseTime == this.UseTime;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return new { IsSoftDisabled, IsHardDisabled, Damage, UseTime }.GetHashCode();
        }
        public WeaponConfig()
            : base()
        { }

        public WeaponConfig(bool softDisabled = false, bool hardDisabled = false)
            : base(softDisabled, hardDisabled)
        { }
    }
}
