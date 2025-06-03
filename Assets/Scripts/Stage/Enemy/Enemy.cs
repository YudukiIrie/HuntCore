using Stage.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵の機能を総括
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public Player Player => _player;
        [Header("プレイヤー")]
        [SerializeField] Player _player;

        // 敵関連クラス
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // コンポーネント
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
        /// プレイヤーとの距離を返す
        /// </summary>
        public float CheckDistanceToPlayer()
        {
            // 座標の取得
            var a = transform.position;
            var b = _player.transform.position;

            // 各成分の差異を取得
            var x = a.x - b.x;
            var y = a.y - b.y;
            var z = a.z - b.z;

            // 距離の算出
            return Mathf.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// 状態遷移の確率判定
        /// </summary>
        /// <param name="percent">確率(0.0～1.0)</param>
        /// <returns>true:当たり, false:外れ</returns>
        public bool IsTransitionHit(float percent)
        {
            return Random.value <= percent;
        }
    }
}
