using Stage.Enemies;
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

        [field: Header("�R���C�_�[�Ǘ��N���X")]
        [field: SerializeField] public PlayerCollider Collider { get; private set; }

        [field: Header("����Q�[���I�u�W�F�N�g")]
        [field: SerializeField] public GameObject Weapon { get; private set; }

        // �v���C���[�֘A�̃N���X
        public PlayerStateMachine StateMachine {  get; private set; }
        public PlayerHitRaction HitReaction { get; private set; }
        public PlayerAnimation Animation {  get; private set; }
        public PlayerAction Action { get; private set; }

        // �R���|�[�l���g
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] Rigidbody _rigidbody;
        public Animator Animator => _animator;
        [SerializeField] Animator _animator;

        // �ڐG���̖ʂɑ΂���@���x�N�g��
        public Vector3 NormalVector {  get; private set; }

        // �^�O��
        const string GROUND_TAG = "Ground";

        // �U���q�b�g��
        public int HitNum {  get; private set; }

        // �K�[�h�֘A
        bool _isBlocking = false;   // �K�[�h��Ԃ̗L��
        public int BlockNum {  get; private set; }  // �K�[�h��

        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            HitReaction = new PlayerHitRaction(this, 5);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            Action.Enable();
        }

        void Start()
        {
            StateMachine.Initialize(PlayerState.Idle);   
        }

        void Update()
        {
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
            {
                BlockNum++;
                StateMachine.TransitionTo(PlayerState.Blocked);
            }
            else
                StateMachine.TransitionTo(PlayerState.Impacted);
        }

        /// <summary>
        /// �p���B�ւ̑J��
        /// </summary>
        public void Parry()
        {
            StateMachine.TransitionTo(PlayerState.Parry);
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
