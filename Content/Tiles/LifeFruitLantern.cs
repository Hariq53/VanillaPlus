using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;

namespace VanillaPlus.Content.Tiles
{
    class LifeFruitLanternTile : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            TileConfig? config = VanillaPlus.ServerSideConfig?.Tiles.LifeFruitLantern;
            if (config is not null && config.IsHardDisabled)
                return false;
            return base.IsLoadingEnabled(mod);
        }

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.DrawYOffset = -2;
            AddMapEntry(new Color(200, 255, 0));
            TileObjectData.addTile(Type);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, ModContent.ItemType<LifeFruitLantern>());
        }

        public override void HitWire(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int topY = j - tile.TileFrameY / 18 % 3;
            short frameAdjustment = (short)(tile.TileFrameX > 0 ? -18 : 18);
            Main.tile[i, topY].TileFrameX += frameAdjustment;
            Main.tile[i, topY + 1].TileFrameX += frameAdjustment;
            Wiring.SkipWire(i, topY);
            Wiring.SkipWire(i, topY + 1);
            NetMessage.SendTileSquare(-1, i, topY + 1, 3);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Main.LocalPlayer.GetModPlayer<LanternHandlerPlayer>().lantern = true;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX == 0)
            {
                r = 0.8f;
                g = 1f;
                b = 0f;
            }
        }
    }

    class LifeFruitLantern : ConfigurableModItem
    {
        // Extract item info from the TileConfig by using the user-defined conversion operator
        protected override ItemConfig? Config => (ItemConfig?)VanillaPlus.ServerSideConfig?.Tiles.LifeFruitLantern;

        protected override void SetRegularDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<LifeFruitLanternTile>();
            Item.placeStyle = 0;
            Item.width = 12;
            Item.height = 28;
            Item.value = 75000;
            Item.rare = ItemRarityID.Green;
        }

        protected override void AddRecipesWithConfig()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LifeFruit)
                .AddIngredient(ItemID.HeartLantern)
                .Register();
        }
    }

    class LanternHandlerPlayer : ModPlayer
    {
        public bool lantern = false;

        //Runs just before UpdateLifeRegen
        public override void PostUpdateMiscEffects()
        {
            if (lantern)
                Player.AddBuff(ModContent.BuffType<Buffs.LifeFruitLamp>(), 2, false);
        }
    }

    class LanternHandlerWorld : ModSystem
    {
        public override void ResetNearbyTileEffects()
        {
            Main.LocalPlayer.GetModPlayer<LanternHandlerPlayer>().lantern = false;
        }
    }
}
