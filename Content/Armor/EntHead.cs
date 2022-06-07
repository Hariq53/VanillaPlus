using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class EntHead : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This is a modded hood.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Green; // The rarity of the item
            Item.defense = 4; // The amount of defense the item will give when equipped
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ExampleBreastplate>() && legs.type == ModContent.ItemType<ExampleLeggings>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "reduces mana cost by 100%";  // This is the setbonus tooltip
            player.manaCost -= 1.1f; // Reduces mana cost by 10%

        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        /*public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<ExampleItem>()
				.AddTile<Tiles.Furniture.ExampleWorkbench>()
				.Register();
		}*/
    }
}
