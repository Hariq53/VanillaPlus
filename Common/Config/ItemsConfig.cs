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
using VanillaPlus.Common.Config.Global;
using VanillaPlus.Common.Config.Items;
using VanillaPlus.Common.Config.Items.Weapons;
using VanillaPlus.Common.Config.Items.Weapons.EOCDrops;
using VanillaPlus.Common.Config.Items.Weapons.EvilMaces;
using VanillaPlus.Common.Models.Config;

using static VanillaPlus.Common.Config.VanillaPlusServerConfig;

namespace VanillaPlus.Common.Config
{
    /*
     * SETUP GUIDE FOR CONFIGURABLE ITEM:
     * 1. Copy this snippet (replace ConfigItem with the name of the item):
     * 
     * [Header("$Mods.VanillaPlus.ItemName.ConfigItem")]
     * public ConfigItemConfig? ConfigItem
     * {
     *     get => _configItem;
     *     set => ItemConfigSetter(ref _configItem, value);
     * }
     *
     * ConfigItemConfig? _configItem;
     *  
     * 2. Update the Equals method (to force reload every time the config of the item is changed)
     *  
     * 3. Add the Item to AllSubs
     *  
     * 4. Change the class of the actual ModItem to ConfigurableItem (or ConfigurableWeapon, etc.)
     *    and override the Config to set it to the instance of ItemConfig you just created, then
     *    update the SetDefaults, AddRecipes, etc. to their configurable counterparts (for example:
     *    SetDefaults -> SetRegularDefaults & SetConfigurableDefaults, AddRecipes -> AddRecipesWithConfig)
     */
    [SeparatePage]
    [Label("$Mods.VanillaPlus.Config.Items.Label")]
    public class ItemsConfig
    {
        [JsonIgnore]
        public Func<ItemConfig?>[] AllSubs => new Func<ItemConfig?>[]
        {
            () => Dualies,
            () => FleshMace,
            () => RottenMace,
            () => EnchantedSpear,
            () => ChristmasBarrage,
            () => EyeballOnAStick,
            () => FangOfCthulhu,
            () => Tear
        };

        [Header("$Mods.VanillaPlus.Config.Items.Weapons.Header")]
        
        public DualiesConfig? Dualies
        {
            get => _dualies;
            set => ElementConfigSetter(ref _dualies, value);
        }
        DualiesConfig? _dualies;

        public EvilMacesConfig? EvilMaces
        {
            get;
            set;
        } = new();

        public EnchantedSpearConfig? EnchantedSpear
        {
            get => _enchantedSpear;
            set => ElementConfigSetter(ref _enchantedSpear, value);
        }
        EnchantedSpearConfig? _enchantedSpear;

        public ChristmasBarrageConfig? ChristmasBarrage
        {
            get => _christmasBarrage;
            set => ElementConfigSetter(ref _christmasBarrage, value);
        }
        ChristmasBarrageConfig? _christmasBarrage;

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
            if (obj is ItemsConfig other)
            {
                if (!Equals(Dualies, other.Dualies))
                    return false;

                if (!Equals(EvilMaces, other.EvilMaces))
                    return false;

                if (!Equals(EnchantedSpear, other.EnchantedSpear))
                    return false;

                if (!Equals(ChristmasBarrage, other.ChristmasBarrage))
                    return false;

                if (!Equals(EOCDrops, other.EOCDrops))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
