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
using VanillaPlus.Common.Config.GameplayTweaks;
using VanillaPlus.Common.Models.Config;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config
{
    [SeparatePage]
    [Label("$Mods.VanillaPlus.Config.GameplayTweaks.Label")]
    public class GameplayTweaksConfig
    {
        [JsonIgnore]
        public IEnumerable<TweakConfig?> AllTweaks
        {
            get
            {
                return new TweakConfig?[]
                {
                    FlyingDragonTweak,
                    GladiusTweak,
                    InfluxWaverTweak,
                    TerraBladeTweak
                };
            }
        }

        [Label("$Mods.VanillaPlus.Config.GameplayTweaks.FlyingDragonTweak.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GameplayTweaks.FlyingDragonTweak.Tooltip")]
        [ReloadRequired]
        public FlyingDragonTweakConfig? FlyingDragonTweak
        {
            get => _flyingDragonTweak;
            set => ElementConfigSetter(ref _flyingDragonTweak, value);
        }
        FlyingDragonTweakConfig? _flyingDragonTweak = new();

        [Label("$Mods.VanillaPlus.Config.GameplayTweaks.GladiusTweak.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GameplayTweaks.GladiusTweak.Tooltip")]
        [ReloadRequired]
        public GladiusTweakConfig? GladiusTweak
        {
            get => _gladiusTweak;
            set => ElementConfigSetter(ref _gladiusTweak, value);
        }
        GladiusTweakConfig? _gladiusTweak = new();

        [Label("$Mods.VanillaPlus.Config.GameplayTweaks.TerraBladeTweak.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GameplayTweaks.TerraBladeTweak.Tooltip")]
        [ReloadRequired]
        public TerraBladeTweakConfig? TerraBladeTweak
        {
            get => _terraBladeTweak;
            set => ElementConfigSetter(ref _terraBladeTweak, value);
        }
        TerraBladeTweakConfig? _terraBladeTweak = new();

        [Label("$Mods.VanillaPlus.Config.GameplayTweaks.InfluxWaverTweak.Label")]
        [Tooltip("$Mods.VanillaPlus.Config.GameplayTweaks.InfluxWaverTweak.Tooltip")]
        [ReloadRequired]
        public InfluxWaverTweakConfig? InfluxWaverTweak
        {
            get => _influxWaverTweak;
            set => ElementConfigSetter(ref _influxWaverTweak, value);
        }
        InfluxWaverTweakConfig? _influxWaverTweak = new();

        public override bool Equals(object? obj)
        {
            if (obj is GameplayTweaksConfig other)
            {
                if (!Equals(FlyingDragonTweak, other.FlyingDragonTweak))
                    return false;

                if (!Equals(GladiusTweak, other.GladiusTweak))
                    return false;

                if (!Equals(InfluxWaverTweak, other.InfluxWaverTweak))
                    return false;

                if (!Equals(TerraBladeTweak, other.TerraBladeTweak))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
