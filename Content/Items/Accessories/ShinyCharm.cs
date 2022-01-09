using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class ShinyCharm : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().ShinyCharmToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 22;
            Item.accessory = true;
            Item.lifeRegen = 4;
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(gold: 6);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BandofRegeneration, 1)
                .AddIngredient(ItemID.ShinyStone, 1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}