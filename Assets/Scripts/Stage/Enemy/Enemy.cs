using Stage.HitCheck;
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
        [field: Header("プレイヤー")]
        [field: SerializeField] public Player Player { get; private set; }

        [field: Header("当たり判定クラス")]
        [field: SerializeField] public OBBHitChecker HitChecker { get; private set; }

        [Header("敵OBB元Transform")]
        [SerializeField] Transform _enemyOBBTransform;

        [Header("敵頭OBB元Transform")]
        [SerializeField] Transform _headOBBTransform;

        // 敵関連クラス
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // コンポーネント
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        // OBB
        public OBB EnemyOBB { get; private set; }
        public OBB EnemyHeadOBB { get; private set; }
        // 被攻撃OBB
        public List<OBB> DamageableOBBs { get; private set; } = new();

        // 攻撃関連
        float _attackInterval;
        float _attackTimer; // 攻撃用タイマー
        bool _canAttack;    // 攻撃状態フラグ
        public int HitNum { get; private set; } // 攻撃ヒット数

        void Awake()
        {
            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(_animator);

            DamageableOBBs.Add(EnemyOBB = 
                new OBB(_enemyOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemySize));
            DamageableOBBs.Add(EnemyHeadOBB = 
                new OBB(_headOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyHeadSize));


            _attackInterval = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackInterval;
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);    
        }

        void Update()
        {
            UpdateOBBInfo();

            UpdateAttackTimer();

            StateMachine.Update();
        }

        void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        /// <summary>
        /// OBB情報の更新
        /// </summary>
        void UpdateOBBInfo()
        {
            EnemyOBB.UpdateInfo(_enemyOBBTransform);
            EnemyHeadOBB.UpdateInfo(_headOBBTransform);
        }

        /// <summary>
        /// プレイヤーとの距離を返す
        /// </summary>
        public float CheckDistanceToPlayer()
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

        public void IncreaseHitNum()
        {
            HitNum++;
        }
    }
}
