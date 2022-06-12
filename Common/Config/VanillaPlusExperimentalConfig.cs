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
using VanillaPlus.Common.Models.Config;

namespace VanillaPlus.Common.Config
{
    public class VanillaPlusExperimentalConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.VanillaPlus.Config.ItemHeader")]

        [Label("Single items (NO LOC)")]
        public ItemsConfig Items { get; set; } = new();

        private bool _itemsDisabled = false;

        [Label("Disable all items (NO LOC)")]
        [ReloadRequired]
        [DefaultValue(false)]
        public bool ItemsDisabled
        {
            get => _itemsDisabled;
            
            set
            {
                _itemsDisabled = ItemConfig.ForceDisableAllItems = value;
            }
        }

        [SeparatePage]
        public class ItemsConfig
        {
            [Header("Weapons (NO LOC)")]
            
            [Label("Configurable Sword (NO LOC)")]
            [ReloadRequired]
            public SwordConfig? Sword { get; set; }

            [Label("Evil Maces (NO LOC)")]
            [ReloadRequired]
            public EvilMacesConfig? EvilMaces { get; set; } = new();
            
            [Label("Enchanted Spear (NO LOC)")]
            [ReloadRequired]
            public EnchantedSpearConfig? EnchantedSpear { get; set; }

            [Label("Christmas Barrage (NO LOC)")]
            [ReloadRequired]
            public ChristmasBarrageConfig? ChristmasBarrage { get; set; }

            [Label("Dualies (NO LOC)")]
            [ReloadRequired]
            public DualiesConfig? Dualies { get; set; }

            [Header("Accessories (NO LOC)")]

            [ReloadRequired]
            public ShinyCharmConfig? ShinyCharm { get; set; }

            // Quick-access properties to access inner classes' elements from outside
            #region Properties
            [JsonIgnore]
            public EvilMacesConfig.FleshMaceConfig? FleshMace => EvilMaces?.FleshMace;

            [JsonIgnore]
            public EvilMacesConfig.FleshMaceConfig? RottenMace => EvilMaces?.RottenMace;
            #endregion

            #region Inner Classes
            // Weapons
            public class SwordConfig : WeaponConfig
            {
                // Override properties to add attributes on top on those already declared in the superclass
                [DefaultValue(30)] // For example: DefaultValue
                public override int Damage { get => base.Damage; set => base.Damage = value; }

                [DefaultValue(11)]
                public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
            }

            public class EvilMacesConfig
            {
                [Label("Flesh Mace (NO LOC)")]
                public FleshMaceConfig? FleshMace;

                public class FleshMaceConfig : WeaponConfig
                {
                    [DefaultValue(25)]
                    public override int Damage { get => base.Damage; set => base.Damage = value; }

                    [DefaultValue(22)]
                    public override int UseTime { get => base.UseTime; set => base.UseTime = value; }

                    [Label("Projectile Sticking Lifetime (NO LOC)")]
                    [ReloadRequired]
                    [DefaultValue(120)]
                    public int StickingLifeTime { get; set; }

                }

                [Header("")]

                [Label("Rotten Mace (NO LOC)")]
                public FleshMaceConfig? RottenMace;

                public class RottenMaceConfig : WeaponConfig
                {
                    [DefaultValue(30)]
                    public override int Damage { get => base.Damage; set => base.Damage = value; }

                    [DefaultValue(22)]
                    public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
                }
            }

            public class EnchantedSpearConfig : WeaponConfig
            {
                [DefaultValue(20)]
                public override int Damage { get => base.Damage; set => base.Damage = value; }

                [DefaultValue(31)]
                public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
            }
            
            public class ChristmasBarrageConfig : WeaponConfig
            {
                [DefaultValue(125)]
                public override int Damage { get => base.Damage; set => base.Damage = value; }

                [DefaultValue(17)]
                public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
            }

            public class DualiesConfig : WeaponConfig
            {
                [DefaultValue(125)]
                public override int Damage { get => base.Damage; set => base.Damage = value; }

                [DefaultValue(17)]
                public override int UseTime { get => base.UseTime; set => base.UseTime = value; }
            }

            // Accessories
            public class ShinyCharmConfig : ItemConfig
            {
                
            }
            #endregion
        }
    }
}
