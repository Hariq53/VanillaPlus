using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    public class WeaponConfig : ItemConfig
    {
        private const int DEFAULT_DAMAGE = 0;

        private int _damage = DEFAULT_DAMAGE;

        protected virtual int DefaultDamage => 1;

        [Label("$Mods.VanillaPlus.Config.WeaponConfig.Damage.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.WeaponConfig.Damage.Tooltip")]
        [ReloadRequired]
        public virtual int Damage
        {
            get
            {
                if (_damage == DEFAULT_DAMAGE)
                    _damage = DefaultDamage;
                return _damage;
            }
            set => _damage = value;
        }

        private const int DEFAULT_USETIME = 0;

        private int _useTime = DEFAULT_USETIME;

        protected virtual int DefaultUseTime => 1;

        [Label("$Mods.VanillaPlus.Config.WeaponConfig.UseTime.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.WeaponConfig.UseTime.Tooltip")]
        [ReloadRequired]
        public virtual int UseTime
        {
            get
            {
                if (_useTime == DEFAULT_USETIME)
                    _useTime = DefaultUseTime;
                return _useTime;
            }
            set => _useTime = value;
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
