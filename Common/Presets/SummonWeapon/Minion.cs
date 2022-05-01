using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Common.Presets.SummonWeapon
{
    class Minion : ModProjectile
    {
        public override bool IsLoadingEnabled(Mod mod) { return false; }

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; // This is necessary for right-click targeting
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.minion = true; // Declares this as a minion (has many effects)
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f; // Amount of slots this minion occupies from the total minion slots available to the player
            Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
        }

        public override bool? CanCutTiles() { return false; }

        public override bool MinionContactDamage() { return true; }

        public bool FoundTarget
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : -1f;
        }

        int TargetID
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public NPC Target
        {
            get
            {
                NPC npc = Main.npc[TargetID];
                if (npc.active)
                    return npc;
                else
                    return null;
            }

            set
            {
                TargetID = value.whoAmI;
            }
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
                return;

            GeneralBehavior(owner, out Vector2 idlePosition);

            SearchForTargets(owner, out bool foundTarget, out NPC target);
            FoundTarget = foundTarget;
            Target = target;

            if (PreAttackBehaviour(owner))
                AttackBehaviour(Target);
            else
                IdleBehaviour(idlePosition);

            Visuals();
        }

        protected virtual int MinionBuff => 0;

        /// <summary>
        /// The "active check" makes sure the minion is alive while the player is alive, and despawns if not
        /// </summary>
        /// <param name="owner">The owner of this minion, the one on which the active check is run </param>
        /// <returns></returns>
        protected virtual bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(MinionBuff);
                return false;
            }

            if (owner.HasBuff(MinionBuff))
                Projectile.timeLeft = 2;

            return true;
        }

        protected virtual Vector2 GetIdlePosition(Player owner) { return owner.Center; }

        /// <summary>
        /// Runs right after the active check
        /// </summary>
        protected virtual void GeneralBehavior(Player owner, out Vector2 idlePosition) { idlePosition = GetIdlePosition(owner); }

        /// <summary>
        /// The maximum distance at which the minion can see an enemy 
        /// </summary>
        protected virtual float MaxSearchDistance => 700f;

        /// <summary>
        /// The maximum distance at which the minion can attack an enemy when using the right click targetting
        /// </summary>
        protected virtual float MaxTargetDistance => 2000f;

        /// <summary>
        /// Allows you to check all the active NPCs for a suitable target, and outputs wheter or not it has been found 
        /// </summary>
        /// <param name="owner">The owner of this minion (useful to have only when right click targetting is active)</param>
        /// <param name="foundTarget">Wheter or not a suitable target has been found</param>
        /// <param name="target">The target center, it's equal to the minion's if no suitable target was found</param>
        protected virtual void SearchForTargets(Player owner, out bool foundTarget, out NPC target)
        {
            // Starting search distance
            float distanceFromTarget = MaxSearchDistance;
            Vector2 targetCenter = Projectile.position;
            target = new();
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                Vector2 npcCenter = Main.npc[owner.MinionAttackTargetNPC].Center;
                float between = Vector2.Distance(npcCenter, Projectile.Center);
                if (between < MaxTargetDistance)
                {
                    distanceFromTarget = between;
                    target = Main.npc[owner.MinionAttackTargetNPC];
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                foreach (NPC npc in Main.npc)
                {
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        if (((closest && inRange) || !foundTarget) && lineOfSight)
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            target = npc;
                            foundTarget = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle the minion's idle behaviour (mainly movement)
        /// </summary>
        /// <param name="idlePosition">The objective the minion must move towards</param>
        protected virtual void IdleBehaviour(Vector2 idlePosition) { }

        protected virtual void AttackBehaviour(NPC target) { }

        /// <summary>
        /// Used for all the visual effects of the minion
        /// </summary>
        protected virtual void Visuals() { }

        /// <summary>
        /// Runs before the attack behaviour, return false to prevent the attack behaviour from running (the idle behaviour will be run instead)
        /// </summary>
        /// <returns></returns>
        protected virtual bool PreAttackBehaviour(Player owner) { return FoundTarget && Target.active; }
    }
}
