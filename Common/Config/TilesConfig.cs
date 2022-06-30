using Newtonsoft.Json;
using System.Collections.Generic;
using Terraria.ModLoader.Config;
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
