using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤー情報用スクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerData",menuName = "ScriptableObject/PlayerData")]
    [System.Serializable]
    public class PlayerData : ScriptableObject
    {
        // 保存先パス
        const string PATH = "PlayerData";

        // アクセス用のインスタンス
        static PlayerData _data;
        public static PlayerData Data
        {
            get
            {
                if (_data == null)
                {
                    // アクセスされたらリソースにあるパス名のオブジェクトを読み込む
                    _data = Resources.Load<PlayerData>(PATH);

                    // 読み込み失敗時のエラー
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        // プレイヤー数値情報
        public float WalkSpeed => _walkSpeed;
        [Header("歩き速度")]
        [SerializeField] float _walkSpeed;

        public float JogSpeed => _jogSpeed;
        [Header("小走り速度")]
        [SerializeField] float _jogSpeed;

        public float RunSpeed => _runSpeed;
        [Header("走り速度")]
        [SerializeField] float _runSpeed;

        public float DrawnMoveSpeed => _drawnMoveSpeed;
        [Header("抜刀状態の移動速度")]
        [SerializeField] float _drawnMoveSpeed;

        public float RotSpeed => _rotSpeed;
        [Header("回転速度")]
        [SerializeField] float _rotSpeed;

        public float DrawnRotSpeed => _drawnRotSpeed;
        [Header("抜刀状態の回転速度")]
        [SerializeField] float _drawnRotSpeed;

        public float HeavyAttackRotSpeed => _heavyAttackRotSpeed;
        [Header("ヘビー攻撃の回転速度")]
        [SerializeField] float _heavyAttackRotSpeed;

        public float SpecialAttackRotSpeed => _specialAttackRotSpeed;
        [Header("スペシャル攻撃の回転速度")]
        [SerializeField] float _specialAttackRotSpeed;

        public float MagnitudeBorder => _magnitudeBorder;
        [Header("歩きと小走りを区別するベクトル長の境界")]
        [SerializeField]float _magnitudeBorder;

        public float ChainTime => _chainTime;
        [Header("コンボ間猶予時間")]
        [SerializeField] float _chainTime;

        public float IdleToOtherDuration => _idleToOtherDuration;
        [Header("待機から遷移可能までの時間")]
        [SerializeField] float _idleToOtherDuration;

        public float AnimBlendTime => _animBlendTime;
        [Header("アニメーションブレンド時間")]
        [SerializeField] float _animBlendTime;
    }
}
