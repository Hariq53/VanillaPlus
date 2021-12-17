using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus.Content.Items.Weapons
{
    class ChristmasBarrage : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().ChristmasBarrageToggle;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 28;
            Item.height = 30;
            Item.UseSound = SoundID.Item21;

            // Animation
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;

            // Weapon Specific
            Item.damage = 125;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<ChristmasBarragePlane>();
            Item.shootSpeed = 8f;
            Item.mana = 22;
            Item.DamageType = DamageClass.Magic;

            // Other
            Item.value = Item.sellPrice(gold: 9);
            Item.rare = ItemRarityID.Yellow;
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
    class SantaDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.SantaNK1)
            {
                var entries = npcLoot.Get(false);
                foreach (var entry in entries)
                    if (entry is LeadingConditionRule leadingRule)
                        foreach (var chainedRule in entry.ChainedRules)
                            if (chainedRule.RuleToChain is OneFromOptionsDropRule oneFromOptionsRule)
                            {
                                var tempList = oneFromOptionsRule.dropIds.ToList();
                                int ItemTypeToAdd = ModContent.ItemType<ChristmasBarrage>();
                                if (tempList.Contains(ItemID.EldMelter) && tempList.Contains(ItemID.ChainGun) && !tempList.Contains(ItemTypeToAdd))
                                    tempList.Add(ItemTypeToAdd);
                                oneFromOptionsRule.dropIds = tempList.ToArray();
                                break;
                            }
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}
