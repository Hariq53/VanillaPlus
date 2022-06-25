﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.ModItems;
using VanillaPlus.Content.Buffs;
using VanillaPlus.Content.Projectiles.Minions;

namespace VanillaPlus.Content.Items.Weapons
{
    class OlMansCurse : SummonStaff
    {
        public override bool ShouldLoad() => true;

        protected override int MinionBuff => ModContent.BuffType<CursedSkullMinionBuff>();

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            base.SetRegularDefaults();

            // GFX
            Item.width = 38;
            Item.height = 38;

            // Animation
            Item.useAnimation = 36;
            Item.useTime = 36;

            // Weapon Specific
            Item.damage = 22;
            Item.knockBack = 8f;
            Item.shoot = ModContent.ProjectileType<CursedSkullMinion>();
            Item.mana = 14;

            // Other
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
        }
    }
}
