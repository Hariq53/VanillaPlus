using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.ModItems;
using VanillaPlus.Content.Buffs;
using VanillaPlus.Content.Projectiles.Minions;

namespace VanillaPlus.Content.Items.Weapons
{
    class EyeballOnAStick : SummonStaff
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusServerConfig>().EOCDropsToggle;
        }

        protected override int MinionBuff => ModContent.BuffType<MiniServantMinion>();

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            // GFX
            Item.width = 26;
            Item.height = 28;

            // Animation
            Item.useAnimation = 36;
            Item.useTime = 36;

            // Weapon Specific
            Item.damage = 10;
            Item.knockBack = 5f;
            Item.shoot = ModContent.ProjectileType<MiniServant>();
            Item.mana = 10;

            // Other
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 60);
        }
    }
}
