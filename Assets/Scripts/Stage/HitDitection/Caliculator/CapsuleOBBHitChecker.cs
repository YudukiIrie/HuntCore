using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// �J�v�Z����OBB�̓����蔻��v�Z�N���X
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
            // OBB����A�J�v�Z��������̌��ؑΏۂ̓_�ւ̃x�N�g�����擾
            const int directionNum = 3;
            Vector3[] direction = new Vector3[directionNum];
            direction[0] = capsule.Center - obb.Center;
            direction[1] = capsule.TopPoint - obb.Center;
            direction[2] = capsule.BottomPoint - obb.Center;

            // ���񖢎g�p�̐����֘A�̕ϐ�(�J�v�Z�����m�̓����蔻��Q��)
            Vector3 h;
            float t;

            // �e���ւ̌���
            for (int i = 0; i < directionNum; ++i)
            {
                // �J�v�Z��������̂����_��OBB�Ƃ̍ŋߐړ_��OBB�����ɍ쐬
                Vector3 closestPoint = 
                    SphereOBBHitChecker.CalcClosestPointInOBB(direction[i], obb);

                // ��L�̓_�ƃJ�v�Z�������Ƃ̍ŒZ�������Z�o
                float dist = 
                    CapsuleHitChecker.CalcPointSegmentDist(
                    closestPoint, capsule.TopPoint, capsule.BottomPoint, out h, out t);

                // ��L�̋����ƃJ�v�Z�����a�Ƃ̔�r
                if (dist <= capsule.Radius)
                    return true;
            }

            return false;
        }
    }
}