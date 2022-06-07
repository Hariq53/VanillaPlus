using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Front)]
    class WaterDome : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        Point prevPos = Point.Zero;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            var pos = player.Center.ToTileCoordinates();
            if (pos != prevPos)
            {
                if (prevPos != Point.Zero)
                    Main.tile[prevPos.X, prevPos.Y].LiquidAmount = byte.MinValue;
                Main.tile[pos.X, pos.Y].LiquidAmount = byte.MaxValue;
                prevPos = player.Center.ToTileCoordinates();
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 5)
                .Register();
        }
    }
}
