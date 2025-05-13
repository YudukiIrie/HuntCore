using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
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

        public float Attack1HitStartRatio => _attack1HitStartRatio;
        [Header("大剣攻撃1の当たり判定開始割合")]
        [SerializeField] float _attack1HitStartRatio;

        public float Attack2HitStartRatio => _attack2HitStartRatio;
        [Header("大剣攻撃2の当たり判定開始割合")]
        [SerializeField] float _attack2HitStartRatio;

        public float Attack3HitStartRatio => _attack3HitStartRatio;
        [Header("大剣攻撃3の当たり判定開始割合")]
        [SerializeField] float _attack3HitStartRatio;
    }
}
