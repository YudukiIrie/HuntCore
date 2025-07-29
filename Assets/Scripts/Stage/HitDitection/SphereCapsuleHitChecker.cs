using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// 球体とカプセルの当たり判定
    /// </summary>
    public static class SphereCapsuleHitChecker
    {
        /// <summary>
        /// 球体とカプセルの当たり判定
        /// </summary>
        /// <param name="sphere">判定対象球体</param>
        /// <param name="capsule">判定対象カプセル</param>
        /// <returns>true:接触, false:非接触</returns>
        public static bool IntersectSphereCapsule(HitSphere sphere, HitCapsule capsule)
        {
            // === 点(球体の中心)と線分(カプセル)の最短距離を求める ===
            float t = 0.0f;
            Vector3 h = Vector3.zero;
            Vector3 point = sphere.Center;
            Vector3 segTop = capsule.TopPoint;
            Vector3 segBottom = capsule.BottomPoint;

            float dist = CapsuleHitChecker.
                CalcPointSegmentDist(point, segTop, segBottom, out h, out t);

            // === 最短距離と半径の比較 ===
            return dist <= sphere.Radius + capsule.Radius;
        }
    }
}