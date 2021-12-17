using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Projectiles
{
    class RottenBit : ModProjectile
    {
        public override void SetDefaults()
        {
            // Hitbox
            Projectile.width = 10;
            Projectile.height = 10;

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
        }

        readonly int DustEffect = DustID.CorruptGibs;

        public override void AI()
        {
            ProjectilesUtilities.ApplyGravity(Projectile, 0.2f);
            ProjectilesUtilities.FaceForwardHorizontalSprite(Projectile);
            if (Main.rand.NextBool())
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptGibs, default, default, default, default, Main.rand.NextFloat(0.6f, 1f));

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath1.SoundId, (int)Projectile.position.X, (int)Projectile.position.Y, 1, 0.4f);
            for (int i = 0; i < 10; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustEffect, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 100);
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Stinky, 180);
            target.AddBuff(BuffID.Slow, 180);
        }
    }
}
