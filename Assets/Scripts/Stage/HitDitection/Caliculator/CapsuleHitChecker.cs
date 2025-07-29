using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// カプセル当たり判定計算クラス
    /// </summary>
    public static class CapsuleHitChecker
    {
        /// <summary>
        /// カプセル同士の当たり判定
        /// </summary>
        /// <param name="capsuleA">判定対象カプセル</param>
        /// <param name="capsuleB">判定対象カプセル</param>
        /// <returns>true:接触, false:非接触</returns>
        public static bool IntersectCapsules(HitCapsule capsuleA, HitCapsule capsuleB)
        {
            // === 各カプセル線分間の最短距離を求める ===
            Vector3 bottomA = capsuleA.BottomPoint;
            Vector3 bottomB = capsuleB.BottomPoint;
            Vector3 topA = capsuleA.TopPoint;
            Vector3 topB = capsuleB.TopPoint;
            float dist = CalcSegmentsDist(bottomA, topA, bottomB, topB);

            // === 最短距離と半径との比較 ===
            return dist <= capsuleA.Radius + capsuleB.Radius;
        }

        /// <summary>
        /// 2線分間の距離を求める
        /// </summary>
        /// <param name="bottomA">カプセルAの最下位点</param>
        /// <param name="topA">カプセルAの最上位点</param>
        /// <param name="bottomB">カプセルBの最下位点</param>
        /// <param name="topB">カプセルAの最上位点</param>
        static float CalcSegmentsDist(Vector3 bottomA, Vector3 topA, Vector3 bottomB, Vector3 topB)
        {
            Vector3 hA = Vector3.zero;  // カプセルA側の垂線の足
            Vector3 hB = Vector3.zero;  // カプセルB側の垂線の足
            float   tA = 0.0f;  // カプセルAのベクトル係数
            float   tB = 0.0f;  // カプセルBのベクトル係数

            // 各カプセルの線分を作成
            Vector3 segmentA = bottomA - topA;
            Vector3 segmentB = bottomB - topB;

            float dist = 0.0f;
            // === 2つの線分が平行の場合 ===
            if (CheckSegmentsParallel(segmentA, segmentB))
            {
                // どの点から伸ばしても垂線になるため
                // 垂線の計算を省略し、重なっているかのみ判定
                dist = CalcPointSegmentDist(topA, topB, bottomB, out hB, out tB);
                if (0.0f <= tB && tB <= 1.0f)
                    return dist;
            }
            // === 2つの線分がねじれの関係の場合 ===
            else
            {
                // 平行ではないため直線と直線の最短距離を求める必要がある
                // ベクトル係数が線分からはみ出していなければ線分間の最短距離である
                dist = CalcLinesDist(segmentA, segmentB, topA, topB, out hA, out hB, out tA, out tB);
                if (0.0f <= tA && tA <= 1.0f &&
                    0.0f <= tB && tB <= 1.0f)
                    return dist;
            }

            // === 垂線の足が線分の外側にある場合 ===
            // lineA側の足を0～1にクランプして垂線を降ろす
            tA = Mathf.Clamp01(tA);
            hA = topA + tA * segmentA;
            dist = CalcPointSegmentDist(hA, topB, bottomB, out hB, out tB);
            if (0.0f <= tB && tB <= 1.0f)
                return dist;

            // lineB側の足を0～1にクランプして垂線を下ろす
            tB = Mathf.Clamp01(tB);
            hB = topB + tB * segmentB;
            dist = CalcPointSegmentDist(hB, topA, bottomA, out hA, out tA);
            if (0.0f <= tA && tA <= 1.0f)
                return dist;

            // 双方の端点との距離を最短距離とする
            tA = Mathf.Clamp01(tA);
            hA = topA + tA * segmentA;
            return (hB - hA).magnitude;
        }

        /// <summary>
        /// 2つの線分が平行か確認
        /// </summary>
        /// <param name="segemntA">カプセルAの線分</param>
        /// <param name="segmentB">カプセルBの線分</param>
        /// <returns></returns>
        static bool CheckSegmentsParallel(Vector3 segmentA, Vector3 segmentB)
        {
            Vector3 cross = Vector3.Cross(segmentA, segmentB);
            
            // 外積の結果が限りなく小さい値 =
            // 2つの線分間に角度が付いていない = 平行
            return cross.sqrMagnitude < 1e-6f;
        }

        /// <summary>
        /// 点と線分の最短距離を求める
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="segTop">線分最上位点</param>
        /// <param name="segBottom">線分最下位点</param>
        /// <param name="h">垂線の線分側の足(戻り値)</param>
        /// <param name="t">線分側のベクトル係数(戻り値)</param>
        /// <returns>点と線分の最短距離</returns>
        public static float CalcPointSegmentDist(
            Vector3 point, Vector3 segTop, Vector3 segBottom,
            out Vector3 h, out float t)
        {
            Vector3 segment = segBottom - segTop;

            // 垂線の長さ、垂線の足(線分側)及び線分のベクトル係数tを算出
            float dist = CalcPointLineDist(point, segment, segTop, out h, out t);

            if (!CheckAngleSharpness(point, segTop, segBottom))
            {
                // 鈍角のため点が最上位点の外側
                // つまり、最短距離が垂線ではなく点と端点との距離になる
                h = segTop;
                return (point - segTop).magnitude;
            }
            else if(!CheckAngleSharpness(point, segBottom, segTop))
            {
                h = segBottom;
                return (point - segBottom).magnitude;
            }

            // どちらも鋭角の場合は垂線距離をそのまま返却
            return dist;
        }

        /// <summary>
        /// 点と直線の最短距離を求める
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="line">線分</param>
        /// <param name="lineP">直線上のある点</param>
        /// <param name="h">線分への垂線の足(戻り値)</param>
        /// <param name="t">線分のベクトル係数(戻り値)</param>
        /// <returns>点と直線の最短距離</returns>
        static float CalcPointLineDist(
            Vector3 point, Vector3 line, Vector3 lineP, 
            out Vector3 h, out float t)
        {
            // 最短距離であるpointからlineへの垂線を求めるためには、
            // 足であるh、またhを求めるためのt(lineのベクトル係数)が必要。
            // tを求めるための式(以下pointをp1, lineをv, linePをp2とする)
            // 0 = Dot(v, (p2 + tv) - p1)   vと垂線は垂直であるため値が0
            // 0 = Dot(v, p2) + tDot(v, v) - Dot(v, p1)
            // t = Dot(v, p1) - Dot(v, p2) / Dot(v, v)
            // この式から以下の計算に続く
            t = Vector3.Dot(line, point - lineP) / line.sqrMagnitude;
            h = lineP + (t * line);
            return (h - point).magnitude;
        }

        /// <summary>
        /// 3点が成す角度が鋭角か鈍角かの確認
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <param name="p3">点3</param>
        /// <returns>true:鋭角, false:鈍角</returns>
        static bool CheckAngleSharpness(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float dot = Vector3.Dot(p1 - p2, p3 - p2);

            // 内積結果で角の大きさが分かる
            // 0以上: 鋭角(90度含む), 0未満: 鈍角
            if (dot >= 0.0f)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 2つの直線間の最短距離を求める
        /// </summary>
        /// <param name="lineA">直線A</param>
        /// <param name="lineB">直線B</param>
        /// <param name="pointA">直線Aのある一点</param>
        /// <param name="pointB">直線Bのある一点</param>
        /// <param name="hA">垂線の直線A側の足</param>
        /// <param name="hB">垂線の直線B側の足</param>
        /// <param name="tA">直線Aのベクトル係数</param>
        /// <param name="tB">直線Bのベクトル係数</param>
        /// <returns>2つの直線間の最短距離</returns>
        static float CalcLinesDist(
            Vector3 lineA, Vector3 lineB, Vector3 pointA, Vector3 pointB,
            out Vector3 hA, out Vector3 hB, out float tA, out float tB)
        {
            float dLALB = Vector3.Dot(lineA, lineB);
            float dLALA = lineA.sqrMagnitude;
            float dLBLB = lineB.sqrMagnitude;
            Vector3 pBpA = pointA - pointB;

            // lineAとlineB双方が互いに垂線を引けていると仮定したときの状態でtAを求める。
            // tAが求まればtB, hA, hBも求まるためhAhB(2直線間の最短距離)が求まる。
            // tAを求めるための式(tBはCalcPointLineDist()の式で求まっている)
            // 以下、lineAをv1, lineBをv2, pointAをp1, pointBをp2, tAをt1, tBをt2とする
            // 0 = Dot(v1, hA - hB)
            //   = Dot(v1, p1 + t1 * v1 - p2 - t2 * v2)
            //   = Dot(v1, p1 - p2) + t1Dot(v1, v1) - t2Dot(v1, v2)
            //   = Dot(v1, p1 - p2) + t1Dot(v1, v1) - Dot(v2, p1 + t1v1 - p2) / Dot(v2, v2) *  Dot(v1, v2)
            //   = Dot(v1, p1 - p2) + t1Dot(v1, v1) + {- Dot(v2, p1 - p2) - t1Dot(v2, v1)} / Dot(v2, v2) * Dot(v1, v2)
            //   = Dot(v1, p1 - p2) + t1{Dot(v1, v1) - (Dot(v2, v1) * Dot(v1, v2)) / Dot(v2, v2)} - {(Dot(v2, p1 - p2) * Dot(v1, v2)) / Dot(v2, v2)}
            // t1 = Dot(v2, p1 - p2) * Dot(v1, v2) - Dot(v1, p1 - p2) * Dot(v2, v2) / Dot(v1, v1) * Dot(v2, v2) - Dot(v2, v1) * Dot(v1, v2)
            // 以下の式に続く
            tA = (dLALB * Vector3.Dot(lineB, pBpA) - dLBLB * Vector3.Dot(lineA, pBpA)) / (dLALA * dLBLB - dLALB * dLALB);
            hA = pointA + (tA * lineA);
            tB = Vector3.Dot(lineB, pointA - pointB) / dLBLB;
            hB = pointB + (tB * lineB);

            return (hB - hA).magnitude;
        }
    }
}