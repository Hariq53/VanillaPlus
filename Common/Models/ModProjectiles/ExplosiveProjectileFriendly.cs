using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Common.Models.ModProjectiles
{
    class ExplosiveProjectileFriendly : ModProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (ShouldExplode())
                ExplosionAI();
            else
                RegularAI();
        }

        protected virtual int ExplosionDuration => 3;

        protected virtual int? ExplosionDamage => null;

        protected virtual float ExplosionKnockback => 8f;

        protected virtual SoundStyle ExplosionSound => SoundID.Item14;

        protected virtual Point ExplosionHitBoxDimensions => new(128, 128);

        protected virtual bool ExplodeOnNPCCollision => false;

        protected virtual bool ExplodeOnTileCollision => false;

        public virtual void ExplosionAI()
        {
            if (Projectile.timeLeft == ExplosionDuration - 1)
                ExplosionEffects();
            ExplosionLogic(ExplosionDamage ?? Projectile.damage, ExplosionKnockback);
        }

        public virtual void RegularAI() { }

        public virtual bool ShouldExplode()
        {
            return Projectile.owner == Main.myPlayer && Projectile.timeLeft <= ExplosionDuration;
        }

        public virtual void ExplosionEffects()
        {
            // Play explosion sound
            SoundEngine.PlaySound(ExplosionSound, Projectile.position);
            ProjectilesUtilities.ExplosionVisualEffect(Projectile);
        }

        public virtual void ExplosionLogic(int explosionDamage, float explosionKnockback)
        {
            Projectile.damage = explosionDamage;
            Projectile.velocity = Vector2.Zero;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.Resize(ExplosionHitBoxDimensions.X, ExplosionHitBoxDimensions.Y);
            Projectile.knockBack = explosionKnockback;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (ExplodeOnNPCCollision)
                if (Projectile.timeLeft > ExplosionDuration)
                    Projectile.timeLeft = ExplosionDuration;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.expertMode)
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                    damage /= 5;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (ExplodeOnTileCollision)
                if (Projectile.timeLeft > ExplosionDuration)
                    Projectile.timeLeft = ExplosionDuration;
            return false;
        }
    }
}