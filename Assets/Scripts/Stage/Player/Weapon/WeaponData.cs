using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// 武器情報用スクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData")]
    [System.Serializable]
    public class WeaponData : ScriptableObject
    {
        // 保存先パス
        const string PATH = "WeaponData";

        // アクセス用のインスタンス
        static WeaponData _data;
        public static WeaponData Data
        {
            get
            {
                if (_data == null)
                {
                    // アクセスされたらリソースにあるパス名のオブジェクトを読み込む
                    _data = Resources.Load<WeaponData>(PATH);

                    // 読み込み失敗時のエラー
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        // 武器数値情報
        public Vector3 GreatSwordSize => _greatSwordSize;
        [Header("大剣サイズ")]
        [SerializeField] Vector3 _greatSwordSize;

        public Vector2 LightAttackHitWindow => _lightAttackHitWindow;
        [Header("ライト攻撃当たり判定有効区間(X:開始, Y:終了)")]
        [SerializeField] Vector2 _lightAttackHitWindow;

        public Vector2 HeavyAttackHitWindow => _heavyAttackHitWindow;
        [Header("ヘビー攻撃当たり判定有効区間(X:開始, Y:終了)")]
        [SerializeField] Vector2 _heavyAttackHitWindow;

        public Vector2 SpecialAttackHitWindow => _specialAttackHitWindow;
        [Header("スペシャル攻撃当たり判定有効区間(X:開始, Y:終了)")]
        [SerializeField] Vector2 _specialAttackHitWindow;

        public Vector2 ParryHitWindow => _parryHitWindow;
        [Header("パリィ当たり判定有効区間(X:開始, Y:終了)")]
        [SerializeField] Vector2 _parryHitWindow;

        public float FreezeDuration => _freezeDuration;
        [Header("ヒットストップ継続時間")]
        [SerializeField] float _freezeDuration;

        public int InitialPoolSize => _initialPoolSize;
        [Header("残像用初期プールサイズ")]
        [SerializeField] int _initialPoolSize;

        public GameObject AfterImagePrefab => _afterImagePrefab;
        [Header("残像用プレハブ")]
        [SerializeField] GameObject _afterImagePrefab;

        public float AfterImageInterval => _afterImageInterval;
        [Header("残像生成間隔")]
        [SerializeField] float _afterImageInterval;

        public float AfterImageDuration => _afterImageDuration;
        [Header("残像表示時間")]
        [SerializeField] float _afterImageDuration;

        public float AfterImageEndRatio => _afterImageEndRatio;
        [Header("アニメーションにおける残像表示終了割合")]
        [SerializeField] float _afterImageEndRatio;
    }
}
