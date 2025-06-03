using Stage.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�̋@�\�𑍊�
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public Player Player => _player;
        [Header("�v���C���[")]
        [SerializeField] Player _player;

        // �G�֘A�N���X
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // �R���|�[�l���g
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }


        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();

            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(Animator);
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

        /// <summary>
        /// �v���C���[�Ƃ̋�����Ԃ�
        /// </summary>
        public float CheckDistanceToPlayer()
        {
            // ���W�̎擾
            var a = transform.position;
            var b = _player.transform.position;

            // �e�����̍��ق��擾
            var x = a.x - b.x;
            var y = a.y - b.y;
            var z = a.z - b.z;

            // �����̎Z�o
            return Mathf.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// ��ԑJ�ڂ̊m������
        /// </summary>
        /// <param name="percent">�m��(0.0�`1.0)</param>
        /// <returns>true:������, false:�O��</returns>
        public bool IsTransitionHit(float percent)
        {
            return Random.value <= percent;
        }
    }
}
