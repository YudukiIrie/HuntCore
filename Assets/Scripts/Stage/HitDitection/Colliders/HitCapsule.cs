using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// 当たり判定用カプセル
    /// </summary>
    public class HitCapsule : HitCollider
    {
        float _height;  // 高さ

        // 最下位点
        public Vector3 BottomPoint {  get; private set; }

        // 最上位点
        public Vector3 TopPoint { get; private set; }

        // 半径
        public float Radius { get; private set; }

        public HitCapsule(HitInfo owner,
            Transform transform, float height, float radius,
            ColliderShape shpae, ColliderRole role) 
            : base(owner, shpae, role, new Vector3(radius * 2, height / 2, radius * 2), transform.position)
        {
            _height = height;
            BottomPoint = transform.position - (transform.up * (height / 2 - radius));
            TopPoint = transform.position + (transform.up * (height / 2 - radius));
            Radius = radius;
        }

        public override void UpdateInfo(Transform transform)
        {
            BottomPoint = transform.position - (transform.up * (_height / 2 - Radius));
            TopPoint = transform.position + (transform.up * (_height / 2 - Radius));

            _position = transform.position;

            _visualCollider?.UpdateInfo(transform, WasHit);
        }
    }
}