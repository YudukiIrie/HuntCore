using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Stage.Enemy
{
    /// <summary>
    /// 敵の機能を総括
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        // 敵関連クラス
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // コンポーネント
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
