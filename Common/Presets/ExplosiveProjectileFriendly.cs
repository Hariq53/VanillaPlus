using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Common.Presets
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

        protected int ExplosionDuration = 3;
        protected int ExplosionDamage = -1;
        protected LegacySoundStyle ExplosionSound = SoundID.Item14;
        protected Point ExplosionHitBoxDimensions = new(128, 128);
        protected bool ExplodeOnNPCCollision = false;
        protected bool ExplodeOnTileCollision = false;

        public virtual void ExplosionAI()
        {
            if (Projectile.timeLeft == ExplosionDuration - 1)
                ExplosionEffects();
            ExplosionLogic();
        }

        public virtual void RegularAI()
        {
            Projectile.damage = 0;
        }

        public virtual bool ShouldExplode()
        {
            return (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= ExplosionDuration);
        }

        public virtual void ExplosionEffects()
        {
            // Play explosion sound
            SoundEngine.PlaySound(ExplosionSound, Projectile.position);
            ProjectilesUtilities.ExplosionVisualEffect(Projectile);
        }

        public virtual void ExplosionLogic()
        {
            if (ExplosionDamage > 0)
                Projectile.damage = ExplosionDamage;
            Projectile.velocity = Vector2.Zero;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.Resize(ExplosionHitBoxDimensions.X, ExplosionHitBoxDimensions.Y);
            Projectile.knockBack = 8f;
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