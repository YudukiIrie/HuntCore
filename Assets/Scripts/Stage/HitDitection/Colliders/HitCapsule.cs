using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// �����蔻��p�J�v�Z��
    /// </summary>
    public class HitCapsule : HitCollider
    {
        float _height;  // ����

        // �ŉ��ʓ_
        public Vector3 BottomPoint {  get; private set; }

        // �ŏ�ʓ_
        public Vector3 TopPoint { get; private set; }

        // ���a
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