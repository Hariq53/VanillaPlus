using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Content.Items.Weapons
{
    public class GoblinsBlade : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().GoblinDropsToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            // GFX
            Item.width = 34;
            Item.height = 36;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;

            // Weapon Specific
            Item.damage = 25;
            Item.knockBack = 5f;
            Item.crit = 2;
            Item.DamageType = DamageClass.Melee;

            // Other
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
