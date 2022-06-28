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
using VanillaPlus.Common.Config.Items.Weapons.EOCDrops;
using VanillaPlus.Common.Config.Items.Weapons.EvilMaces;
using VanillaPlus.Common.Models.Config;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config.Items.Weapons
{
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.SkeletronDrops")]
    public class SkeletronDropsConfig
    {
        public SkullOfBoomConfig? SkullOfBoom
        {
            get => _skullOfBoom;
            set => ElementConfigSetter(ref _skullOfBoom, value);
        }
        SkullOfBoomConfig? _skullOfBoom;

        public SkeletronsFingerConfig? SkeletronsFinger
        {
            get => _skeletronsFinger;
            set => ElementConfigSetter(ref _skeletronsFinger, value);
        }
        SkeletronsFingerConfig? _skeletronsFinger;

        public OlMansCurseConfig? OlMansCurse
        {
            get => _olMansCurse;
            set => ElementConfigSetter(ref _olMansCurse, value);
        }
        OlMansCurseConfig? _olMansCurse;

        public override bool Equals(object? obj)
        {
            if (obj is SkeletronDropsConfig other)
            {
                if (!Equals(SkullOfBoom, other.SkullOfBoom))
                    return false;

                if (!Equals(SkeletronsFinger, other.SkeletronsFinger))
                    return false;

                if (!Equals(OlMansCurse, other.OlMansCurse))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
