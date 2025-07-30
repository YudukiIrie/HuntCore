using Stage.Enemies;
using Stage.HitDetection;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.enemies
{
    /// <summary>
    /// 敵コライダー管理クラス
    /// </summary>
    public class EnemyCollider : HitInfo
    {
        [Header("敵Transform")]
        [SerializeField] Transform _enemyTransform;

        [Header("頭Transform")]
        [SerializeField] Transform _headTransform;

        [Header("右翼脚Transform")]
        [SerializeField] Transform _rWingTransform;

        [Header("左翼脚Transform")]
        [SerializeField] Transform _lWingTransform;

        [Header("右翼脚付け根Transform")]
        [SerializeField] Transform _rWingRootTransform;

        [Header("左翼脚付け根Transform")]
        [SerializeField] Transform _lWingRootTransform;

        [Header("右爪Transform")]
        [SerializeField] Transform _rClawTransform;

        [Header("左爪Transform")]
        [SerializeField] Transform _lClawTransform;

        [Header("首Transform")]
        [SerializeField] Transform _neckTransform;

        [Header("右前脚Transform")]
        [SerializeField] Transform _rLegTransform;

        [Header("左前脚Transform")]
        [SerializeField] Transform _lLegTransform;

        // コライダー
        public OBB Enemy {  get; private set; }
        public OBB RWing { get; private set; }
        public OBB LWing { get; private set; }
        public OBB RWingRoot { get; private set; }
        public OBB LWingRoot { get; private set; }
        public HitSphere Head {  get; private set; }
        public HitCapsule Neck { get; private set; }
        public HitCapsule RClaw { get; private set; }
        public HitCapsule LClaw { get; private set; }
        public HitCapsule RLeg { get; private set; }
        public HitCapsule LLeg { get; private set; }

        // コライダー一括管理用List
        public List<HitCollider> Colliders { get; private set; }

        // 上Listに対応したコライダー更新用配列
        Transform[] _transforms;

        void Awake()
        {
            int capacity = 15;
            CreateColliders(capacity);
        }

        void Update()
        {
            UpdateColliders();
        }

        /// <summary>
        /// コライダーの作成
        /// </summary>
        void CreateColliders(int capacity)
        {
            Colliders = new List<HitCollider>(capacity);
            _transforms = new Transform[capacity];

            // 敵コライダー
            Colliders.Add(Enemy = new OBB(this,
                _enemyTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).Size,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _enemyTransform;

            // 右翼脚コライダー
            Colliders.Add(RWing = new OBB(this,
                _rWingTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rWingTransform;

            // 左翼脚コライダー
            Colliders.Add(LWing = new OBB(this,
                _lWingTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lWingTransform;

            // 右翼脚付け根コライダー
            Colliders.Add(RWingRoot = new OBB(this,
                _rWingRootTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingRootSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rWingRootTransform;

            // 左翼脚付け根コライダー
            Colliders.Add(LWingRoot = new OBB(this,     
                _lWingRootTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingRootSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lWingRootTransform;

            // 頭コライダー
            Colliders.Add(Head = new HitSphere(this,
                _headTransform.position, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).HeadRadius,
                HitCollider.ColliderShape.Sphere, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _headTransform;

            // 右爪コライダー
            Colliders.Add(RClaw = new HitCapsule(this,
                _rClawTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.y, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.x, 
                HitCollider.ColliderShape.Capsule, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rClawTransform;

            // 左爪コライダー
            Colliders.Add(LClaw = new HitCapsule(this,
                _lClawTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.x,
                HitCollider.ColliderShape.Capsule, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lClawTransform;

            // 首コライダー
            Colliders.Add(Neck = new HitCapsule(this,
                _neckTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).NeckSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).NeckSize.x,
                HitCollider.ColliderShape.Capsule, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _neckTransform;

            // 右前脚コライダー
            Colliders.Add(RLeg = new HitCapsule(this,
                _rLegTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.x,
                HitCollider.ColliderShape.Capsule, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rLegTransform;

            // 左前脚コライダー
            Colliders.Add(LLeg = new HitCapsule(this,
                _lLegTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.x,
                HitCollider.ColliderShape.Capsule, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lLegTransform;
        }

        void UpdateColliders()
        {
            for (int i = 0; i < Colliders.Count; ++i)
            {
                Colliders[i].UpdateInfo(_transforms[i]);
            }
        }
    }
}