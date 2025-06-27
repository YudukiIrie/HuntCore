using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBBによる当たり判定
    /// </summary>
    public static class OBBHitChecker
    {
        /// <summary>
        /// コライダー同士の当たり判定
        /// 形状ごとにそれぞれに適したメソッドを呼ぶ
        /// </summary>
        /// <param name="oneself">受動側コライダー</param>
        /// <param name="other">能動側コライダー</param>
        /// <returns>true:接触, false:非接触</returns>
        public static bool IsColliding(HitCollider oneself, List<HitCollider> other)
        {
            var a = oneself;
            foreach (var b in other)
            {
                // 能動側が判定済み、またはOBBが武器の場合は無視
                if (a.HitInfo.didHit || b.Role == HitCollider.ColliderRole.Weapon)
                    return false;

                // === 自身がOBBの場合 ===
                if (a.Shape == HitCollider.ColliderShape.OBB)
                {
                    // 相手がOBBの場合
                    if (b.Shape == HitCollider.ColliderShape.OBB)
                    {
                        if (IsCollideBoxOBB((OBB)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // 相手が球体の場合
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

                // === 自身が球体の場合 ===
                else if (a.Shape == HitCollider.ColliderShape.Sphere)
                {
                    // 相手がOBBの場合
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
        /// 指定したOBB同士の当たり判定
        /// </summary>
        /// <param name="obbA">判定対象OBB</param>
        /// <param name="obbB">判定対象OBB</param>
        /// <returns>true:接触、false:非接触</returns>
        static bool IsCollideBoxOBB(OBB obbA, OBB obbB)
        {
            // 中心間の距離の取得
            Vector3 distance = obbA.Center - obbB.Center;

            // 検証軸を用いた距離の比較
            // Aを検証軸とした場合
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisZ, distance)) return false;
            // 敵を検証軸とした場合
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisZ, distance)) return false;

            // 分離軸同士の外積を用いた距離の比較
            Vector3 cross = Vector3.zero;
            // 武器のX軸との比較
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // 武器のY軸との比較
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // 武器のZ軸との比較
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;

            return true;
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
        /// 球体とOBBの当たり判定
        /// </summary>
        /// <param name="sphere">判定対象球体</param>
        /// <param name="obbs">判定対象OBB</param>
        /// <returns>true:接触, false:非接触</returns>
        static bool IsSphereIntersectingOBB(HitSphere sphere, OBB obb)
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
                switch(i)
                {
                    case 0 : axis = obb.AxisX; break;
                    case 1 : axis = obb.AxisY; break;
                    case 2 : axis = obb.AxisZ; break;
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

        /// <summary>
        /// ヒット情報のリセット
        /// </summary>
        /// <param name="oneself">能動側コライダー</param>
        /// <param name="other">受動側コライダー</param>
        public static void ResetHitInfo(HitCollider oneself, List<HitCollider> other)
        {
            oneself.ResetHitRecords();

            foreach (HitCollider o in other) o.ResetHitReceived();
        }
    }
}
