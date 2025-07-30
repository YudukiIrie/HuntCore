using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// “–‚½‚è”»’è—p‹…‘Ì
    /// </summary>
    public class HitSphere : HitCollider
    {
        // ’†SÀ•W
        public Vector3 Center { get; private set; }

        // ”¼Œa
        public float Radius { get; private set; }

        public HitSphere(HitInfo owner,
            Vector3 center, float radius, 
            ColliderShape shape, ColliderRole type) 
            : base(owner, shape, type, new Vector3(radius,radius,radius) * 2)
        {
            Center = center;
            Radius = radius;
        }

        public override void UpdateInfo(Transform transform)
        {
            Center = transform.position;

            _visualCollider.UpdateInfo(transform, HitInfo.wasHit);
        }
    }
}