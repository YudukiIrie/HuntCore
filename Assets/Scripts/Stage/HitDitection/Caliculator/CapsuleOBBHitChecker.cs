using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// カプセルとOBBの当たり判定計算クラス
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
            // OBBから、カプセル線分上の検証対象の点へのベクトルを取得
            const int directionNum = 3;
            Vector3[] direction = new Vector3[directionNum];
            direction[0] = capsule.Center - obb.Center;
            direction[1] = capsule.TopPoint - obb.Center;
            direction[2] = capsule.BottomPoint - obb.Center;

            // 今回未使用の垂線関連の変数(カプセル同士の当たり判定参照)
            Vector3 h;
            float t;

            // 各軸への検証
            for (int i = 0; i < directionNum; ++i)
            {
                // カプセル線分上のある一点とOBBとの最近接点をOBB内部に作成
                Vector3 closestPoint = 
                    SphereOBBHitChecker.CalcClosestPointInOBB(direction[i], obb);

                // 上記の点とカプセル線分との最短距離を算出
                float dist = 
                    CapsuleHitChecker.CalcPointSegmentDist(
                    closestPoint, capsule.TopPoint, capsule.BottomPoint, out h, out t);

                // 上記の距離とカプセル半径との比較
                if (dist <= capsule.Radius)
                    return true;
            }

            return false;
        }
    }
}