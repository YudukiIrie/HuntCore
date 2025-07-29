using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// カプセルとOBBの当たり判定
    /// </summary>
    public static class CapsuleOBBHitChecker
    {
        /// <summary>
        /// カプセルとOBBの当たり判定
        /// </summary>
        /// <param name="capsule">判定対象カプセル</param>
        /// <param name="obb">判定対象OBB</param>
        /// <returns>true:接触, false:非接触</returns>
        public static bool IntersectCapsuleOBB(HitCapsule capsule, OBB obb)
        {
            // カプセル線分の作成
            Vector3 segment = capsule.BottomPoint - capsule.TopPoint;

            // 各分離軸を列挙
            const int axisNum = 7;
            Vector3[] axes = new Vector3[axisNum];
            axes[0] = obb.AxisX;
            axes[1] = obb.AxisY;
            axes[2] = obb.AxisZ;
            axes[3] = segment;
            axes[4] = Vector3.Cross(segment, axes[0]);
            axes[5] = Vector3.Cross(segment, axes[1]);
            axes[6] = Vector3.Cross(segment, axes[2]);

            // 各軸に対しての検証
            foreach(Vector3 axis in axes)
            {
                Vector3 nAxis = axis.normalized;

                // === カプセルの投影範囲の取得 ===
                float minC, maxC;
                ProjectCapsule(capsule, nAxis, out minC, out maxC);

                // === OBB投影範囲の取得 ===
                float minB, maxB;
                ProjectOBB(obb, nAxis, out minB, out maxB);

                // === 各投影範囲が重なっているか確認 ===
                if (maxC < minB || maxB < minC)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 検証軸に対してのカプセル投影
        /// </summary>
        /// <param name="capsule">投影カプセル</param>
        /// <param name="axis">検証軸</param>
        /// <param name="minC">投影最小値</param>
        /// <param name="maxC">投影最大値</param>
        static void ProjectCapsule(
            HitCapsule capsule, Vector3 axis, out float minC, out float maxC)
        {
            // カプセル線分の端点を投影
            float dotA = Vector3.Dot(capsule.TopPoint, axis);
            float dotB = Vector3.Dot(capsule.BottomPoint, axis);

            // 投影範囲の最大、最小値の取得
            minC = Mathf.Min(dotA, dotB) - capsule.Radius;
            maxC = Mathf.Max(dotA, dotB) + capsule.Radius;
        }

        static void ProjectOBB(OBB obb, Vector3 axis, out float minB, out float maxB)
        {
            float center = Vector3.Dot(obb.Center, axis);

            // 軸に対してOBBの影を降ろした際の半径を取得
            float r = 
                Mathf.Abs(Vector3.Dot(obb.AxisX, axis)) * obb.Radius[0] + 
                Mathf.Abs(Vector3.Dot(obb.AxisY, axis)) * obb.Radius[1] + 
                Mathf.Abs(Vector3.Dot(obb.AxisZ, axis)) * obb.Radius[2];

            // 投影範囲の最大、最小値の取得
            minB = center - r;
            maxB = center + r;
        }
    }
}