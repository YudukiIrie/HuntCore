using System.Buffers;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// 当たり判定コライダー親クラス
    /// </summary>
    public abstract class HitCollider
    {
        // コライダーの所有者
        public HitInfo Owner {  get; private set; }

        // コライダーの形状
        public enum ColliderShape
        {
            OBB,    // Oriented Bounding Box
            Sphere, // 球体
            Capsule // カプセル
        }
        public ColliderShape Shape {  get; private set; }

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
        public ColliderRole Role { get; private set; }

        // ヒット情報
        public struct HitInformation
        {
            public bool wasHit;              // ヒットさせられたかどうか
            public HitCollider other;        // 接触相手のコライダー

            public HitInformation(bool didHit, bool wasHit)
            {
                this.wasHit = wasHit;
                other = null;
            }

            /// <summary>
            /// ヒット情報の登録(能動側)
            /// </summary>
            /// <param name="other">相手コライダー</param>
            public void RegisterHit(HitCollider other)
            {
                this.other = other;
            }

            /// <summary>
            /// ヒット情報の受け取り(受動側)
            /// </summary>
            public void ReceiveHit()
            {
                wasHit = true;
            }

            /// <summary>
            /// ヒット情報のリセット(能動側)
            /// </summary>
            public void ResetHitRecords()
            {
                other = null;
            }

            /// <summary>
            /// ヒット情報のリセット(受動側)
            /// </summary>
            public void ResetHitReceived()
            {
                wasHit = false;
            }
        }
        public HitInformation HitInfo { get; private set; }

        // 可視化用オブジェクト
        protected VisualCollider _visualCollider;
        // 可視化コライダーサイズ
        Vector3 _scale;

        public HitCollider(HitInfo owner, ColliderShape shape, ColliderRole role, Vector3 scale)
        {
            Owner = owner;
            Shape = shape;
            Role  = role;
            HitInfo = new HitInformation(false, false);
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
            HitInformation info = HitInfo;
            info.RegisterHit(other);
            HitInfo = info;
        }

        public void ReceiveHit()
        {
            HitInformation info = HitInfo;
            info.ReceiveHit();
            HitInfo = info;

            Owner.ReceiveHit();
        }

        public void ResetHitRecords()
        {
            HitInformation info = HitInfo;
            info.ResetHitRecords();
            HitInfo = info;
        }

        public void ResetHitReceived()
        {
            HitInformation info = HitInfo;
            info.ResetHitReceived();
            HitInfo = info;

            Owner.ResetHitReceived();
        }

        /// <summary>
        /// 情報の更新
        /// </summary>
        /// <param name="transform">更新元Transform</param>
        public abstract void UpdateInfo(Transform transform);
    }
}