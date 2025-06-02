using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵情報用スクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyDataList", menuName = "ScriptableObject/EnemyDataList")]
    [System.Serializable]
    public class EnemyDataList : ScriptableObject
    {
        // 敵データをListで保持
        public List<EnemyData> Enemies = new();

        // 保存先パス
        const string PATH = "EnemyDataList";

        // アクセス用のインスタンス
        static EnemyDataList _data;
        public static EnemyDataList Data
        {
            get
            {
                if (_data == null)
                {
                    // アクセスされたらリソースにあるパス名のオブジェクトを読み込む
                    _data = Resources.Load<EnemyDataList>(PATH);

                    // 読み込み失敗時のエラー
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        /// <summary>
        /// 指定した敵タイプの情報を取得
        /// </summary>
        /// <param name="type">指定した敵タイプ</param>
        /// <returns>敵データ</returns>
        public EnemyData GetData(EnemyData.Type type)
        {
            return Enemies.Find(item => item.EnemyType == type);
        }
    }

    [System.Serializable]
    public class EnemyData
    {
        // 敵種類
        public enum Type
        {
            BossEnemy, // ボス
        }

        public Type EnemyType => _enemyType;
        [Header("敵種類")]
        [SerializeField] Type _enemyType;

        // 敵数値情報
        public Vector3 EnemySize => _enemySize;
        [Header("敵サイズ")]
        [SerializeField] Vector3 _enemySize;

        public float FindDistance => _findDistance;
        [Header("プレイヤー発見距離")]
        [SerializeField] float _findDistance;

        public float RoarDistance => _roarDistance;
        [Header("咆哮距離")]
        [SerializeField] float _roarDistance;
    }
}
