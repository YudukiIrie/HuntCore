using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// 球体とOBBの当たり判定計算クラス
    /// </summary>
    public static class SphereOBBHitChecker
    {

        /// <summary>
        /// 球体とOBBの当たり判定
        /// </summary>
        /// <param name="sphere">判定対象球体</param>
        /// <param name="obbs">判定対象OBB</param>
        /// <returns>true:接触, false:非接触</returns>
        public static bool IntersectSphereOBB(HitSphere sphere, OBB obb)
        {
            // 内積の際に使用するベクトルの取得
            Vector3 direction = sphere.Center - obb.Center;

            // 最近接点の元となる点の取得
            Vector3 closestPoint = obb.Center;

            // OBBの各軸に対する検証
            int axisNum = 3;
            for (int i = 0; i < axisNum; ++i)
            {
                // 軸の切り替え
                Vector3 axis = Vector3.zero;
                switch (i)
                {
                    case 0: axis = obb.AxisX; break;
                    case 1: axis = obb.AxisY; break;
                    case 2: axis = obb.AxisZ; break;
                }

                // 内積で出た影の長さを
                // OBB表面上に制限する際に使用する値の取得(各軸に対応する半径)
                float extent = obb.Radius[i];

                // OBBから球へのベクトルを各軸に射影した長さを取得
                float projection = Vector3.Dot(direction, axis);

                // 上記長さを制限
                projection = Mathf.Clamp(projection, -extent, extent);

                // 各軸のprojectionを積み重ねていき
                // 最終的な値を最近接点とする
                closestPoint += axis * projection;
            }

            // 球体と最近接点の距離を取得
            float distance = Vector3.Distance(closestPoint, sphere.Center);

            // 上記で求めた距離が球体の半径より短い場合は接触
            return distance <= sphere.Radius;
        }
    }
}