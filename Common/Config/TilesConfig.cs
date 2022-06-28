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
using VanillaPlus.Common.Config.Items;
using VanillaPlus.Common.Config.Items.Accessories;
using VanillaPlus.Common.Config.Items.Weapons;
using VanillaPlus.Common.Config.Items.Weapons.EOCDrops;
using VanillaPlus.Common.Config.Items.Weapons.EvilMaces;
using VanillaPlus.Common.Config.Tiles;
using VanillaPlus.Common.Models.Config;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config
{
    [SeparatePage]
    [Label("$Mods.VanillaPlus.Config.Tiles.Label")]
    public class TilesConfig
    {
        [JsonIgnore]
        public IEnumerable<TileConfig?> AllTiles
        {
            get
            {
                return new TileConfig?[]
                {
                    LifeFruitLantern
                };
            }
        }

        public LifeFruitLanternConfig? LifeFruitLantern
        {
            get => _lifeFruitLantern;
            set => ElementConfigSetter(ref _lifeFruitLantern, value);
        }
        LifeFruitLanternConfig? _lifeFruitLantern;

        public override bool Equals(object? obj)
        {
            if (obj is TilesConfig other)
            {
                if (!Equals(LifeFruitLantern, other.LifeFruitLantern))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
