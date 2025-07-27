using Unity.VisualScripting;
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
        public Vector3 ButtomPoint {  get; private set; }

        // 最上位点
        public Vector3 TopPoint { get; private set; }

        // 半径
        public float Radius { get; private set; }

        public HitCapsule(
            Transform transform, float height, float radius,
            ColliderShape shpae, ColliderRole role) 
            : base(shpae, role, new Vector3(radius, height, radius))
        {
            _height = height;
            ButtomPoint = transform.up * -(height / 2);
            TopPoint = transform.up * (height / 2);
            Radius = radius;
        }

        public void UpdateInfo(Transform transform)
        {
            ButtomPoint = transform.up * -(_height / 2);
            TopPoint = transform.up * (_height / 2);

            _visualCollider.UpdateInfo(transform, HitInfo.wasHit);
        }
    }
}