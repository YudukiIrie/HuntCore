using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// ���̂ƃJ�v�Z���̓����蔻��
    /// </summary>
    public static class SphereCapsuleHitChecker
    {
        /// <summary>
        /// ���̂ƃJ�v�Z���̓����蔻��
        /// </summary>
        /// <param name="sphere">����Ώۋ���</param>
        /// <param name="capsule">����ΏۃJ�v�Z��</param>
        /// <returns>true:�ڐG, false:��ڐG</returns>
        public static bool IntersectSphereCapsule(HitSphere sphere, HitCapsule capsule)
        {
            // === �_(���̂̒��S)�Ɛ���(�J�v�Z��)�̍ŒZ���������߂� ===
            float t = 0.0f;
            Vector3 h = Vector3.zero;
            Vector3 point = sphere.Center;
            Vector3 segTop = capsule.TopPoint;
            Vector3 segBottom = capsule.BottomPoint;

            float dist = CapsuleHitChecker.
                CalcPointSegmentDist(point, segTop, segBottom, out h, out t);

            // === �ŒZ�����Ɣ��a�̔�r ===
            return dist <= sphere.Radius + capsule.Radius;
        }
    }
}