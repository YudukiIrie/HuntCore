using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// 各オブジェクトOBB情報
    /// </summary>
    public class OBB : HitCollider
    {
        // 中心座標
        public Vector3 Center { get; private set; }

        // 回転値
        public Quaternion Rotation { get; private set; }

        // 分離軸
        public Vector3 AxisX { get; private set; }
        public Vector3 AxisY { get; private set; }
        public Vector3 AxisZ { get; private set; }

        // 中心からXYZ平面までの長さ(半径)
        public Vector3 Radius { get; private set; }

        public OBB(HitInfo owner,
            Transform transform, Vector3 size,
            ColliderShape shape, ColliderRole type) : base(owner, shape, type, size, transform.position)
        {
            Center   = transform.position;
            Rotation = transform.rotation;
            AxisX    = transform.right;
            AxisY    = transform.up;
            AxisZ    = transform.forward;
            Radius   = size * 0.5f;
        }

        public override void UpdateInfo(Transform transform)
        {
            Center = transform.position;
            Rotation = transform.rotation;

            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;

            _position = transform.position;

            _visualCollider.UpdateInfo(transform, WasHit);
        }
    }
}
