using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace VanillaPlus.Common
{
    class ProjectilesUtilities
    {
        public static void ApplyGravity(Projectile Projectile, float gravForce)
        {
            Projectile.velocity.Y += gravForce;
            if (Projectile.velocity.Y > 16f) // Prevents the Projectile from going past 16f Y velocity so it won't travel through blocks
                Projectile.velocity.Y = 16f;
        }

        public static void ApplyGravity(ref float projectileYVelocity, float gravForce)
        {
            projectileYVelocity += gravForce;
            if (projectileYVelocity > 16f) // Prevents the Projectile from going past 16f Y velocity so it won't travel through blocks
                projectileYVelocity = 16f;
        }

        public static void ApplyRotation(Projectile Projectile, float rotationSpeed)
        {
            Projectile.rotation += rotationSpeed * Projectile.direction;
        }

        public static void ApplyRotation(ref float projectileRotation, float projectileDirection, float rotationSpeed)
        {
            projectileRotation += rotationSpeed * projectileDirection;
        }

        public static void FaceForwardHorizontalSprite(Projectile projectile)
        {
            projectile.spriteDirection = projectile.direction = (projectile.velocity.X > 0).ToDirectionInt();
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
        }

        public static void ExplosionVisualEffect(Projectile projectile, int smokeDustID = DustID.Smoke, int fireDustID = DustID.Torch, params int[] smokeGore)
        {
            if (smokeGore.Length == 0)
            {
                smokeGore = new int[3];
                smokeGore[0] = GoreID.Smoke1;
                smokeGore[1] = GoreID.Smoke2;
                smokeGore[2] = GoreID.Smoke3;
            }

            // Smoke Dust spawn
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, smokeDustID, Alpha: 100, Scale: 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, fireDustID, Alpha: 100, Scale: 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, fireDustID, Alpha: 100, Scale: 3f);
                Main.dust[dustIndex].velocity *= 3f;
            }
            // Large Smoke Gore spawn
            for (int i = 0; i < 2; i++)
            {
                int goreIndex = Gore.NewGore(projectile.GetSource_Death(), new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default, smokeGore[Main.rand.Next(smokeGore.Length)]);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(projectile.GetSource_Death(), new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default, smokeGore[Main.rand.Next(smokeGore.Length)]);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(projectile.GetSource_Death(), new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default, smokeGore[Main.rand.Next(smokeGore.Length)]);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(projectile.GetSource_Death(), new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default, smokeGore[Main.rand.Next(smokeGore.Length)]);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
        }

        public static void InfernoForkVisualEffect(Projectile projectile, int dustID = DustID.InfernoFork, float dustScale = 1.5f)
        {
            for (int i = 0; i < 25; i++)
            {
                float num338 = Main.rand.Next(-10, 11);
                float num339 = Main.rand.Next(-10, 11);
                float num340 = Main.rand.Next(3, 9);
                float num341 = MathF.Sqrt(num338 * num338 + num339 * num339);
                num341 = num340 / num341;
                num338 *= num341;
                num339 *= num341;
                int num342 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustID, Scale: dustScale);
                Main.dust[num342].noGravity = true;
                Main.dust[num342].position.X = projectile.Center.X;
                Main.dust[num342].position.Y = projectile.Center.Y;
                Main.dust[num342].position.X += Main.rand.Next(-10, 11);
                Main.dust[num342].position.Y += Main.rand.Next(-10, 11);
                Main.dust[num342].velocity.X = num338;
                Main.dust[num342].velocity.Y = num339;
            }
        }

        public static void BoomerangAI(Projectile projectile, Vector2 targetCenter, float speed, float inertia)
        {
            Vector2 vectorFromTarget = targetCenter - projectile.Center;
            float distanceFromTarget = Vector2.Distance(targetCenter, projectile.Center);

            distanceFromTarget = speed / distanceFromTarget;
            vectorFromTarget *= distanceFromTarget;

            if (projectile.velocity.X < vectorFromTarget.X)
            {
                projectile.velocity.X += inertia;

                if (projectile.velocity.X < 0f && vectorFromTarget.X > 0f)
                    projectile.velocity.X += inertia;
            }
            else if (projectile.velocity.X > vectorFromTarget.X)
            {
                projectile.velocity.X -= inertia;

                if (projectile.velocity.X > 0f && vectorFromTarget.X < 0f)
                    projectile.velocity.X -= inertia;
            }

            if (projectile.velocity.Y < vectorFromTarget.Y)
            {
                projectile.velocity.Y += inertia;

                if (projectile.velocity.Y < 0f && vectorFromTarget.Y > 0f)
                    projectile.velocity.Y += inertia;
            }
            else if (projectile.velocity.Y > vectorFromTarget.Y)
            {
                projectile.velocity.Y -= inertia;

                if (projectile.velocity.Y > 0f && vectorFromTarget.Y < 0f)
                    projectile.velocity.Y -= inertia;
            }
        }
    }
}
