using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Models.ModProjectiles;
using VanillaPlus.Content.Items.Weapons;

namespace VanillaPlus.Content.Projectiles
{
    class ChristmasBarragePlane : ExplosiveProjectileFriendly
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }

        protected override bool ExplodeOnNPCCollision => true;

        protected override bool ExplodeOnTileCollision => true;

        protected override Point ExplosionHitBoxDimensions => new(100, 100);

        public override void SetDefaults()
        {
            base.SetDefaults();

            // HitBox
            Projectile.width = 32;
            Projectile.height = 32;

            // Damage
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;

            // AI
            Projectile.timeLeft = 120;
        }

        int CurrentFrame
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override bool PreAI()
        {
            if (!ShouldExplode())
            {
                if (++Projectile.frameCounter >= 5)
                {
                    Projectile.frameCounter = 0;
                    CurrentFrame += 2;
                    if (CurrentFrame >= Main.projFrames[Type])
                        CurrentFrame = 0;
                }
                if (Projectile.timeLeft % 40 == 0)
                {
                    CurrentFrame++;

                    IEntitySource source = Projectile.GetSource_FromThis();
                    if (Projectile.owner == Main.myPlayer)
                        if (Main.player[Main.myPlayer].HeldItem.type == ModContent.ItemType<ChristmasBarrage>())
                            source = Main.player[Main.myPlayer].GetSource_ItemUse(Main.player[Main.myPlayer].HeldItem);
                    Projectile.NewProjectile(source, Projectile.Center + Projectile.velocity, Projectile.velocity, ModContent.ProjectileType<ChristmasBarrageBomb>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
            return true;
        }

        public override void RegularAI()
        {
            ProjectilesUtilities.FaceForwardHorizontalSprite(Projectile);
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -32;
                DrawOriginOffsetX = 16;
                DrawOriginOffsetY = 0;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -16;
                DrawOriginOffsetY = 0;
            }
            Projectile.frame = CurrentFrame;
        }
    }
}
