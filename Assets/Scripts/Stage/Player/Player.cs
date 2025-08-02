using Stage.Effects;
using Stage.Enemies;
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
        [field: SerializeField] public WeaponAfterImageSpawner AfterImageSpawner { get; private set; }

        [field: Header("血しぶきエフェクト生成クラス")]
        [field: SerializeField] public BloodFXSpawner BloodFXSpawner { get; private set; }

        [field: Header("コライダー管理クラス")]
        [field: SerializeField] public PlayerCollider Collider { get; private set; }

        [field: Header("武器ゲームオブジェクト")]
        [field: SerializeField] public GameObject Weapon { get; private set; }

        // プレイヤー関連のクラス
        public PlayerStateMachine StateMachine {  get; private set; }
        public PlayerHitRaction HitReaction { get; private set; }
        public PlayerAnimation Animation {  get; private set; }
        public PlayerAction Action { get; private set; }

        // コンポーネント
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] Rigidbody _rigidbody;
        public Animator Animator => _animator;
        [SerializeField] Animator _animator;

        // 接触中の面に対する法線ベクトル
        public Vector3 NormalVector {  get; private set; }

        // タグ名
        const string GROUND_TAG = "Ground";

        // 攻撃ヒット数
        public int HitNum {  get; private set; }

        // ガード状態の有無
        bool _isBlocking = false;

        // ヒットストップ関連
        bool _isFreezed;    // ヒットストップの有無
        float _freezeTimer; // ヒットストップタイマー
        float _freezeDuration;
        
        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            HitReaction = new PlayerHitRaction(this, 5);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            Action.Enable();

            _freezeDuration = WeaponData.Data.FreezeDuration;
        }

        void Start()
        {
            StateMachine.Initialize(PlayerState.Idle);   
        }

        void Update()
        {
            StateMachine.Update();

            UpdateFreezeTimer();
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
                StateMachine.TransitionTo(PlayerState.Blocked);
            else
                StateMachine.TransitionTo(PlayerState.Impacted);
        }

        /// <summary>
        /// パリィへの遷移
        /// </summary>
        public void Parry()
        {
            StateMachine.TransitionTo(PlayerState.Parry);
        }

        /// <summary>
        /// 攻撃ヒット数の増加
        /// </summary>
        public void IncreaseHitNum()
        {
            HitNum++;
        }

        /// <summary>
        /// ヒットストップ開始
        /// </summary>
        public void FreezeFrame()
        {
            _isFreezed = true;
            Animation.Stop();
        }

        /// <summary>
        ///  ヒットストップタイマーの更新
        /// </summary>
        void UpdateFreezeTimer()
        {
            if (_isFreezed)
            {
                _freezeTimer += Time.deltaTime;
                if (_freezeTimer >= _freezeDuration)
                {
                    _isFreezed = false;
                    _freezeTimer = 0.0f;
                    Animation.ResetParam();
                }
            }
        }
    }
}
