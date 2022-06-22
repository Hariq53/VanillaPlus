using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Content.Items.Weapons
{
    public class WarriorsMallet : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true; //.GoblinDropsToggle;
        }


        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 32;
            Item.height = 32;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;

            // Weapon Specific
            Item.damage = 40;
            Item.knockBack = 10f;
            Item.crit = 5;
            Item.DamageType = DamageClass.Melee;

            // Other
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
        }
    }
}