using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Content.Projectiles;

namespace VanillaPlus.Content.Items.Weapons
{
    public class Tear : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanillaPlusConfig>().EOCDropsToggle;
        }

        public override string Texture => ModContent.GetInstance<VanillaPlusClientConfig>().TearAltToggle ? "VanillaPlus/Content/Items/Weapons/Tear_Alt" : base.Texture;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override bool? UseItem(Player player)
        {
            return base.UseItem(player);
        }

        public override void SetDefaults()
        {
            // GFX
            Item.width = 34;
            Item.height = 34;
            Item.UseSound = SoundID.Item1;

            // Animation
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            // Weapon Specific
            Item.damage = 9;
            Item.knockBack = 3.5f;
            Item.shoot = ModContent.ProjectileType<TearProjectile>();
            Item.shootSpeed = 16f;
            Item.DamageType = DamageClass.Melee;

            // Other
            Item.value = Item.sellPrice(silver: 70);
            Item.rare = ItemRarityID.Green;
        }
    }

    class TearProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // The following sets are only applicable to yoyo that use aiStyle 99.
            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 7f;
            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 200f;
            // YoyosTopSpeed is top speed of the yoyo projectile. 
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 10f;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodYoyo);
        }

        bool EyesSpawned
        {
            get => Projectile.localAI[1] == 1f;
            set => Projectile.localAI[1] = value ? 1f : -1f;
        }

        const int EYE_COUNT = 3;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            // Change the string color to red if no string accessories are being used
            if (player.stringColor == 0)
                player.stringColor = 1;

            if (!EyesSpawned)
            {
                for (int i = 1; i < EYE_COUNT + 1; i++)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, MathHelper.ToRadians(360f / EYE_COUNT * i).ToRotationVector2(), ModContent.ProjectileType<TearEye>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.whoAmI);
                EyesSpawned = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.type == ModContent.ProjectileType<TearEye>() && projectile.ModProjectile is TearEye eye)
                    if (eye.OwnerID == Projectile.whoAmI)
                        projectile.Kill();
            }
        }
    }
}
