using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーの機能を総括
    /// </summary>
    public class Player : MonoBehaviour
    {
        [field: Header("当たり判定クラス")]
        [field: SerializeField] public OBBHitChecker HitChecker { get; private set; }

        [field: Header("武器残像生成クラス")]
        [field: SerializeField] public WeaponAfterImageSpawner Spawner { get; private set; }

        [field: Header("武器ゲームオブジェクト")]
        [field: SerializeField] public GameObject Weapon { get; private set; }


        // プレイヤー関連のクラス
        public PlayerStateMachine StateMachine {  get; private set; }
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

        void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            Animation = new PlayerAnimation(_animator);
            Action = new PlayerAction();

            Action.Enable();
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);   
        }

        void Update()
        {
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
        /// 衝撃を受ける
        /// </summary>
        public void TakeImpact()
        {
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
