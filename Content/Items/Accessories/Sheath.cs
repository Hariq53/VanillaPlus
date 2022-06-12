using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;

namespace VanillaPlus.Content.Items.Accessories
{
    internal class Sheath : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 26;
            Item.height = 26;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<SheathHandlerPlayer>().sheath = true;
            player.meleeScaleGlove = true;
            player.dashType = 1;
        }

        public static bool IsTrueMelee(Item item)
        {
            return item.DamageType == DamageClass.Melee && !item.noMelee;
        }

        class SheathHandlerPlayer : ModPlayer
        {
            public bool sheath = false;
            public bool sheathDash = false;
            public int sheathCooldown = 0;

            public override void PreUpdateBuffs()
            {
                sheath = false;
            }

            public void HandleSheathDash()
            {
                if (Player.itemAnimation > 0 && sheathCooldown <= 0)
                    sheathDash = true;

                if (sheathDash)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Dust obj = Main.dust[Dust.NewDust(Player.position, Player.width, Player.height, DustID.SteampunkSteam, newColor: Color.Red)];
                        obj.noLight = true;
                        obj.noGravity = true;
                        obj.velocity *= 0.5f;
                    }
                }
            }

            public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
            {
                return !sheathDash;
            }

            public override bool CanBeHitByProjectile(Projectile proj)
            {
                return !sheathDash;
            }

            public override void UpdateLifeRegen()
            {

                if (sheath)
                {
                    //Main.NewText($"sheathDash: {sheathDash}, sheathCooldown: {sheathCooldown}");

                    if (sheathCooldown > 0)
                    {
                        sheathCooldown--;
                        if (sheathCooldown <= 0)
                            ItemUtilities.EmitMaxManaEffect(Player);
                    }
                    else
                        sheathCooldown = 0;

                    if (IsTrueMelee(Player.inventory[Player.selectedItem]))
                    {

                        // If player is dashing
                        if (Player.dashDelay < 0)
                            HandleSheathDash();
                        else if (sheathDash)
                        {
                            sheathCooldown = 120;
                            sheathDash = false;
                        }
                    }
                }
                else
                {
                    sheathDash = false;
                    sheathCooldown = 0;
                }
            }

        }

        class SheathHandlerItem : GlobalItem
        {
            public override bool InstancePerEntity => true;

            private static bool HasSheath(Player player)
            {
                return player.GetModPlayer<SheathHandlerPlayer>().sheath;
            }

            public override GlobalItem Clone(Item item, Item itemClone)
            {
                return base.Clone(item, itemClone);
            }

            public override bool AppliesToEntity(Item entity, bool lateInstantiation)
            {
                return IsTrueMelee(entity);
            }

            public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
            {
                if (HasSheath(player))
                {
                    switch (item.type)
                    {
                        case ItemID.TerraBlade:
                            damage += 0.75f;
                            break;
                        case ItemID.FetidBaghnakhs:
                            damage += 0.5f;
                            break;
                        default:
                            damage += 0.5f;
                            break;
                    }
                }
            }

            public override bool CanShoot(Item item, Player player)
            {
                return !HasSheath(player);
            }
        }
    }
}
