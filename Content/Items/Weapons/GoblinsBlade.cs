using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;

namespace VanillaPlus.Content.Items.Weapons
{
    public class GoblinsBlade : ConfigurableWeapon
    {
        protected override WeaponConfig? Config => VanillaPlus.ServerSideConfig?.Items.GoblinsBlade;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            // GFX
            Item.width = 34;
            Item.height = 36;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useTime = Item.useAnimation = 26;
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
