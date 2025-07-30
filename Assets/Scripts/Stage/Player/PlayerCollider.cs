using Stage.HitDetection;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーコライダー管理クラス
    /// </summary>
    public class PlayerCollider : HitInfo
    {
        [Header("プレイヤーコライダーTransform")]
        [SerializeField] Transform _playerTransform;

        [Header("武器コライダーTransform")]
        [SerializeField] Transform _weaponTransform;

        // コライダー
        public HitCapsule Player {  get; private set; }
        public OBB Weapon { get; private set; }

        // コライダー一括管理用List
        public List<HitCollider> Colliders { get; private set; }

        // 上Listに対応したコライダー更新用配列
        Transform[] _transforms;

        void Awake()
        {
            int capacity = 5;
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

            // プレイヤーコライダー
            Colliders.Add(Player = new HitCapsule(this,
                _playerTransform, PlayerData.Data.Size.y, PlayerData.Data.Size.x, 
                HitCollider.ColliderShape.Capsule, HitCollider.ColliderRole.Body));
            _transforms[Colliders.Count - 1] = _playerTransform;

            // 武器コライダー登録
            Colliders.Add(Weapon = new OBB(this,
                _weaponTransform, WeaponData.Data.GreatSwordSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Weapon));
            _transforms[Colliders.Count - 1] = _weaponTransform;
        }

        /// <summary>
        /// コライダー情報の更新
        /// </summary>
        void UpdateColliders()
        {
            for (int i = 0; i < Colliders.Count; ++i)
            {
                Colliders[i].UpdateInfo(_transforms[i]);
            }
        }
    }
}