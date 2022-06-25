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

namespace VanillaPlus.Common.Config.Items
{
    [SeparatePage]
    [Label("$Mods.VanillaPlus.Config.Items.Weapons.Label")]
    public class WeaponsConfig
    {
        [JsonIgnore]
        public Func<ItemConfig?>[] AllSubs => new Func<ItemConfig?>[]
        {
            () => Dualies,
            () => FleshMace,
            () => RottenMace
        };

        [Header("$Mods.VanillaPlus.ItemName.Dualies")]
        public DualiesConfig? Dualies
        {
            get => _dualies;
            set => ElementConfigSetter(ref _dualies, value);
        }

        DualiesConfig? _dualies;

        [Header("$Mods.VanillaPlus.Config.Items.Weapons.EvilMaces")]
        public EvilMacesConfig? EvilMaces
        {
            get;
            set;
        } = new();

        [Header("$Mods.VanillaPlus.ItemName.EnchantedSpear")]
        public EnchantedSpearConfig? EnchantedSpear
        {
            get => _enchantedSpear;
            set => ElementConfigSetter(ref _enchantedSpear, value);
        }

        EnchantedSpearConfig? _enchantedSpear;

        [Header("$Mods.VanillaPlus.ItemName.ChristmasBarrage")]
        public ChristmasBarrageConfig? ChristmasBarrage
        {
            get => _christmasBarrage;
            set => ElementConfigSetter(ref _christmasBarrage, value);
        }

        ChristmasBarrageConfig? _christmasBarrage;

        [Header("$Mods.VanillaPlus.Config.Items.Weapons.EOCDrops")]
        public EOCDropsConfig? EOCDrops
        {
            get;
            set;
        } = new();

        [JsonIgnore]
        public FleshMaceConfig? FleshMace => EvilMaces?.FleshMace;

        [JsonIgnore]
        public RottenMaceConfig? RottenMace => EvilMaces?.RottenMace;

        [JsonIgnore]
        public EyeballOnAStickConfig? EyeballOnAStick => EOCDrops?.EyeballOnAStick;

        [JsonIgnore]
        public FangOfCthulhuConfig? FangOfCthulhu => EOCDrops?.FangOfCthulhu;

        [JsonIgnore]
        public TearConfig? Tear => EOCDrops?.Tear;

        public override bool Equals(object? obj)
        {
            if (obj is WeaponsConfig other)
            {
                if (!Equals(Dualies, other.Dualies))
                    return false;

                if (!Equals(EvilMaces, other.EvilMaces))
                    return false;

                if (!Equals(ChristmasBarrage, other.ChristmasBarrage))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
