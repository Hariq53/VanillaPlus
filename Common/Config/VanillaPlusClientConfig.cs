﻿using Microsoft.Xna.Framework;
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
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config
{
    [Label("$Mods.VanillaPlus.Config.ClientSideConfig")]
    public class VanillaPlusClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
    }
}
