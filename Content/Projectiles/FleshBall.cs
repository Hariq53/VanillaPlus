using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.Projectiles
{
    class FleshBall : ModProjectile
    {
        public override void SetDefaults()
        {
            // HitBox
            Projectile.width = 10;
            Projectile.height = 10;

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
        }

        bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        int TargetIndex
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public static int DustEffect = DustID.Blood;
        public static int StickingLifeTime = 120; // How long the projectile should stick to an enemy

        public override void AI()
        {
            if (IsStickingToTarget) StickyAI();
            else RegularAI();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            IsStickingToTarget = true;
            TargetIndex = target.whoAmI; // Set the target index

            // Change velocity based on delta center of targets (difference between entity centers)
            Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netUpdate = true;
            Projectile.damage = 0; // Makes sure the sticking projectile doesn't deal damage to other NPCs
            Projectile.timeLeft = StickingLifeTime;

            // Spawn dust on hit
            for (int i = 0; i < 5; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustEffect,
                             Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 100);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }

        public readonly static float GravForce = 0.2f; // How fast the projectile will fall
        public readonly static float RotationSpeed = 0.4f; // How fast the projectile will spin mid-air

        private void RegularAI()
        {
            ProjectilesUtilities.ApplyGravity(Projectile, GravForce);
            ProjectilesUtilities.ApplyRotation(Projectile, RotationSpeed);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustEffect,
                         Projectile.velocity.X * 0.3f, Projectile.velocity.Y * 0.3f, Alpha: 150);
        }

        internal static int HitFrequency { get; set; } = 30; // Every how many ticks the projectiles deals damage to the NPC it's sticking to

        private void StickyAI()
        {
            Projectile.Center = Main.npc[TargetIndex].Center - Projectile.velocity * 2f;

            if (Projectile.timeLeft % HitFrequency == 0)
                Main.npc[TargetIndex].StrikeNPC(Projectile.damage / 4, 0, Projectile.direction);

            if (!(Main.npc[TargetIndex].active))
                Projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustEffect,
                             Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 100);
            base.Kill(timeLeft);
        }
    }
}
