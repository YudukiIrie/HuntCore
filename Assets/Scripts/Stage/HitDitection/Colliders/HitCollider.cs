using UnityEngine;

namespace Stage.HitDetection
{
    // コライダーの形状
    public enum ColliderShape
    {
        OBB,    // Oriented Bounding Box
        Sphere, // 球体
        Capsule // カプセル
    }

    // コライダーの役割
    public enum ColliderRole
    {
        Body,   // 体の部位
        Weapon, // 武器または攻撃の一部
        Guard,  // 防御状態
        Parry,  // パリィ可能状態
        Roll,   // 回避状態
        None
    }

    /// <summary>
    /// 当たり判定コライダー親クラス
    /// </summary>
    public abstract class HitCollider
    {
        // コライダーの所有者
        public HitInfo Owner {  get; private set; }

        // 形状
        public ColliderShape Shape {  get; private set; }

        // 属性
        public ColliderRole Role { get; private set; }

        // 被攻撃の有無
        public bool WasHit {  get; private set; }

        // 接触相手コライダー
        public HitCollider Other {  get; private set; }

        // 可視化用オブジェクト
        protected VisualCollider _visualCollider;

        // 可視化コライダーサイズ
        Vector3 _scale;

        public HitCollider(HitInfo owner, ColliderShape shape, ColliderRole role, Vector3 scale)
        {
            Owner = owner;
            Shape = shape;
            Role  = role;
            WasHit = false;
            Other  = null;
            _scale = scale;
        }

        public void SetColliderRole(ColliderRole type)
        {
            Role = type;
        }

        public void CreateVisualCollider(GameObject go, Material noHit, Material hit)
        {
            _visualCollider = new VisualCollider(go, _scale, noHit, hit);
        }

        public void RegisterHit(HitCollider other)
        {
            Other = other;
        }

        public void ReceiveHit()
        {
            WasHit = true;
            Owner.ReceiveHit();
        }

        public void ResetHitRecords()
        {
            Other = null;
        }

        public void ResetHitReceived()
        {
            WasHit = false;
            Owner.ResetHitReceived();
        }

        /// <summary>
        /// 情報の更新
        /// </summary>
        /// <param name="transform">更新元Transform</param>
        public abstract void UpdateInfo(Transform transform);
    }
}