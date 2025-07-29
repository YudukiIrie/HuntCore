using Stage.enemies;
using Stage.HitDetection;
using Stage.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵の機能を総括
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [field: Header("プレイヤークラス")]
        [field: SerializeField] public Player Player { get; private set; }

        [field: Header("コライダー管理クラス")]
        [field: SerializeField] public EnemyCollider Collider { get; private set; }

        // 敵関連クラス
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // コンポーネント
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        // 攻撃関連
        float _attackInterval;
        float _attackTimer; // 攻撃用タイマー
        bool _canAttack;    // 攻撃状態フラグ
        public int HitNum { get; private set; } // 攻撃ヒット数

        void Awake()
        {
            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(_animator);

            _attackInterval = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackInterval;
        }

        void Start()
        {
            StateMachine.Initialize(EnemyState.Idle);    
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
        public float GetDistanceToPlayer()
        {
            // 座標の取得
            var a = transform.position;
            var b = Player.transform.position;

            // 各成分の差異を取得
            var x = a.x - b.x;
            var y = a.y - b.y;
            var z = a.z - b.z;

            // 距離の算出
            return Mathf.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// プレイヤーへの方向ベクトルを返す
        /// </summary>
        public Vector3 GetDirectionToPlayer()
        {
            return (Player.transform.position - transform.position).normalized;
        }

        /// <summary>
        /// 自身の正面とプレイヤーとの角度を返す
        /// </summary>
        public float GetAngleToPlayer()
        {
            // 正面ベクトルとプレイヤーへの方向ベクトルを内積
            var v0 = transform.forward;
            var v1 = GetDirectionToPlayer();
            var dot = Vector3.Dot(v0, v1);

            // 角度を求め返却(度数法)
            return Mathf.Acos(dot) * Mathf.Rad2Deg;
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

        /// <summary>
        /// 衝撃を受ける
        /// </summary>
        /// <param name="nextState">遷移後ステート</param>
        public void TakeImpact(EnemyState nextState)
        {
            StateMachine.TransitionTo(nextState);
        }

        public void IncreaseHitNum()
        {
            HitNum++;
        }
    }
}
