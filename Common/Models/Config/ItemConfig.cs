using System.ComponentModel;
using System.Text.Json.Serialization;
using Terraria.ModLoader.Config;

namespace VanillaPlus.Common.Models.Config
{
    public class ItemConfig
    {
        [JsonIgnore]
        public static bool ForceDisableAllItems { get; set; } = false;

        private bool _isSoftDisabled = !ForceDisableAllItems;

        [DefaultValue(false)]
        [Label("Soft Disable (NO LOC)")]
        [Tooltip("Removes crafting recipes/drops from NPCs but allows players to keep using the item (RECOMMENDED) (NO LOC)")]
        [BackgroundColor(0, 127, 0)]
        [ReloadRequired]
        public bool IsSoftDisabled
        {
            get
            {
                if (ForceDisableAllItems)
                    return false;
                
                return _isSoftDisabled;
            }

            set
            {
                if (ForceDisableAllItems)
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

        private bool _isHardDisabled = ForceDisableAllItems;

        [DefaultValue(false)]
        [Label("Hard Disable (NO LOC)")]
        [Tooltip("Removes the item completely, rendering it unusable in-game (use with caution) (NO LOC)")]
        [BackgroundColor(127, 0, 0)]
        [ReloadRequired]
        public bool IsHardDisabled
        {
            get
            {
                if (ForceDisableAllItems)
                    return true;
                
                return _isHardDisabled;
            }

            set
            {
                if (ForceDisableAllItems)
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
