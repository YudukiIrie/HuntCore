using System.Collections.Generic;

namespace Stage.HitDetection
{
    /// <summary>
    /// 当たり判定
    /// </summary>
    public static class HitChecker
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
                // 受動側の所有者が判定済み、
                // またはコライダーが武器・回避の場合は無視
                if (b.Owner.WasHit ||
                    b.Role == HitCollider.ColliderRole.Weapon ||
                    b.Role == HitCollider.ColliderRole.Roll)
                    return false;

                // === 自身がOBBの場合 ===
                if (a.Shape == HitCollider.ColliderShape.OBB)
                {
                    // 相手がOBBの場合
                    if (b.Shape == HitCollider.ColliderShape.OBB)
                    {
                        if (OBBHitChecker.IntersectOBBs((OBB)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // 相手が球体の場合
                    else if (b.Shape == HitCollider.ColliderShape.Sphere)
                    {
                        if (SphereOBBHitChecker.IntersectSphereOBB((HitSphere)b, (OBB)a))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // 相手がカプセルの場合
                    else if (b.Shape == HitCollider.ColliderShape.Capsule)
                    {
                        if (CapsuleOBBHitChecker.IntersectCapsuleOBB((HitCapsule)b, (OBB)a))
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
                        if (SphereOBBHitChecker.IntersectSphereOBB((HitSphere)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // 相手がカプセルの場合
                    if (b.Shape == HitCollider.ColliderShape.Capsule)
                    {
                        if (SphereCapsuleHitChecker.IntersectSphereCapsule((HitSphere)a, (HitCapsule)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                }

                // === 自身がカプセルの場合 ===
                else if (a.Shape == HitCollider.ColliderShape.Capsule)
                {
                    // 相手がOBBの場合
                    if (b.Shape == HitCollider.ColliderShape.OBB)
                    {
                        if (CapsuleOBBHitChecker.IntersectCapsuleOBB((HitCapsule)a, (OBB)b))
                        {
                            a.RegisterHit(b);
                            b.ReceiveHit();
                            return true;
                        }
                    }
                    // 相手がカプセルの場合
                    else if (b.Shape == HitCollider.ColliderShape.Capsule)
                    {
                        if (CapsuleHitChecker.IntersectCapsules((HitCapsule)a, (HitCapsule)b))
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