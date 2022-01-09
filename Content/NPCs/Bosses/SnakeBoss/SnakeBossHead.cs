using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossHead : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.npcSlots = 5f;
            NPC.width = 56;
            NPC.height = 32;
            NPC.aiStyle = -1;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.netAlways = true;
            NPC.dontCountMe = true;
        }

        int OwnerID
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        bool IsAttacking
        {
            get => NPC.ai[1] == 1f;
            set
            {
                NPC.ai[1] = (value ? 1f : 0f);
                BodyModNPC.AI_State = (value ? (float)SnakeBossBody.ActionState.HeadAttack : (float)SnakeBossBody.ActionState.Jump);
            }
        }

        int AttackTimer
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }

        Player Target
        {
            get => Main.player[NPC.target];
        }

        NPC Body
        {
            get => Main.npc[OwnerID];
        }

        SnakeBossBody BodyModNPC
        {
            get => (SnakeBossBody)Body.ModNPC;
        }

        Vector2 attackDirection = new();
        float attackProgress = 0f;

        public override void AI()
        {
            if (Body.active && Body.type == ModContent.NPCType<SnakeBossBody>())
            {
                int attackFrequency = 120;

                if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                    NPC.TargetClosest();

                if (IsAttacking && BodyModNPC.AI_State != (float)SnakeBossBody.ActionState.Fleeing)
                {
                    AttackAI();
                }
                else
                {
                    StationaryAI();

                    if (AttackTimer++ > attackFrequency)
                        StartAttack();
                }
            }
            else
            {
                // Die
            }
        }

        Vector2 GetHeadPosition()
        {
            Vector2 headPosition = new(-22f, -85f);
            headPosition.X *= -Body.direction;
            headPosition += Body.Center;
            return headPosition;
        }

        void StationaryAI()
        {
            NPC.velocity = Vector2.Zero;
            NPC.Center = GetHeadPosition();
            NPC.direction = Body.direction;
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = 0f;
        }

        void StartAttack()
        {
            IsAttacking = true;
            AttackTimer = 0;
            attackDirection = Vector2.Normalize(Target.Center - NPC.Center);
            attackProgress = 0f;
        }

        void StopAttack()
        {
            IsAttacking = false;
            AttackTimer = 0;
        }

        void AttackAI()
        {
            float attackSpeed = 0.5f, attackReach = 400f;
            Vector2 attackObjective = attackDirection * attackReach;

            if (attackProgress > 20f)
            {
                StopAttack();
            }
            else
            {
                if (attackProgress > 10f)
                    NPC.Center = GetHeadPosition() + Vector2.SmoothStep(attackObjective, attackDirection, (attackProgress - 10f) / 10f);
                else
                    NPC.Center = GetHeadPosition() + Vector2.SmoothStep(attackDirection, attackObjective, attackProgress / 10f);

                attackProgress += attackSpeed;

                NPC.spriteDirection = NPC.direction = (attackDirection.X > 0).ToDirectionInt();
                NPC.rotation = attackDirection.ToRotation() + (NPC.spriteDirection == 1 ? 0f : MathHelper.Pi);
            }
        }
    }
}
