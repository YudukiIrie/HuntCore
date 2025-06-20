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

        // OBB
        public OBB PlayerOBB {  get; private set; }
        public OBB WeaponOBB {  get; private set; }
        // プレイヤーOBB一括管理用List
        public List<OBB> PlayerOBBs { get; private set; } = new();

        // 接触中の面に対する法線ベクトル
        public Vector3 NormalVector {  get; private set; }

        // タグ名
        const string GROUND_TAG = "Ground";

        // 攻撃ヒット数
        public int HitNum {  get; private set; }

        // ガード状態の有無
        bool _isBlocking = false;

        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            PlayerOBBs.Add(PlayerOBB = 
                new OBB(_playerOBBTransform, PlayerData.Data.PlayerSize, OBB.OBBType.Body));

            PlayerOBBs.Add(WeaponOBB = 
                new OBB(_weaponOBBTransform, WeaponData.Data.GreatSwordSize, OBB.OBBType.Weapon));

            Action.Enable();
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);   
        }

        void Update()
        {
            UpdateOBBInfo();

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
        /// OBB情報の更新
        /// </summary>
        void UpdateOBBInfo()
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
                StateMachine.TransitionTo(StateMachine.BlockedState);
            else
                StateMachine.TransitionTo(StateMachine.ImpactedState);
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
