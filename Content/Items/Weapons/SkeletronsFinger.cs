using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;
using VanillaPlus.Common.Config;

namespace VanillaPlus.Content.Items.Weapons
{
    class SkeletronsFinger : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true; //.SkeletronDropsToggle;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 15f;
            Item.damage = 35;
            Item.width = 20;
            Item.height = 34;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.buyPrice(0, 35);
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<SkeletronsFingerProjectile>();
        }

        Projectile projectile = new();

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return !projectile.active;
        }
    }

    class SkeletronsFingerProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
        }

        public int BackToPlayerTimer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        bool lookingForTarget = true;
        NPC target = new();
        int originalDirection = 0;

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 8;
                SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
            }

            // Spawn bone dust trail
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Bone, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, Scale: 1);
                dust.noGravity = true;
            }


            if (originalDirection == 0)
                originalDirection = owner.direction;

            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1]++;
                HomingAI(owner);
                if (Projectile.ai[1] >= 30f)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.tileCollide = false;

                ProjectilesUtilities.BoomerangAI(Projectile, owner.Center, 12f, 1.5f);

                if (Main.myPlayer == Projectile.owner)
                    if (Projectile.Hitbox.Intersects(Main.player[Projectile.owner].Hitbox))
                        Projectile.Kill();
            }

            Projectile.rotation += 0.4f * (float)Projectile.direction;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] == 0f)
            {
                Projectile.velocity.X = 0f - Projectile.velocity.X;
                Projectile.velocity.Y = 0f - Projectile.velocity.Y;
                Projectile.netUpdate = true;
            }
            Projectile.ai[0] = 1f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] = 1f;
            Projectile.velocity.X = 0f - oldVelocity.X;
            Projectile.velocity.Y = 0f - oldVelocity.Y;
            return false;
        }

        void HomingAI(Player owner, float speed = 15f, float inertia = 10f)
        {
            if (lookingForTarget)
                SearchForTargets(owner, originalDirection, out target, out lookingForTarget);

            if (target.active)
            {
                Vector2 directionToTarget = target.Center - Projectile.Center;
                directionToTarget.Normalize();
                directionToTarget *= speed;
                Projectile.velocity = (Vector2.Normalize(Projectile.velocity) * speed * (inertia - 1) + directionToTarget) / inertia;
            }
        }

        bool SearchForTargets(Player owner, int direction, out NPC target, out bool lookingForTarget)
        {
            // Starting search distance
            float distanceFromTarget = 800f;

            Vector2 targetCenter = Projectile.position;
            lookingForTarget = true;
            target = new();

            if (lookingForTarget)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);

                        if (((closest && inRange) || lookingForTarget) && lineOfSight)
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            target = npc;
                            lookingForTarget = false;
                        }
                    }
                }
            }

            return lookingForTarget;
        }
    }
}
