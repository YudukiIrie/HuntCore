using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB�ɂ�铖���蔻��
    /// </summary>
    public static class OBBHitChecker
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
                // �\����������ς݁A�܂���OBB������̏ꍇ�͖���
                if (a.HitInfo.didHit || b.Role == HitCollider.ColliderRole.Weapon)
                    return false;

                // === ���g��OBB�̏ꍇ ===
                if (a.Shape == HitCollider.ColliderShape.OBB)
                {
                    // ���肪OBB�̏ꍇ
                    if (b.Shape == HitCollider.ColliderShape.OBB)
                    {
                        if (IsCollideBoxOBB((OBB)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // ���肪���̂̏ꍇ
                    else if (b.Shape == HitCollider.ColliderShape.Sphere)
                    {
                        if (IsSphereIntersectingOBB((HitSphere)b, (OBB)a))
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
                        if (IsSphereIntersectingOBB((HitSphere)a, (OBB)b))
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
        /// �w�肵��OBB���m�̓����蔻��
        /// </summary>
        /// <param name="obbA">����Ώ�OBB</param>
        /// <param name="obbB">����Ώ�OBB</param>
        /// <returns>true:�ڐG�Afalse:��ڐG</returns>
        static bool IsCollideBoxOBB(OBB obbA, OBB obbB)
        {
            // ���S�Ԃ̋����̎擾
            Vector3 distance = obbA.Center - obbB.Center;

            // ���؎���p���������̔�r
            // A�����؎��Ƃ����ꍇ
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisZ, distance)) return false;
            // �G�����؎��Ƃ����ꍇ
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisZ, distance)) return false;

            // ���������m�̊O�ς�p���������̔�r
            Vector3 cross = Vector3.zero;
            // �����X���Ƃ̔�r
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // �����Y���Ƃ̔�r
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // �����Z���Ƃ̔�r
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;

            return true;
        }

        /// <summary>
        /// �w�肵������OBB�ƓGOBB�̋�����r
        /// </summary>
        /// <param name="separating">�w�肵��������</param>
        /// <param name="distance">2�_�Ԃ̋���</param>
        /// <returns></returns>
        static bool CompareLengthOBB(OBB obbA, OBB obbB, Vector3 separating, Vector3 distance)
        {
            // ���؎���̕���ƓG�̋���
            // �}�C�i�X�̃x�N�g���ł���\�������邽�ߐ�Βl��
            float length = Mathf.Abs(Vector3.Dot(separating, distance));

            // ����̌��؎���ɂ����镐��̋����̔��������߂�
            float weaponDist =
                Mathf.Abs(Vector3.Dot(obbA.AxisX, separating)) * obbA.Radius.x +
                Mathf.Abs(Vector3.Dot(obbA.AxisY, separating)) * obbA.Radius.y +
                Mathf.Abs(Vector3.Dot(obbA.AxisZ, separating)) * obbA.Radius.z;

            // ����̌��؎���ɂ�����G�̋����̔��������߂�
            float enemyDist =
                Mathf.Abs(Vector3.Dot(obbB.AxisX, separating)) * obbB.Radius.x +
                Mathf.Abs(Vector3.Dot(obbB.AxisY, separating)) * obbB.Radius.y +
                Mathf.Abs(Vector3.Dot(obbB.AxisZ, separating)) * obbB.Radius.z;

            // ����ƓG�̋����ƁA��L�̋����̍��v���r
            if (length > weaponDist + enemyDist)
                return false;

            return true;
        }

        /// <summary>
        /// ���̂�OBB�̓����蔻��
        /// </summary>
        /// <param name="sphere">����Ώۋ���</param>
        /// <param name="obbs">����Ώ�OBB</param>
        /// <returns>true:�ڐG, false:��ڐG</returns>
        static bool IsSphereIntersectingOBB(HitSphere sphere, OBB obb)
        {
            // ���ς̍ۂɎg�p����x�N�g���̎擾
            Vector3 direction = sphere.Center - obb.Center;

            // �ŋߐړ_�̌��ƂȂ�_�̎擾
            Vector3 closestPoint = obb.Center;

            // OBB�̊e���ɑ΂��錟��
            int axisNum = 3;
            for (int i = 0; i < axisNum; ++i)
            {
                // ���̐؂�ւ�
                Vector3 axis = Vector3.zero;
                switch(i)
                {
                    case 0 : axis = obb.AxisX; break;
                    case 1 : axis = obb.AxisY; break;
                    case 2 : axis = obb.AxisZ; break;
                }

                // ���ςŏo���e�̒�����
                // OBB�\�ʏ�ɐ�������ۂɎg�p����l�̎擾(�e���ɑΉ����锼�a)
                float extent = obb.Radius[i];

                // OBB���狅�ւ̃x�N�g�����e���Ɏˉe�����������擾
                float projection = Vector3.Dot(direction, axis);

                // ��L�����𐧌�
                projection = Mathf.Clamp(projection, -extent, extent);

                // �e����projection��ςݏd�˂Ă���
                // �ŏI�I�Ȓl���ŋߐړ_�Ƃ���
                closestPoint += axis * projection;
            }

            // ���̂ƍŋߐړ_�̋������擾
            float distance = Vector3.Distance(closestPoint, sphere.Center);

            // ��L�ŋ��߂����������̂̔��a���Z���ꍇ�͐ڐG
            return distance <= sphere.Radius;
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
