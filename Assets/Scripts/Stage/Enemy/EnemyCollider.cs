using Stage.Enemies;
using Stage.HitDetection;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.enemies
{
    /// <summary>
    /// GRC_[ÇNX
    /// </summary>
    public class EnemyCollider : HitInfo
    {
        [Header("GTransform")]
        [SerializeField] Transform _enemyTransform;

        [Header("ªTransform")]
        [SerializeField] Transform _headTransform;

        [Header("ErTransform")]
        [SerializeField] Transform _rWingTransform;

        [Header("¶rTransform")]
        [SerializeField] Transform _lWingTransform;

        [Header("Ert¯ªTransform")]
        [SerializeField] Transform _rWingRootTransform;

        [Header("¶rt¯ªTransform")]
        [SerializeField] Transform _lWingRootTransform;

        [Header("EÜTransform")]
        [SerializeField] Transform _rClawTransform;

        [Header("¶ÜTransform")]
        [SerializeField] Transform _lClawTransform;

        [Header("ñTransform")]
        [SerializeField] Transform _neckTransform;

        [Header("EOrTransform")]
        [SerializeField] Transform _rLegTransform;

        [Header("¶OrTransform")]
        [SerializeField] Transform _lLegTransform;

        // RC_[
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

        // RC_[êÇpList
        public List<HitCollider> Colliders { get; private set; }

        // ãListÉÎµ½RC_[XVpzñ
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
        /// RC_[Ìì¬
        /// </summary>
        void CreateColliders(int capacity)
        {
            Colliders = new List<HitCollider>(capacity);
            _transforms = new Transform[capacity];

            // GRC_[
            Colliders.Add(Enemy = new OBB(this,
                _enemyTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).Size,
                ColliderShape.OBB, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _enemyTransform;

            // ErRC_[
            Colliders.Add(RWing = new OBB(this,
                _rWingTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingSize,
                ColliderShape.OBB, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rWingTransform;

            // ¶rRC_[
            Colliders.Add(LWing = new OBB(this,
                _lWingTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingSize,
                ColliderShape.OBB, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lWingTransform;

            // Ert¯ªRC_[
            Colliders.Add(RWingRoot = new OBB(this,
                _rWingRootTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingRootSize,
                ColliderShape.OBB, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rWingRootTransform;

            // ¶rt¯ªRC_[
            Colliders.Add(LWingRoot = new OBB(this,     
                _lWingRootTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).WingRootSize,
                ColliderShape.OBB, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lWingRootTransform;

            // ªRC_[
            Colliders.Add(Head = new HitSphere(this,
                _headTransform.position, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).HeadRadius,
                ColliderShape.Sphere, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _headTransform;

            // EÜRC_[
            Colliders.Add(RClaw = new HitCapsule(this,
                _rClawTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.y, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.x, 
                ColliderShape.Capsule, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rClawTransform;

            // ¶ÜRC_[
            Colliders.Add(LClaw = new HitCapsule(this,
                _lClawTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawSize.x,
                ColliderShape.Capsule, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _lClawTransform;

            // ñRC_[
            Colliders.Add(Neck = new HitCapsule(this,
                _neckTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).NeckSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).NeckSize.x,
                ColliderShape.Capsule, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _neckTransform;

            // EOrRC_[
            Colliders.Add(RLeg = new HitCapsule(this,
                _rLegTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.x,
                ColliderShape.Capsule, ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _rLegTransform;

            // ¶OrRC_[
            Colliders.Add(LLeg = new HitCapsule(this,
                _lLegTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.y,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LegSize.x,
                ColliderShape.Capsule, ColliderRole.Body));
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