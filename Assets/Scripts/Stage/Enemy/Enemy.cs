using Stage.HitCheck;
using Stage.Players;
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

        // 敵関連クラス
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // コンポーネント
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        float _attackInterval;
        float _attackTimer; // 攻撃用タイマー
        bool _canAttack;    // 攻撃状態フラグ

        void Awake()
        {
            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(_animator);

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
    }
}
