using Stage.HitCheck;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーの機能を総括
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("当たり判定クラス")]
        public OBBHitChecker HitCheck;

        // プレイヤー関連のクラス
        public PlayerStateMachine StateMachine {  get; private set; }
        public PlayerAnimation Animation {  get; private set; }
        public PlayerAction Action { get; private set; }

        // コンポーネント
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator {  get; private set; }

        // 接触中の面に対する法線ベクトル
        public Vector3 NormalVector {  get; private set; }

        void Awake()
        {
            // コンポーネント取得
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();

            // 各クラスの作成
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
        /// 衝撃を受ける
        /// </summary>
        public void TakeImpact()
        {
            StateMachine.TransitionTo(StateMachine.ImpactedState);
        }
    }
}
