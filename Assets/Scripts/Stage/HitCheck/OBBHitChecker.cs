using UnityEngine;
using System.Collections.Generic;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB同士による当たり判定
    /// </summary>
    public static class OBBHitChecker
    {
        /// <summary>
        /// 指定したOBB同士の当たり判定
        /// </summary>
        /// <param name="obbA">能動的OBB</param>
        /// <param name="obbBs">受動的OBB</param>
        /// <returns>true:接触、false:非接触</returns>
        public static bool IsCollideBoxOBB(OBB obbA, List<OBB> obbBs)
        {
            foreach (OBB obbB in obbBs)
            {
                // 判定済みのOBBは無視
                if (obbA.IsHit) break;
                
                // 中心間の距離の取得
                Vector3 distance = obbA.Center - obbB.Center;

                // 検証軸を用いた距離の比較
                // Aを検証軸とした場合
                if (!CompareLengthOBB(obbA, obbB, obbA.AxisX, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbA.AxisY, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbA.AxisZ, distance)) continue;
                // 敵を検証軸とした場合
                if (!CompareLengthOBB(obbA, obbB, obbB.AxisX, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbB.AxisY, distance)) continue;
                if (!CompareLengthOBB(obbA, obbB, obbB.AxisZ, distance)) continue;

                // 分離軸同士の外積を用いた距離の比較
                Vector3 cross = Vector3.zero;
                // 武器のX軸との比較
                cross = Vector3.Cross(obbA.AxisX, obbB.AxisX);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisX, obbB.AxisY);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisX, obbB.AxisZ);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                // 武器のY軸との比較
                cross = Vector3.Cross(obbA.AxisY, obbB.AxisX);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisY, obbB.AxisY);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisY, obbB.AxisZ);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                // 武器のZ軸との比較
                cross = Vector3.Cross(obbA.AxisZ, obbB.AxisX);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisZ, obbB.AxisY);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;
                cross = Vector3.Cross(obbA.AxisZ, obbB.AxisZ);
                if (!CompareLengthOBB(obbA, obbB, cross, distance)) continue;

                // 接触済みOBBの情報を更新
                obbA.Hit();
                obbB.Hit();
      
                return true;
            }

            return false;
        }

        /// <summary>
        /// 指定した武器OBBと敵OBBの距離比較
        /// </summary>
        /// <param name="separating">指定した分離軸</param>
        /// <param name="distance">2点間の距離</param>
        /// <returns></returns>
        static bool CompareLengthOBB(OBB obbA, OBB obbB, Vector3 separating, Vector3 distance)
        {
            // 検証軸上の武器と敵の距離
            // マイナスのベクトルである可能性があるため絶対値化
            float length = Mathf.Abs(Vector3.Dot(separating, distance));

            // 特定の検証軸上における武器の距離の半分を求める
            float weaponDist =
                Mathf.Abs(Vector3.Dot(obbA.AxisX, separating)) * obbA.Radius.x +
                Mathf.Abs(Vector3.Dot(obbA.AxisY, separating)) * obbA.Radius.y +
                Mathf.Abs(Vector3.Dot(obbA.AxisZ, separating)) * obbA.Radius.z;

            // 特定の検証軸上における敵の距離の半分を求める
            float enemyDist =
                Mathf.Abs(Vector3.Dot(obbB.AxisX, separating)) * obbB.Radius.x +
                Mathf.Abs(Vector3.Dot(obbB.AxisY, separating)) * obbB.Radius.y +
                Mathf.Abs(Vector3.Dot(obbB.AxisZ, separating)) * obbB.Radius.z;

            // 武器と敵の距離と、上記の距離の合計を比較
            if (length > weaponDist + enemyDist)
                return false;

            return true;
        }

        /// <summary>
        /// ヒット情報のリセット
        /// </summary>
        /// <param name="obbA">能動的OBB</param>
        /// <param name="obbBs">受動的OBB</param>
        public static void ResetHitInfo(OBB obbA, List<OBB> obbBs)
        {
            obbA.ResetHitInfo();

            foreach (OBB obbB in obbBs) obbB.ResetHitInfo();
        }
    }
}
