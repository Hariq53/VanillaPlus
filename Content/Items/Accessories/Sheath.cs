using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Items.Accessories
{
    internal class Sheath : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // SheathHandlerItem.ShootEnabled = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BandofRegeneration, 1)
                .AddIngredient(ItemID.ShinyStone, 1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        class SheathHandlerPlayer : ModPlayer
        {
            public bool Sheath = false;
        }

        class SheathHandlerItem : GlobalItem
        {
            public override bool InstancePerEntity => true;

            public bool ShootEnabled = true;

            public override GlobalItem Clone(Item item, Item itemClone)
            {
                return base.Clone(item, itemClone);
            }

            public override bool CanShoot(Item item, Player player)
            {
                bool sheath = ShootEnabled;
                //Sheath = false;
                return ShootEnabled;
            }
        }
    }
}
