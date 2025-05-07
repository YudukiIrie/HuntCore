using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemy
{
    /// <summary>
    /// 敵情報用スクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData")]
    [System.Serializable]
    public class EnemyData : ScriptableObject
    {
        // 保存先パス
        const string PATH = "EnemyData";

        // アクセス用のインスタンス
        static EnemyData _data;
        public static EnemyData Data
        {
            get
            {
                if (_data == null)
                {
                    // アクセスされたらリソースにあるパス名のオブジェクトを読み込む
                    _data = Resources.Load<EnemyData>(PATH);

                    // 読み込み失敗時のエラー
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        // 敵数値情報
        public Vector3 EnemySize => _enemySize;
        [Header("敵サイズ")]
        [SerializeField] Vector3 _enemySize;
    }
}
