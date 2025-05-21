using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Stage.Enemy
{
    /// <summary>
    /// �G�̋@�\�𑍊�
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        // �G�֘A�N���X
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // �R���|�[�l���g
        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }

        void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
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
    }
}
