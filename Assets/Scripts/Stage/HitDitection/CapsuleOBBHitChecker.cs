using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// �J�v�Z����OBB�̓����蔻��
    /// </summary>
    public static class CapsuleOBBHitChecker
    {
        /// <summary>
        /// �J�v�Z����OBB�̓����蔻��
        /// </summary>
        /// <param name="capsule">����ΏۃJ�v�Z��</param>
        /// <param name="obb">����Ώ�OBB</param>
        /// <returns>true:�ڐG, false:��ڐG</returns>
        public static bool IntersectCapsuleOBB(HitCapsule capsule, OBB obb)
        {
            // �J�v�Z�������̍쐬
            Vector3 segment = capsule.BottomPoint - capsule.TopPoint;

            // �e���������
            const int axisNum = 7;
            Vector3[] axes = new Vector3[axisNum];
            axes[0] = obb.AxisX;
            axes[1] = obb.AxisY;
            axes[2] = obb.AxisZ;
            axes[3] = segment;
            axes[4] = Vector3.Cross(segment, axes[0]);
            axes[5] = Vector3.Cross(segment, axes[1]);
            axes[6] = Vector3.Cross(segment, axes[2]);

            // �e���ɑ΂��Ă̌���
            foreach(Vector3 axis in axes)
            {
                Vector3 nAxis = axis.normalized;

                // === �J�v�Z���̓��e�͈͂̎擾 ===
                float minC, maxC;
                ProjectCapsule(capsule, nAxis, out minC, out maxC);

                // === OBB���e�͈͂̎擾 ===
                float minB, maxB;
                ProjectOBB(obb, nAxis, out minB, out maxB);

                // === �e���e�͈͂��d�Ȃ��Ă��邩�m�F ===
                if (maxC < minB || maxB < minC)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// ���؎��ɑ΂��ẴJ�v�Z�����e
        /// </summary>
        /// <param name="capsule">���e�J�v�Z��</param>
        /// <param name="axis">���؎�</param>
        /// <param name="minC">���e�ŏ��l</param>
        /// <param name="maxC">���e�ő�l</param>
        static void ProjectCapsule(
            HitCapsule capsule, Vector3 axis, out float minC, out float maxC)
        {
            // �J�v�Z�������̒[�_�𓊉e
            float dotA = Vector3.Dot(capsule.TopPoint, axis);
            float dotB = Vector3.Dot(capsule.BottomPoint, axis);

            // ���e�͈͂̍ő�A�ŏ��l�̎擾
            minC = Mathf.Min(dotA, dotB) - capsule.Radius;
            maxC = Mathf.Max(dotA, dotB) + capsule.Radius;
        }

        static void ProjectOBB(OBB obb, Vector3 axis, out float minB, out float maxB)
        {
            float center = Vector3.Dot(obb.Center, axis);

            // ���ɑ΂���OBB�̉e���~�낵���ۂ̔��a���擾
            float r = 
                Mathf.Abs(Vector3.Dot(obb.AxisX, axis)) * obb.Radius[0] + 
                Mathf.Abs(Vector3.Dot(obb.AxisY, axis)) * obb.Radius[1] + 
                Mathf.Abs(Vector3.Dot(obb.AxisZ, axis)) * obb.Radius[2];

            // ���e�͈͂̍ő�A�ŏ��l�̎擾
            minB = center - r;
            maxB = center + r;
        }
    }
}