using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Items.Weapons
{
    public class WarriorsMallet : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().GoblinDropsToggle;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 40;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.crit = 5;
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.knockBack = 10;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
        }
    }
}