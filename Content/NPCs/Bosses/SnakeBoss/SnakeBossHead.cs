using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VanillaPlus.Common;

namespace VanillaPlus.Content.NPCs.Bosses.SnakeBoss
{
    class SnakeBossHead : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 5f;
            NPC.width = 54;
            NPC.height = 24;
            NPC.aiStyle = -1;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.netAlways = true;
            NPC.dontCountMe = true;
        }

        public enum ActionState
        {
            Stationary,
            Attack
        }

        private enum Frame
        {
            Phase1MouthClosed,
            Phase1TongueOut1,
            Phase1TongueOut2,
            Phase1MouthOpen
        }

        public int BodyID
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        SnakeBossBody Body
        {
            get
            {
                NPC npc = Main.npc[BodyID];
                if (npc.active &&
                    npc.type == ModContent.NPCType<SnakeBossBody>() &&
                    npc.ModNPC is SnakeBossBody body)
                    return body;
                else
                {
                    NPCsUtilities.Kill(NPC.whoAmI);
                    return null;
                }
            }
        }

        public ActionState AIState
        {
            get => (ActionState)NPC.ai[1];
            set => NPC.ai[1] = (float)value;
        }

        public ref float AttackProgress => ref NPC.ai[2];

        public int SectionsCount
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        bool SetupFlag
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : -1f;
        }

        int AnimationPhase
        {
            get => (int)NPC.localAI[1];
            set => NPC.localAI[1] = value;
        }

        public ref float AttackDirection => ref NPC.localAI[1];

        Player Target
        {
            get => Main.player[NPC.target];
        }

        Vector2 PivotPoint
        {
            get
            {
                if (NPC.direction == 1)
                    return NPC.Right;
                else
                    return NPC.Left;
            }

            set
            {
                if (NPC.direction == 1)
                    NPC.Right = value;
                else
                    NPC.Left = value;
            }
        }

        private bool Setup()
        {
            if (Body == null)
            {
                NPCsUtilities.Kill(NPC.whoAmI);
                return false;
            }
            NPC.realLife = BodyID;
            return true;
        }

        public override bool PreAI()
        {
            if (!SetupFlag)
            {
                SetupFlag = Setup();
                return SetupFlag;
            }
            return true;
        }

        public override void AI()
        {
            if (Body == null)
                return;

            switch (AIState)
            {
                case ActionState.Attack:
                    AttackAI();
                    break;
                case ActionState.Stationary:
                    StationaryAI();
                    break;
                default:
                    StationaryAI();
                    break;
            }
        }

        void StationaryAI()
        {
            NPC.direction = NPC.spriteDirection = Body.NPC.direction;
            PivotPoint = Body.HeadPosition;
            NPC.position += GetOffsetCoefficient(NPC.direction);
        }

        void AttackAI()
        {
            float attackSpeed = 0.5f, attackReach = 400f;
            Vector2 attackRotationVector = AttackDirection.ToRotationVector2();
            Vector2 attackObjective = attackRotationVector * attackReach;
            Vector2 attackPosition;

            if (AttackProgress > 20f)
            {
                StopAttack();
            }
            else
            {
                NPC.spriteDirection = NPC.direction = Body.NPC.direction;
                NPC.rotation = AttackDirection + (NPC.spriteDirection == -1 ? 0f : MathHelper.Pi);

                if (AttackProgress > 10f)
                {
                    KillNeckSections(true);

                    float actualProgress = (AttackProgress - 10f) / 10f;

                    attackPosition = DetermineAttackPosition(attackObjective, attackRotationVector, actualProgress);

                    PivotPoint = attackPosition;

                    AttackProgress += attackSpeed / 2;
                }
                else
                {
                    float actualProgress = AttackProgress / 10f;

                    attackPosition = DetermineAttackPosition(attackRotationVector, attackObjective, actualProgress);

                    PivotPoint = attackPosition;

                    if (SectionsCount == 0 || NoNeckIntersection())
                        SpawnNeckSection(NPC.Center.ToPoint(), AttackDirection, SectionsCount++);
                }

                AttackProgress += attackSpeed;
            }
        }

        public void StartAttack()
        {
            if (NPC.target < 0 || NPC.target == 255 || Target.dead || !Target.active)
                NPC.TargetClosest();

            AIState = ActionState.Attack;
            AttackProgress = 0f;
            AttackDirection = Vector2.Normalize(Target.Center - NPC.Center).ToRotation();
            SectionsCount = 0;
        }

        public void StopAttack()
        {
            AIState = ActionState.Stationary;
            NPC.rotation = 0f;
            KillNeckSections();
        }

        Vector2 DetermineAttackPosition(Vector2 attackDirection, Vector2 targetPosition, float attackProgress)
        {
            Vector2 progressVector = Vector2.SmoothStep(attackDirection, targetPosition, attackProgress);

            return Body.HeadPosition + GetOffsetCoefficient(NPC.direction) + progressVector;
        }

        Vector2 GetOffsetCoefficient(int direction)
        {
            Vector2 pointToRotate = PivotPoint;
            Vector2 realPos = pointToRotate.RotatedBy(NPC.rotation, NPC.Center);

            Vector2 offset = pointToRotate - realPos;

            return offset;
        }

        int SpawnNeckSection(Point spawnPos, float rotation, int sectionNumber)
        {
            return NPC.NewNPC(NPC.GetBossSpawnSource(NPC.target), spawnPos.X, spawnPos.Y, ModContent.NPCType<SnakeBossNeck>(), NPC.whoAmI, BodyID, rotation, sectionNumber); ;
        }

        bool NoNeckIntersection()
        {
            foreach (var npc in Main.npc)
            {
                if (!npc.active || npc.type != ModContent.NPCType<SnakeBossNeck>())
                    continue;

                if (NPC.Hitbox.Intersects(npc.Hitbox))
                    return false;
            }

            return true;
        }

        void KillNeckSections(bool isIntersecting = false)
        {
            foreach (var npc in Main.npc)
            {
                if (!npc.active || npc.type != ModContent.NPCType<SnakeBossNeck>())
                    continue;

                if (isIntersecting)
                    if (!NPC.Hitbox.Intersects(npc.Hitbox))
                        continue;

                NPCsUtilities.Kill(npc.whoAmI);
            }
        }

        private void SetFrame(Frame frame, int frameHeight)
        {
            NPC.frame.Y = frameHeight * (int)frame;
            switch (frame)
            {
                case Frame.Phase1TongueOut1:
                case Frame.Phase1MouthClosed:
                    NPC.gfxOffY = 0f;
                    break;
                case Frame.Phase1TongueOut2:
                    NPC.gfxOffY = 2f;
                    break;
                case Frame.Phase1MouthOpen:
                    NPC.gfxOffY = 14f;
                    break;
                default:
                    NPC.frame.Y = 0;
                    NPC.gfxOffY = 0f;
                    break;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            int animationDelay = 3, animationWait = 60;
            switch (AIState)
            {
                case ActionState.Attack:
                    {
                        if (AttackProgress < 12f)
                            SetFrame(Frame.Phase1MouthOpen, frameHeight);
                        else
                            SetFrame(Frame.Phase1MouthClosed, frameHeight);
                        NPC.frameCounter = 0.0;
                        break;
                    }
                case ActionState.Stationary:
                    {
                        NPC.frameCounter++;

                        if (NPC.frameCounter <= animationWait)
                        {
                            // To reset after bite attack
                            AnimationPhase = 0;
                            return;
                        }

                        if ((NPC.frameCounter - animationWait) > animationDelay)
                        {
                            NPC.frameCounter = animationWait;
                            switch (++AnimationPhase)
                            {
                                case 1:
                                    SetFrame(Frame.Phase1TongueOut1, frameHeight);
                                    break;
                                case 2:
                                    SetFrame(Frame.Phase1TongueOut2, frameHeight);
                                    break;
                                case 3:
                                    SetFrame(Frame.Phase1TongueOut1, frameHeight);
                                    break;
                                default:
                                    AnimationPhase = NPC.frame.Y = 0;
                                    NPC.gfxOffY = 0f;
                                    NPC.frameCounter = 0.0;
                                    break;
                            }
                        }
                        break;
                    }
                default:
                    SetFrame(Frame.Phase1MouthClosed, frameHeight);
                    return;
            }
        }
    }
}
