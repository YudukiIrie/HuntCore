using UnityEngine;
using System.Collections.Generic;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB���m�ɂ�铖���蔻��
    /// </summary>
    public static class OBBHitChecker
    {
        /// <summary>
        /// �w�肵��OBB���m�̓����蔻��
        /// </summary>
        /// <param name="obbA">�\���IOBB</param>
        /// <param name="obbBs">�󓮓IOBB</param>
        /// <returns>true:�ڐG�Afalse:��ڐG</returns>
        public static bool IsCollideBoxOBB(OBB obbA, List<OBB> obbBs)
        {
            foreach (OBB obbB in obbBs)
            {
                // ����ς݂�OBB�͖���
                if (obbA.IsHit) break;
                
                // ���S�Ԃ̋����̎擾
                Vector3 distance = obbA.Center - obbB.Center;

                // ���؎���p���������̔�r
                // A�����؎��Ƃ����ꍇ
                if (!CompareLengthOBB(obbA, obbB, obbA.AxisX, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbA.AxisY, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbA.AxisZ, distance)) continue;
                // �G�����؎��Ƃ����ꍇ
                if (!CompareLengthOBB(obbA, obbB, obbB.AxisX, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbB.AxisY, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbB.AxisZ, distance)) continue;

                // ���������m�̊O�ς�p���������̔�r
                Vector3 cross = Vector3.zero;
                // �����X���Ƃ̔�r
                cross = Vector3.Cross(obbA.AxisX, obbB.AxisX);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisX, obbB.AxisY);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisX, obbB.AxisZ);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                // �����Y���Ƃ̔�r
                cross = Vector3.Cross(obbA.AxisY, obbB.AxisX);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisY, obbB.AxisY);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisY, obbB.AxisZ);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                // �����Z���Ƃ̔�r
                cross = Vector3.Cross(obbA.AxisZ, obbB.AxisX);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisZ, obbB.AxisY);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisZ, obbB.AxisZ);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;

                // �ڐG�ς�OBB�̏����X�V
                obbA.Hit();
                obbB.Hit();
      
                return true;
            }

            return false;
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
        /// �q�b�g���̃��Z�b�g
        /// </summary>
        /// <param name="obbA">�\���IOBB</param>
        /// <param name="obbBs">�󓮓IOBB</param>
        public static void ResetHitInfo(OBB obbA, List<OBB> obbBs)
        {
            obbA.ResetHitInfo();

            foreach (OBB obbB in obbBs) obbB.ResetHitInfo();
        }
    }
}
