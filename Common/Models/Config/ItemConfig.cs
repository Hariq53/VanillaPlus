using System.ComponentModel;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    public class ItemConfig
    {
        private bool _isSoftDisabled;

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("Soft Disable")]
        [Tooltip("Removes crafting recipes/drops from NPCs but allows players to keep using the item (RECOMMENDED)")]
        [BackgroundColor(0, 127, 0)]
        public bool IsSoftDisabled
        {
            get => _isSoftDisabled;

            set
            {
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

        [ReloadRequired]
        [DefaultValue(false)]
        [Label("Hard Disable")]
        [Tooltip("Removes the item completely, rendering it unusable in-game (use with caution)")]
        [BackgroundColor(127, 0, 0)]
        public bool IsHardDisabled
        {
            get => _isHardDisabled;

            set
            {
                if (value)
                {
                    _isSoftDisabled = false;
                    _isHardDisabled = true;
                }
                else
                    _isHardDisabled = false;
            }
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
