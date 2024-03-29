﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Common.Models.ModItems
{
    class SummonStaff : ConfigurableWeapon
    {
        protected virtual int MinionBuffType => 0;

        protected virtual int MinionProjectileType => 0;

        protected override void SetRegularDefaults()
        {
            // GFX
            Item.UseSound = SoundID.Item113;

            // Animation
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;

            // Weapon Specific
            Item.DamageType = DamageClass.Summon;
            Item.shoot = MinionProjectileType;

            // Other
            Item.buffType = MinionBuffType;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }
    }
}
