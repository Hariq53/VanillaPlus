using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.Mount
{
    public class Motorcycle : ModMount
    {
        public override void SetStaticDefaults()
        {
            // Movement
            MountData.jumpHeight = 5; // How high the mount can jump.
            MountData.acceleration = 0.3f; // The rate at which the mount speeds up.
            MountData.jumpSpeed = 4f; // The rate at which the player and mount ascend towards (negative y velocity) the jump height when the jump button is presssed.
            MountData.blockExtraJumps = false; // Determines whether or not you can use a double jump (like cloud in a bottle) while in the mount.
            MountData.constantJump = true; // Allows you to hold the jump button down.
            MountData.heightBoost = 20; // Height between the mount and the ground
            MountData.fallDamage = 0.5f; // Fall damage multiplier.
            MountData.runSpeed = 15f; // The speed of the mount
            MountData.dashSpeed = 8f; // The speed the mount moves when in the state of dashing.
            MountData.flightTimeMax = 1; // The amount of time in frames a mount can be in the state of flying.

            // Misc
            MountData.fatigueMax = 0;
            MountData.buff = ModContent.BuffType<Buffs.MotorcycleMount>(); // The ID number of the buff assigned to the mount.

            // Effects
            MountData.spawnDust = DustID.Ash; // The ID of the dust spawned when mounted or dismounted.

            // Frame data and player offsets
            MountData.totalFrames = 6; // Amount of animation frames for the mount
            MountData.playerYOffsets = Enumerable.Repeat(16, MountData.totalFrames).ToArray(); // Fills an array with values for less repeating code
            MountData.constantJump = true;
            MountData.emitsLight = true;
            MountData.lightColor = new Vector3(255, 255, 100);
            MountData.abilityChargeMax = 1;
            MountData.abilityCooldown = 0;
            MountData.abilityDuration = 100;
            MountData.xOffset = 20;
            MountData.yOffset = 20;
            MountData.playerHeadOffset = 15;
            MountData.bodyFrame = 3;

            // Standing
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 7;
            MountData.standingFrameStart = 0;
            // Running
            MountData.runningFrameCount = 3;
            MountData.runningFrameDelay = 8;
            MountData.runningFrameStart = 0;
            // Flying
            MountData.flyingFrameCount = 3;
            MountData.flyingFrameDelay = 7;
            MountData.flyingFrameStart = 3;
            // In-air
            MountData.inAirFrameCount = 3;
            MountData.inAirFrameDelay = 7;
            MountData.inAirFrameStart = 3;
            // Idle
            MountData.idleFrameCount = 1;
            MountData.idleFrameDelay = 7;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = false;
            // Swim
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;

            if (!Main.dedServ)
            {
                MountData.textureWidth = MountData.backTexture.Width();
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }
        public override void UpdateEffects(Player player)
        {
            float speed = Math.Abs(player.velocity.X);
            if (speed > 5f)
            {
                if (Collision.WetCollision(player.position, player.width, player.height + 30) == true)
                {
                    Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height + 30, DustID.Water_BloodMoon);
                    player.maxFallSpeed = 0;
                }
            }
            else
            {
                if (Collision.WetCollision(player.position, player.width, player.height + 10) == true)
                {
                    MountData.runSpeed = 3f;
                    MountData.acceleration = 0.1f;
                    player.maxFallSpeed = 3;
                }
                else
                {
                    MountData.acceleration = 0.3f;
                    MountData.runSpeed = 15f;
                }
            }
            Vector2 offsetPosition = new(player.position.X + 8, player.position.Y - 10);
            if (Collision.SolidCollision(offsetPosition, 75, 70) == true && speed > 6f)
            {
                player.maxFallSpeed = -4;
            }
            Rectangle hitbox = new((int)player.position.X, (int)player.position.Y, 100, 70);
            player.CollideWithNPCs(hitbox, 1.6f * speed, 3.3f * speed, 4, 5);

        }

        public override void AimAbility(Player player, Vector2 mousePosition)
        {
            base.AimAbility(player, mousePosition);
        }
    }
}