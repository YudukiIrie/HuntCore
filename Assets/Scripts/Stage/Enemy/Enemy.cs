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

        float _attackInterval;
        float _attackTimer; // 攻撃用タイマー
        bool _canAttack;    // 攻撃状態フラグ

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();

            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(Animator);

            _attackInterval = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackInterval;
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);    
        }

        void Update()
        {
            UpdateAttackTimer();

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
        /// 攻撃タイマーの更新
        /// </summary>
        void UpdateAttackTimer()
        {
            if (!_canAttack)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= _attackInterval)
                {
                    _attackTimer = 0.0f;
                    _canAttack = true;
                }
            }
        }

        /// <summary>
        /// 攻撃状態の確認と切り替え
        /// </summary>
        /// <returns>true:攻撃可, false:攻撃不可</returns>
        public bool CheckAttackState()
        {
            if (_canAttack)
            {
                _canAttack = false;
                return true;
            }

            return false;
        }
    }
}
