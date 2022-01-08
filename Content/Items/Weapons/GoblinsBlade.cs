using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Items.Weapons
{
    public class GoblinsBlade : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().GoblinDropsToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            Item.damage = 25;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.crit = 2;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
