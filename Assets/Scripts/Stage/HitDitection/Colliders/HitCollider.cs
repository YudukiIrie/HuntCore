using System.Buffers;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// �����蔻��R���C�_�[�e�N���X
    /// </summary>
    public abstract class HitCollider
    {
        // �R���C�_�[�̏��L��
        public HitInfo Owner {  get; private set; }

        // �R���C�_�[�̌`��
        public enum ColliderShape
        {
            OBB,    // Oriented Bounding Box
            Sphere, // ����
            Capsule // �J�v�Z��
        }
        public ColliderShape Shape {  get; private set; }

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
        public ColliderRole Role { get; private set; }

        // �q�b�g���
        public struct HitInformation
        {
            public bool wasHit;              // �q�b�g������ꂽ���ǂ���
            public HitCollider other;        // �ڐG����̃R���C�_�[

            public HitInformation(bool didHit, bool wasHit)
            {
                this.wasHit = wasHit;
                other = null;
            }

            /// <summary>
            /// �q�b�g���̓o�^(�\����)
            /// </summary>
            /// <param name="other">����R���C�_�[</param>
            public void RegisterHit(HitCollider other)
            {
                this.other = other;
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
                other = null;
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

        public HitCollider(HitInfo owner, ColliderShape shape, ColliderRole role, Vector3 scale)
        {
            Owner = owner;
            Shape = shape;
            Role  = role;
            HitInfo = new HitInformation(false, false);
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

            Owner.ReceiveHit();
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

            Owner.ResetHitReceived();
        }

        /// <summary>
        /// ���̍X�V
        /// </summary>
        /// <param name="transform">�X�V��Transform</param>
        public abstract void UpdateInfo(Transform transform);
    }
}