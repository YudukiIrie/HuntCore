using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// �����蔻��p����
    /// </summary>
    public class HitSphere : HitCollider
    {
        // ���S���W
        public Vector3 Center { get; private set; }

        // ���a
        public float Radius { get; private set; }

        public HitSphere(
            Vector3 center, float radius, 
            ColliderShape shape, ColliderRole type) 
            : base(shape, type, new Vector3(radius,radius,radius) * 2)
        {
            Center = center;
            Radius = radius;
        }

        public void UpdateInfo(Transform transform)
        {
            Center = transform.position;

            _visualCollider.UpdateInfo(transform, HitInfo.wasHit);
        }
    }
}