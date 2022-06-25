using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
using VanillaPlus.Common.Config.Global;
using VanillaPlus.Common.Config.Items.Weapons;
using VanillaPlus.Common.Config.Items.Weapons.EvilMaces;
using VanillaPlus.Common.Models.Config;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.EvilMaces")]
    public class EvilMacesConfig
    {
        public FleshMaceConfig? FleshMace
        {
            get => _fleshMace;
            set => ElementConfigSetter(ref _fleshMace, value);
        }
        FleshMaceConfig? _fleshMace;

        public RottenMaceConfig? RottenMace
        {
            get => _rottenMace;
            set => ElementConfigSetter(ref _rottenMace, value);
        }
        RottenMaceConfig? _rottenMace;

        public override bool Equals(object? obj)
        {
            if (obj is EvilMacesConfig other)
            {
                if (!Equals(FleshMace, other.FleshMace))
                    return false;

                if (!Equals(RottenMace, other.RottenMace))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
