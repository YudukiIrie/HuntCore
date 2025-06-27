using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// 当たり判定コライダー親クラス
    /// </summary>
    public class HitCollider
    {
        // コライダーの形状
        public enum ColliderShape
        {
            OBB,    // Oriented Bounding Box
            Sphere, // 球体
        }
        public ColliderShape Shape {  get; private set; }

        // コライダーの役割
        public enum ColliderRole
        {
            Body,   // 体の部位
            Weapon, // 武器または攻撃の一部
            Guard,  // 防御状態
            None
        }
        public ColliderRole Role { get; private set; }

        // ヒット情報
        public struct HitInformation
        {
            public bool didHit;              // ヒットさせたかどうか
            public bool wasHit;              // ヒットさせられたかどうか
            public ColliderRole targetRole;  // 接触相手のColliderType

            public HitInformation(bool didHit, bool wasHit, ColliderRole role)
            {
                this.didHit = didHit;
                this.wasHit = wasHit;
                this.targetRole = role;
            }

            /// <summary>
            /// ヒット情報の登録(能動側)
            /// </summary>
            /// <param name="other">相手コライダー</param>
            public void RegisterHit(HitCollider other)
            {
                didHit = true;
                targetRole = other.Role;
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
                didHit = false;
                targetRole = ColliderRole.None;
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

        public HitCollider(ColliderShape shape, ColliderRole type, Vector3 scale)
        {
            Shape = shape;
            Role  = type;
            HitInfo = new HitInformation(false, false, ColliderRole.None);
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
        }
    }
}