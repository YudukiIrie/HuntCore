using Stage.HitDetection;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�R���C�_�[�Ǘ��N���X
    /// </summary>
    public class PlayerCollider : HitInfo
    {
        [Header("�v���C���[�R���C�_�[Transform")]
        [SerializeField] Transform _playerTransform;

        [Header("����R���C�_�[Transform")]
        [SerializeField] Transform _weaponTransform;

        // �R���C�_�[
        public HitCapsule Player {  get; private set; }
        public OBB Weapon { get; private set; }

        // �R���C�_�[�ꊇ�Ǘ��pList
        public List<HitCollider> Colliders { get; private set; }

        // ��List�ɑΉ������R���C�_�[�X�V�p�z��
        Transform[] _transforms;

        void Awake()
        {
            int capacity = 5;
            CreateColliders(capacity);
        }

        void Update()
        {
            UpdateColliders();   
        }

        /// <summary>
        /// �R���C�_�[�̍쐬
        /// </summary>
        void CreateColliders(int capacity)
        {
            Colliders = new List<HitCollider>(capacity);
            _transforms = new Transform[capacity];

            // �v���C���[�R���C�_�[
            Colliders.Add(Player = new HitCapsule(this,
                _playerTransform, PlayerData.Data.Size.y, PlayerData.Data.Size.x, 
                HitCollider.ColliderShape.Capsule, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _playerTransform;

            // ����R���C�_�[�o�^
            Colliders.Add(Weapon = new OBB(this,
                _weaponTransform, WeaponData.Data.GreatSwordSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Weapon));
            _transforms[Colliders.Count - 1] = _weaponTransform;
        }

        /// <summary>
        /// �R���C�_�[���̍X�V
        /// </summary>
        void UpdateColliders()
        {
            for (int i = 0; i < Colliders.Count; ++i)
            {
                Colliders[i].UpdateInfo(_transforms[i]);
            }
        }
    }
}