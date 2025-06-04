using Stage.HitCheck;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̋@�\�𑍊�
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("�����蔻��N���X")]
        public OBBHitChecker HitCheck;

        // �v���C���[�֘A�̃N���X
        public PlayerStateMachine StateMachine {  get; private set; }
        public PlayerAnimation Animation {  get; private set; }
        public PlayerAction Action { get; private set; }

        // �R���|�[�l���g
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator {  get; private set; }

        // �ڐG���̖ʂɑ΂���@���x�N�g��
        public Vector3 NormalVector {  get; private set; }

        void Awake()
        {
            // �R���|�[�l���g�擾
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();

            // �e�N���X�̍쐬
            StateMachine = new PlayerStateMachine(this);
            Animation = new PlayerAnimation(Animator);
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
            if (collision.gameObject.CompareTag("Ground"))
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
