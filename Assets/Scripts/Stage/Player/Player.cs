using Stage.Enemies;
using Stage.HitCheck;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーの機能を総括
    /// </summary>
    public class Player : MonoBehaviour
    {
        [field: Header("敵クラス")]
        [field: SerializeField] public Enemy Enemy { get; private set; }

        [field: Header("武器残像生成クラス")]
        [field: SerializeField] public WeaponAfterImageSpawner Spawner { get; private set; }

        [field: Header("武器ゲームオブジェクト")]
        [field: SerializeField] public GameObject Weapon { get; private set; }

        [Header("プレイヤーOBB元Transform")]
        [SerializeField] Transform _playerOBBTransform;

        [Header("武器OBB元Transform")]
        [SerializeField] Transform _weaponOBBTransform;

        // プレイヤー関連のクラス
        public PlayerStateMachine StateMachine {  get; private set; }
        public PlayerAnimation Animation {  get; private set; }
        public PlayerAction Action { get; private set; }

        // コンポーネント
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] Rigidbody _rigidbody;
        public Animator Animator => _animator;
        [SerializeField] Animator _animator;

        // 当たり判定関連
        public OBB PlayerOBB {  get; private set; }
        public OBB WeaponOBB {  get; private set; }
        // プレイヤーコライダー一括管理用List
        public List<HitCollider> PlayerColliders { get; private set; } = new();

        // 接触中の面に対する法線ベクトル
        public Vector3 NormalVector {  get; private set; }

        // タグ名
        const string GROUND_TAG = "Ground";

        // 攻撃ヒット数
        public int HitNum {  get; private set; }

        // ガード関連
        bool _isBlocking = false;   // ガード状態の有無
        public int BlockNum {  get; private set; }  // ガード回数

        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            CreateColliders();

            Action.Enable();
        }

        void Start()
        {
            StateMachine.Initialize(PlayerState.Idle);   
        }

        void Update()
        {
            UpdateColliderInfo();

            StateMachine.Update();
        }

        void FixedUpdate()
        {
            StateMachine.FixedUpdate();   
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(GROUND_TAG))
                NormalVector = collision.contacts[0].normal;
        }

        /// <summary>
        /// コライダーの作成
        /// </summary>
        void CreateColliders()
        {
            PlayerColliders.Add(PlayerOBB = new OBB(
                _playerOBBTransform, PlayerData.Data.PlayerSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Body));

            PlayerColliders.Add(WeaponOBB = new OBB(
                _weaponOBBTransform, WeaponData.Data.GreatSwordSize,
                HitCollider.ColliderShape.OBB, HitCollider.ColliderRole.Weapon));
        }

        /// <summary>
        /// コライダー情報の更新
        /// </summary>
        void UpdateColliderInfo()
        {
            PlayerOBB.UpdateInfo(_playerOBBTransform);
            WeaponOBB.UpdateInfo(_weaponOBBTransform);
        }

        /// <summary>
        /// ガード状態の切り替え
        /// </summary>
        /// <param name="isBlocking">切り替え後ガード状態</param>
        public void SetGuardState(bool isBlocking)
        {
            _isBlocking = isBlocking;
        }

        /// <summary>
        /// 衝撃を受ける
        /// </summary>
        public void TakeImpact()
        {
            if (_isBlocking)
            {
                BlockNum++;
                StateMachine.TransitionTo(PlayerState.Blocked);
            }
            else
                StateMachine.TransitionTo(PlayerState.Impacted);
        }

        /// <summary>
        /// 攻撃ヒット数の増加
        /// </summary>
        public void IncreaseHitNum()
        {
            HitNum++;
        }
    }
}
