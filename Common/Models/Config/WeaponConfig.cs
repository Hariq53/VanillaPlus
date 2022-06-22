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
        [Label("Damage (NO LOC)")]
        [ReloadRequired]
        public virtual int Damage { get; set; }

        [Label("Use Time (NO LOC)")]
        [ReloadRequired]
        public virtual int UseTime { get; set; }

        public override void Assign(ItemConfig itemConfig)
        {
            base.Assign(itemConfig);
        }

        public void Assign(WeaponConfig itemConfig)
        {
            Assign((ItemConfig)itemConfig);
            Damage = itemConfig.Damage;
            UseTime = itemConfig.UseTime;
        }

        public WeaponConfig()
            : base()
        { }
        
        public WeaponConfig(bool softDisabled = false, bool hardDisabled = false)
            : base(softDisabled, hardDisabled)
        { }
    }
}
