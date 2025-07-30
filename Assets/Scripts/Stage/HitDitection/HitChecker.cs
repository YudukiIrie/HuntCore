using System.Collections.Generic;

namespace Stage.HitDetection
{
    /// <summary>
    /// �����蔻��
    /// </summary>
    public static class HitChecker
    {
        /// <summary>
        /// �R���C�_�[���m�̓����蔻��
        /// �`�󂲂Ƃɂ��ꂼ��ɓK�������\�b�h���Ă�
        /// </summary>
        /// <param name="oneself">�󓮑��R���C�_�[</param>
        /// <param name="other">�\�����R���C�_�[</param>
        /// <returns>true:�ڐG, false:��ڐG</returns>
        public static bool IsColliding(HitCollider oneself, List<HitCollider> other)
        {
            var a = oneself;
            foreach (var b in other)
            {
                // �󓮑��̏��L�҂�����ς݁A
                // �܂��̓R���C�_�[������E����̏ꍇ�͖���
                if (b.Owner.WasHit ||
                    b.Role == HitCollider.ColliderRole.Weapon ||
                    b.Role == HitCollider.ColliderRole.Roll)
                    return false;

                // === ���g��OBB�̏ꍇ ===
                if (a.Shape == HitCollider.ColliderShape.OBB)
                {
                    // ���肪OBB�̏ꍇ
                    if (b.Shape == HitCollider.ColliderShape.OBB)
                    {
                        if (OBBHitChecker.IntersectOBBs((OBB)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // ���肪���̂̏ꍇ
                    else if (b.Shape == HitCollider.ColliderShape.Sphere)
                    {
                        if (SphereOBBHitChecker.IntersectSphereOBB((HitSphere)b, (OBB)a))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // ���肪�J�v�Z���̏ꍇ
                    else if (b.Shape == HitCollider.ColliderShape.Capsule)
                    {
                        if (CapsuleOBBHitChecker.IntersectCapsuleOBB((HitCapsule)b, (OBB)a))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                }

                // === ���g�����̂̏ꍇ ===
                else if (a.Shape == HitCollider.ColliderShape.Sphere)
                {
                    // ���肪OBB�̏ꍇ
                    if (b.Shape == HitCollider.ColliderShape.OBB)
                    {
                        if (SphereOBBHitChecker.IntersectSphereOBB((HitSphere)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // ���肪�J�v�Z���̏ꍇ
                    if (b.Shape == HitCollider.ColliderShape.Capsule)
                    {
                        if (SphereCapsuleHitChecker.IntersectSphereCapsule((HitSphere)a, (HitCapsule)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                }

                // === ���g���J�v�Z���̏ꍇ ===
                else if (a.Shape == HitCollider.ColliderShape.Capsule)
                {
                    // ���肪OBB�̏ꍇ
                    if (b.Shape == HitCollider.ColliderShape.OBB)
                    {
                        if (CapsuleOBBHitChecker.IntersectCapsuleOBB((HitCapsule)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // ���肪�J�v�Z���̏ꍇ
                    else if (b.Shape == HitCollider.ColliderShape.Capsule)
                    {
                        if (CapsuleHitChecker.IntersectCapsules((HitCapsule)a, (HitCapsule)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// �q�b�g���̃��Z�b�g
        /// </summary>
        /// <param name="oneself">�\�����R���C�_�[</param>
        /// <param name="other">�󓮑��R���C�_�[</param>
        public static void ResetHitInfo(HitCollider oneself, List<HitCollider> other)
        {
            oneself.ResetHitRecords();

            foreach (HitCollider o in other) o.ResetHitReceived();
        }
    }
}