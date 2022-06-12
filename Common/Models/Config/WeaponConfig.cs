using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
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

        public WeaponConfig()
            : this(false, false)
        { }

        public WeaponConfig(bool softDisabled = false, bool hardDisabled = false)
            : base(softDisabled, hardDisabled)
        { }
    }
}
