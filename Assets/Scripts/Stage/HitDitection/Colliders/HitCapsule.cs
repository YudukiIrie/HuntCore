using Unity.VisualScripting;
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
        public Vector3 ButtomPoint {  get; private set; }

        // �ŏ�ʓ_
        public Vector3 TopPoint { get; private set; }

        // ���a
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