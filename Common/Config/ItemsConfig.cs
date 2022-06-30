using Newtonsoft.Json;
using System.Collections.Generic;
using Terraria.ModLoader.Config;
using VanillaPlus.Common.Config.Items.Accessories;
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
     * 3. Add the Item to AllItems
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
        public IEnumerable<ItemConfig?> AllItems
        {
            get
            {
                return new ItemConfig?[]
                {
                    Dualies,
                    FleshMace,
                    RottenMace,
                    EnchantedSpear,
                    ChristmasBarrage,
                    EyeballOnAStick,
                    FangOfCthulhu,
                    Tear,
                    TheOcularMenace,
                    GoblinsBlade,
                    ThiefsDagger,
                    WarriorsMallet,
                    SkullOfBoom,
                    SkeletronsFinger,
                    OlMansCurse,
                    ShinyCharm
                };
            }
        }

        [Header("$Mods.VanillaPlus.Config.Items.Weapons.Header")]

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.Dualies.Tooltip")]
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

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.EnchantedSpear.Tooltip")]
        public EnchantedSpearConfig? EnchantedSpear
        {
            get => _enchantedSpear;
            set => ElementConfigSetter(ref _enchantedSpear, value);
        }
        EnchantedSpearConfig? _enchantedSpear;

        [Tooltip("$Mods.VanillaPlus.Config.Items.Weapons.ChristmasBarrage.Tooltip")]
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

        public GoblinsDropsConfig? GoblinsDrops
        {
            get;
            set;
        } = new();

        public SkeletronDropsConfig? SkeletronDrops
        {
            get;
            set;
        } = new();

        [Header("$Mods.VanillaPlus.Config.Items.Accessories.Header")]
        public ShinyCharmConfig? ShinyCharm
        {
            get => _shinyCharm;
            set => ElementConfigSetter(ref _shinyCharm, value);
        }
        ShinyCharmConfig? _shinyCharm;

        // Evil Maces
        [JsonIgnore]
        public FleshMaceConfig? FleshMace => EvilMaces?.FleshMace;

        [JsonIgnore]
        public RottenMaceConfig? RottenMace => EvilMaces?.RottenMace;


        // EOC Drops
        [JsonIgnore]
        public EyeballOnAStickConfig? EyeballOnAStick => EOCDrops?.EyeballOnAStick;

        [JsonIgnore]
        public FangOfCthulhuConfig? FangOfCthulhu => EOCDrops?.FangOfCthulhu;

        [JsonIgnore]
        public TearConfig? Tear => EOCDrops?.Tear;

        [JsonIgnore]
        public TheOcularMenaceConfig? TheOcularMenace => EOCDrops?.TheOcularMenace;

        // Goblins Drops
        [JsonIgnore]
        public GoblinsBladeConfig? GoblinsBlade => GoblinsDrops?.GoblinsBlade;

        [JsonIgnore]
        public ThiefsDaggerConfig? ThiefsDagger => GoblinsDrops?.ThiefsDagger;

        [JsonIgnore]
        public WarriorsMalletConfig? WarriorsMallet => GoblinsDrops?.WarriorsMallet;

        // Skeletron Drops
        [JsonIgnore]
        public SkullOfBoomConfig? SkullOfBoom => SkeletronDrops?.SkullOfBoom;

        [JsonIgnore]
        public SkeletronsFingerConfig? SkeletronsFinger => SkeletronDrops?.SkeletronsFinger;

        [JsonIgnore]
        public OlMansCurseConfig? OlMansCurse => SkeletronDrops?.OlMansCurse;

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

                if (!Equals(GoblinsDrops, other.GoblinsDrops))
                    return false;

                if (!Equals(SkeletronDrops, other.SkeletronDrops))
                    return false;

                if (!Equals(ShinyCharm, other.ShinyCharm))
                    return false;

                return true;
            }
            else
                return false;
        }
    }
}
