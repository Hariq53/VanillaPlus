using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace VanillaPlus.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	class EntWoodguard : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 40; // Width of the item
			Item.height = 18; // Height of the item
			Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
		}
		public override void UpdateEquip(Player player)
		{

        }
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Wood, 35)
				.AddTile(18)
				.Register();
		}
	}
}
