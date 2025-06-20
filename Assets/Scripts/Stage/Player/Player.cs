using Stage.Enemies;
using Stage.HitCheck;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̋@�\�𑍊�
    /// </summary>
    public class Player : MonoBehaviour
    {
        [field: Header("�G�N���X")]
        [field: SerializeField] public Enemy Enemy { get; private set; }

        [field: Header("����c�������N���X")]
        [field: SerializeField] public WeaponAfterImageSpawner Spawner { get; private set; }

        [field: Header("����Q�[���I�u�W�F�N�g")]
        [field: SerializeField] public GameObject Weapon { get; private set; }

        [Header("�v���C���[OBB��Transform")]
        [SerializeField] Transform _playerOBBTransform;

        [Header("����OBB��Transform")]
        [SerializeField] Transform _weaponOBBTransform;

        // �v���C���[�֘A�̃N���X
        public PlayerStateMachine StateMachine {  get; private set; }
        public PlayerAnimation Animation {  get; private set; }
        public PlayerAction Action { get; private set; }

        // �R���|�[�l���g
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] Rigidbody _rigidbody;
        public Animator Animator => _animator;
        [SerializeField] Animator _animator;

        // OBB
        public OBB PlayerOBB {  get; private set; }
        public OBB WeaponOBB {  get; private set; }
        // �v���C���[OBB�ꊇ�Ǘ��pList
        public List<OBB> PlayerOBBs { get; private set; } = new();

        // �ڐG���̖ʂɑ΂���@���x�N�g��
        public Vector3 NormalVector {  get; private set; }

        // �^�O��
        const string GROUND_TAG = "Ground";

        // �U���q�b�g��
        public int HitNum {  get; private set; }

        // �K�[�h��Ԃ̗L��
        bool _isBlocking = false;

        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            PlayerOBBs.Add(PlayerOBB = 
                new OBB(_playerOBBTransform, PlayerData.Data.PlayerSize, OBB.OBBType.Body));

            PlayerOBBs.Add(WeaponOBB = 
                new OBB(_weaponOBBTransform, WeaponData.Data.GreatSwordSize, OBB.OBBType.Weapon));

            Action.Enable();
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);   
        }

        void Update()
        {
            UpdateOBBInfo();

            StateMachine.Update();
        }

        void FixedUpdate()
        {
            StateMachine.FixedUpdate();   
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(GROUND_TAG))
                NormalVector = collision.contacts[0].normal;
        }

        /// <summary>
        /// OBB���̍X�V
        /// </summary>
        void UpdateOBBInfo()
        {
            PlayerOBB.UpdateInfo(_playerOBBTransform);
            WeaponOBB.UpdateInfo(_weaponOBBTransform);
        }

        /// <summary>
        /// �K�[�h��Ԃ̐؂�ւ�
        /// </summary>
        /// <param name="isBlocking">�؂�ւ���K�[�h���</param>
        public void SetGuardState(bool isBlocking)
        {
            _isBlocking = isBlocking;
        }

        /// <summary>
        /// �Ռ����󂯂�
        /// </summary>
        public void TakeImpact()
        {
            if (_isBlocking)
                StateMachine.TransitionTo(StateMachine.BlockedState);
            else
                StateMachine.TransitionTo(StateMachine.ImpactedState);
        }

        /// <summary>
        /// �U���q�b�g���̑���
        /// </summary>
        public void IncreaseHitNum()
        {
            HitNum++;
        }
    }
}
