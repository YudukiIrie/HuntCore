using UnityEngine;

namespace Stage.HitDetection
{
    // �R���C�_�[�̌`��
    public enum ColliderShape
    {
        OBB,    // Oriented Bounding Box
        Sphere, // ����
        Capsule // �J�v�Z��
    }

    // �R���C�_�[�̖���
    public enum ColliderRole
    {
        Body,   // �̂̕���
        Weapon, // ����܂��͍U���̈ꕔ
        Guard,  // �h����
        Parry,  // �p���B�\���
        Roll,   // ������
        None
    }

    /// <summary>
    /// �����蔻��R���C�_�[�e�N���X
    /// </summary>
    public abstract class HitCollider
    {
        // �R���C�_�[�̏��L��
        public HitInfo Owner {  get; private set; }

        // �`��
        public ColliderShape Shape {  get; private set; }

        // ����
        public ColliderRole Role { get; private set; }

        // ��U���̗L��
        public bool WasHit {  get; private set; }

        // �ڐG����R���C�_�[
        public HitCollider Other {  get; private set; }

        // �����p�I�u�W�F�N�g
        protected VisualCollider _visualCollider;

        // �����R���C�_�[�T�C�Y
        Vector3 _scale;

        public HitCollider(HitInfo owner, ColliderShape shape, ColliderRole role, Vector3 scale)
        {
            Owner = owner;
            Shape = shape;
            Role  = role;
            WasHit = false;
            Other  = null;
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
            Other = other;
        }

        public void ReceiveHit()
        {
            WasHit = true;
            Owner.ReceiveHit();
        }

        public void ResetHitRecords()
        {
            Other = null;
        }

        public void ResetHitReceived()
        {
            WasHit = false;
            Owner.ResetHitReceived();
        }

        /// <summary>
        /// ���̍X�V
        /// </summary>
        /// <param name="transform">�X�V��Transform</param>
        public abstract void UpdateInfo(Transform transform);
    }
}