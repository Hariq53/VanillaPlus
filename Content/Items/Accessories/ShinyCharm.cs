using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;

namespace VanillaPlus.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    class ShinyCharm : ConfigurableItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            Item.width = 28;
            Item.height = 22;
            Item.accessory = true;
            Item.lifeRegen = 4;
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(gold: 6);
        }

        protected override void SetConfigurableDefaults(ItemConfig config)
        {
        }

        protected override void AddRecipesWithConfig()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BandofRegeneration, 1)
                .AddIngredient(ItemID.ShinyStone, 1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}