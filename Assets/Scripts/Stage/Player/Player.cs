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
        [field: Header("�����蔻��N���X")]
        [field: SerializeField] public OBBHitChecker HitChecker { get; private set; }

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
        public OBB PlayerOBB { get; private set; }
        public OBB WeaponOBB { get; private set; }
        // ��U��OBB
        public List<OBB> DamageableOBBs { get; private set; } = new();

        // �ڐG���̖ʂɑ΂���@���x�N�g��
        public Vector3 NormalVector {  get; private set; }

        // �^�O��
        const string GROUND_TAG = "Ground";

        // �U���q�b�g��
        public int HitNum {  get; private set; }

        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            DamageableOBBs.Add(PlayerOBB = new OBB(_playerOBBTransform, PlayerData.Data.PlayerSize));
            WeaponOBB = new OBB(_weaponOBBTransform, WeaponData.Data.GreatSwordSize);

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
        /// �Ռ����󂯂�
        /// </summary>
        public void TakeImpact()
        {
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
