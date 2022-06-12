using Microsoft.Xna.Framework;
using System;
using System.ComponentModel;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;
using static VanillaPlus.Common.Config.VanillaPlusExperimentalConfig;

namespace VanillaPlus.Content.Items.Weapons
{
    class ConfigurableSword : ConfigurableWeapon
    {
        protected override WeaponConfig Config => VanillaPlus.ExperimentalConfig.Items.Sword;

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.width = Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;

            base.SetDefaults();
        }
    }
}
