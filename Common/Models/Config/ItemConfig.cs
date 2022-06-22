using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    public class ItemConfig
    {
        internal Func<ItemConfig?>? SuperConfigHandler { get; set; }

        [JsonIgnore]
        internal ItemConfig? SuperConfig => (SuperConfigHandler is not null ? SuperConfigHandler() : null);

        public virtual void Assign(ItemConfig itemConfig)
        {
            _isSoftDisabled = itemConfig._isSoftDisabled;
            _isHardDisabled = itemConfig._isHardDisabled;
        }

        private bool _isSoftDisabled;

        [DefaultValue(false)]
        [Label("$Mods.VanillaPlus.Config.Items.SoftDisable.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.Items.SoftDisable.Tooltip")]
        [BackgroundColor(0, 127, 0)]
        [ReloadRequired]
        public virtual bool IsSoftDisabled
        {
            get
            {
                if (SuperConfig is not null)
                {
                    if (SuperConfig.IsHardDisabled)
                        return false;

                    if (SuperConfig.IsSoftDisabled)
                        return true;
                }

                return _isSoftDisabled;
            }

            set
            {
                if (SuperConfig is not null)
                    if (SuperConfig.IsSoftDisabled || SuperConfig.IsHardDisabled)
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

        [DefaultValue(false)]
        [Label("$Mods.VanillaPlus.Config.Items.HardDisable.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.Items.HardDisable.Tooltip")]
        [BackgroundColor(127, 0, 0)]
        [ReloadRequired]
        public virtual bool IsHardDisabled
        {
            get
            {
                if (SuperConfig is not null)
                {
                    if (SuperConfig.IsHardDisabled)
                        return true;

                    if (SuperConfig.IsSoftDisabled)
                        return false;
                }

                return _isHardDisabled;
            }

            set
            {
                if (SuperConfig is not null)
                    if (SuperConfig.IsSoftDisabled || SuperConfig.IsHardDisabled)
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
            return new { _isSoftDisabled, _isHardDisabled }.GetHashCode();
        }

        public ItemConfig()
            : this(false, false)
        { }
        
        public ItemConfig(bool softDisabled = false, bool hardDisabled = false)
        {
            _isSoftDisabled = softDisabled;
            _isHardDisabled = hardDisabled;
        }
    }
}
