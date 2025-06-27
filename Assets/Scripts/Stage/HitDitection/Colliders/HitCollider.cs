using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// �����蔻��R���C�_�[�e�N���X
    /// </summary>
    public class HitCollider
    {
        // �R���C�_�[�̌`��
        public enum ColliderShape
        {
            OBB,    // Oriented Bounding Box
            Sphere, // ����
        }
        public ColliderShape Shape {  get; private set; }

        // �R���C�_�[�̖���
        public enum ColliderRole
        {
            Body,   // �̂̕���
            Weapon, // ����܂��͍U���̈ꕔ
            Guard,  // �h����
            None
        }
        public ColliderRole Role { get; private set; }

        // �q�b�g���
        public struct HitInformation
        {
            public bool didHit;              // �q�b�g���������ǂ���
            public bool wasHit;              // �q�b�g������ꂽ���ǂ���
            public ColliderRole targetRole;  // �ڐG�����ColliderType

            public HitInformation(bool didHit, bool wasHit, ColliderRole role)
            {
                this.didHit = didHit;
                this.wasHit = wasHit;
                this.targetRole = role;
            }

            /// <summary>
            /// �q�b�g���̓o�^(�\����)
            /// </summary>
            /// <param name="other">����R���C�_�[</param>
            public void RegisterHit(HitCollider other)
            {
                didHit = true;
                targetRole = other.Role;
            }

            /// <summary>
            /// �q�b�g���̎󂯎��(�󓮑�)
            /// </summary>
            public void ReceiveHit()
            {
                wasHit = true;
            }

            /// <summary>
            /// �q�b�g���̃��Z�b�g(�\����)
            /// </summary>
            public void ResetHitRecords()
            {
                didHit = false;
                targetRole = ColliderRole.None;
            }

            /// <summary>
            /// �q�b�g���̃��Z�b�g(�󓮑�)
            /// </summary>
            public void ResetHitReceived()
            {
                wasHit = false;
            }
        }
        public HitInformation HitInfo { get; private set; }

        // �����p�I�u�W�F�N�g
        protected VisualCollider _visualCollider;
        // �����R���C�_�[�T�C�Y
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