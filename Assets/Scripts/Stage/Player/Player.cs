using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̋@�\�𑍊�
    /// </summary>
    public class Player : MonoBehaviour
    {
        public OBBHitChecker HitCheck => _hitCheck;
        [Header("�����蔻��N���X")]
        [SerializeField] OBBHitChecker _hitCheck;

        // �v���C���[�֘A�̃N���X
        public PlayerStateMachine StateMachine {  get; private set; }
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

        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            Action.Enable();
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);   
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
        /// �Ռ����󂯂�
        /// </summary>
        public void TakeImpact()
        {
            StateMachine.TransitionTo(StateMachine.ImpactedState);
        }
    }
}
