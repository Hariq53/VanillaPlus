using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    [Label("$Mods.VanillaPlus.Config.ElementConfig.Label")]
    public class ElementConfig
    {
        internal ElementConfig? SuperConfig { get; set; }

        private bool _isSoftDisabled;

        [Label("$Mods.VanillaPlus.Config.ElementConfig.SoftDisable.Label")]
        [BackgroundColor(0, 127, 0)]
        [ReloadRequired]
        public virtual bool IsSoftDisabled
        {
            get
            {
                if (SuperConfig is not null && SuperConfig.CanOverrideSubConfig())
                    return SuperConfig.IsSoftDisabled;

                return _isSoftDisabled;
            }

            set
            {
                if (SuperConfig is not null && SuperConfig.CanOverrideSubConfig())
                    return;

                if (value)
                {
                    _isHardDisabled = false;
                    _isSoftDisabled = true;
                }
                else
                    _isSoftDisabled = false;
            }
        }

        private bool _isHardDisabled;

        [Label("$Mods.VanillaPlus.Config.ElementConfig.HardDisable.Label")]
        [BackgroundColor(127, 0, 0)]
        [ReloadRequired]
        public virtual bool IsHardDisabled
        {
            get
            {
                if (SuperConfig is not null && SuperConfig.CanOverrideSubConfig())
                    return SuperConfig.IsHardDisabled;

                return _isHardDisabled;
            }

            set
            {
                if (SuperConfig is not null && SuperConfig.CanOverrideSubConfig())
                    return;

                if (value)
                {
                    _isSoftDisabled = false;
                    _isHardDisabled = true;
                }
                else
                    _isHardDisabled = false;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is ItemConfig other)
                return other.IsSoftDisabled == this.IsSoftDisabled && other.IsHardDisabled == this.IsHardDisabled;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return new { IsSoftDisabled, IsHardDisabled }.GetHashCode();
        }

        public virtual bool CanOverrideSubConfig()
        {
            return IsSoftDisabled || IsHardDisabled;
        }

        public ElementConfig()
            : this(false, false)
        { }

        public ElementConfig(bool softDisabled = false, bool hardDisabled = false)
        {
            _isSoftDisabled = softDisabled;
            _isHardDisabled = hardDisabled;
        }
    }
}
