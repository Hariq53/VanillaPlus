﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Presets.SummonWeapon;
using VanillaPlus.Content.Buffs;
using VanillaPlus.Content.Projectiles.Minions;

namespace VanillaPlus.Content.Items.Weapons
{
    class OlMansCurse : SummonStaff
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().SkeletronDropsToggle;
        }

        protected override int MinionBuff => ModContent.BuffType<CursedSkullMinionBuff>();

        public override void SetDefaults()
        {
            base.SetDefaults();

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
