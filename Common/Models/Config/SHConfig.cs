using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    [Label("$Mods.VanillaPlus.Config.ElementConfig.Label")]
    public class SHConfig : ElementConfig
    {
        public override bool IsEnabled()
        {
            if (IsSoftDisabled) return false;
            if (IsHardDisabled) return false;
            return true;
        }

        private bool _isSoftDisabled;

        [Label("$Mods.VanillaPlus.Config.ElementConfig.SoftDisable.Label")]
        [BackgroundColor(0, 127, 0)]
        [ReloadRequired]
        public virtual bool IsSoftDisabled
        {
            get
            {
                if (SuperConfig is SHConfig superConfig && superConfig.CanOverride(this))
                    return superConfig.IsSoftDisabled;

                return _isSoftDisabled;
            }

            set
            {
                if (SuperConfig is SHConfig superConfig && superConfig.CanOverride(this))
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
                if (SuperConfig is SHConfig superConfig && superConfig.CanOverride(this))
                    return superConfig.IsHardDisabled;

                return _isHardDisabled;
            }

            set
            {
                if (SuperConfig is SHConfig superConfig && superConfig.CanOverride(this))
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
            if (obj is SHConfig other)
                return other.IsSoftDisabled == this.IsSoftDisabled && other.IsHardDisabled == this.IsHardDisabled;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return new { IsSoftDisabled, IsHardDisabled }.GetHashCode();
        }

        public SHConfig(bool softDisabled = false, bool hardDisabled = false)
        {
            _isSoftDisabled = softDisabled;
            _isHardDisabled = hardDisabled;
        }
    }
}
