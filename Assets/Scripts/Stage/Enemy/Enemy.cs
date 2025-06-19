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
        [field: Header("プレイヤークラス")]
        [field: SerializeField] public Player Player { get; private set; }

        [Header("敵OBB元Transform")]
        [SerializeField] Transform _enemyOBBTransform;

        [Header("敵頭OBB元Transform")]
        [SerializeField] Transform _headOBBTransform;

        [Header("敵右翼脚OBB元Transform")]
        [SerializeField] Transform _rWingOBBTransform;

        [Header("敵左翼脚OBB元Transform")]
        [SerializeField] Transform _lWingOBBTransform;

        // 敵関連クラス
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // コンポーネント
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        // OBB
        public OBB EnemyOBB { get; private set; }
        public OBB EnemyHeadOBB { get; private set; }
        public OBB EnemyRWingOBB { get; private set; }
        public OBB EnemyLWingOBB { get; private set; }
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

            CreateOBBs();
            
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
        /// OBBの作成
        /// </summary>
        void CreateOBBs()
        {
            DamageableOBBs.Add(EnemyOBB =
                new OBB(_enemyOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemySize));
            DamageableOBBs.Add(EnemyHeadOBB =
                new OBB(_headOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyHeadSize));
            DamageableOBBs.Add(EnemyRWingOBB =
                new OBB(_rWingOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingSize));
            DamageableOBBs.Add(EnemyLWingOBB =
                new OBB(_lWingOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingSize));
        }

        /// <summary>
        /// OBB情報の更新
        /// </summary>
        void UpdateOBBInfo()
        {
            EnemyOBB.UpdateInfo(_enemyOBBTransform);
            EnemyHeadOBB.UpdateInfo(_headOBBTransform);
            EnemyRWingOBB.UpdateInfo(_rWingOBBTransform);
            EnemyLWingOBB.UpdateInfo(_lWingOBBTransform);
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

        public void IncreaseHitNum()
        {
            HitNum++;
        }
    }
}
