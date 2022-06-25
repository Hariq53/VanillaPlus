using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common.Config;
using VanillaPlus.Common.Models.Config;
using VanillaPlus.Common.Models.ModItems;
using VanillaPlus.Content.Buffs;
using VanillaPlus.Content.Projectiles.Minions;

namespace VanillaPlus.Content.Items.Weapons
{
    class EyeballOnAStick : SummonStaff
    {
        protected override WeaponConfig? Config => VanillaPlus.ServerSideConfig?.Items.EyeballOnAStick;

        protected override int MinionBuff => ModContent.BuffType<MiniServantMinion>();

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        protected override void SetRegularDefaults()
        {
            base.SetRegularDefaults();

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
