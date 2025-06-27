using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// �e�I�u�W�F�N�gOBB���
    /// </summary>
    public class OBB : HitCollider
    {
        // ���S���W
        public Vector3 Center { get; private set; }

        // ��]�l
        public Quaternion Rotation { get; private set; }

        // ������
        public Vector3 AxisX { get; private set; }
        public Vector3 AxisY { get; private set; }
        public Vector3 AxisZ { get; private set; }

        // ���S����XYZ���ʂ܂ł̒���(���a)
        public Vector3 Radius { get; private set; }

        public OBB(
            Transform transform, Vector3 size,
            ColliderShape shape, ColliderRole type) : base(shape, type, size)
        {
            Center   = transform.position;
            Rotation = transform.rotation;
            AxisX    = transform.right;
            AxisY    = transform.up;
            AxisZ    = transform.forward;
            Radius   = size * 0.5f;
        }

        public void UpdateInfo(Transform transform)
        {
            Center = transform.position;
            Rotation = transform.rotation;

            AxisX = transform.right;
            AxisY = transform.up;
            AxisZ = transform.forward;

            _visualCollider.UpdateInfo(transform, HitInfo.wasHit);
        }
    }
}
