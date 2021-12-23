﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Items.Weapons
{
    public class ThiefsDagger : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().GoblinDropsToggle;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.damage = 31;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.crit = 5;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.knockBack = 3.5f;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
