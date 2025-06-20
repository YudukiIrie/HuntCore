using Stage.HitCheck;
using Stage.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// GΜ@\π
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [field: Header("vC[NX")]
        [field: SerializeField] public Player Player { get; private set; }

        [Header("GOBB³Transform")]
        [SerializeField] Transform _enemyOBBTransform;

        [Header("GͺOBB³Transform")]
        [SerializeField] Transform _headOBBTransform;

        [Header("GErOBB³Transform")]
        [SerializeField] Transform _rWingOBBTransform;

        [Header("GΆrOBB³Transform")]
        [SerializeField] Transform _lWingOBBTransform;

        [Header("GErt―ͺOBB³Transform")]
        [SerializeField] Transform _rWingRootOBBTransform;

        [Header("GΆrt―ͺOBB³Transform")]
        [SerializeField] Transform _lWingRootOBBTransform;

        // GΦANX
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // R|[lg
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        // OBB
        public OBB EnemyOBB {  get; private set; }
        public OBB EnemyHeadOBB { get; private set; }
        public OBB EnemyRWingOBB { get; private set; }
        public OBB EnemyLWingOBB { get; private set; }
        public OBB EnemyRWingRootOBB { get; private set; }
        public OBB EnemyLWingRootOBB { get; private set; }
        // GOBBκΗpList
        public List<OBB> EnemyOBBs { get; private set; } = new();

        // UΦA
        float _attackInterval;
        float _attackTimer; // Up^C}[
        bool _canAttack;    // UσΤtO
        public int HitNum { get; private set; } // Uqbg

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
        /// OBBΜμ¬
        /// </summary>
        void CreateOBBs()
        {
            EnemyOBBs.Add(EnemyOBB =
                new OBB(_enemyOBBTransform,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemySize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyHeadOBB =
                new OBB(_headOBBTransform, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyHeadSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyRWingOBB =
                new OBB(_rWingOBBTransform, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyLWingOBB =
                new OBB(_lWingOBBTransform, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyRWingRootOBB =
                new OBB(_rWingRootOBBTransform,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingRootSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyLWingRootOBB =
                new OBB(_lWingRootOBBTransform,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingRootSize, OBB.OBBType.Body));
        }

        /// <summary>
        /// OBBξρΜXV
        /// </summary>
        void UpdateOBBInfo()
        {
            EnemyOBB.UpdateInfo(_enemyOBBTransform);
            EnemyHeadOBB.UpdateInfo(_headOBBTransform);
            EnemyRWingOBB.UpdateInfo(_rWingOBBTransform);
            EnemyLWingOBB.UpdateInfo(_lWingOBBTransform);
            EnemyRWingRootOBB.UpdateInfo(_rWingRootOBBTransform);
            EnemyLWingRootOBB.UpdateInfo(_lWingRootOBBTransform);
        }

        /// <summary>
        /// vC[ΖΜ£πΤ·
        /// </summary>
        public float GetDistanceToPlayer()
        {
            // ΐWΜζΎ
            var a = transform.position;
            var b = Player.transform.position;

            // e¬ͺΜ·ΩπζΎ
            var x = a.x - b.x;
            var y = a.y - b.y;
            var z = a.z - b.z;

            // £ΜZo
            return Mathf.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// vC[ΦΜϋόxNgπΤ·
        /// </summary>
        public Vector3 GetDirectionToPlayer()
        {
            return (Player.transform.position - transform.position).normalized;
        }

        /// <summary>
        /// ©gΜ³ΚΖvC[ΖΜpxπΤ·
        /// </summary>
        public float GetAngleToPlayer()
        {
            // ³ΚxNgΖvC[ΦΜϋόxNgπΰΟ
            var v0 = transform.forward;
            var v1 = GetDirectionToPlayer();
            var dot = Vector3.Dot(v0, v1);

            // pxπίΤp(x@)
            return Mathf.Acos(dot) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// U^C}[ΜXV
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
        /// UσΤΜmFΖΨθΦ¦
        /// </summary>
        /// <returns>true:UΒ, false:UsΒ</returns>
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
