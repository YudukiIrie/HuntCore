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
            NormalEnemy, // 雑魚敵
        }

        public Type EnemyType => _enemyType;
        [Header("敵種類")]
        [SerializeField] Type _enemyType;

        // 敵数値情報
        public Vector3 EnemySize => _enemySize;
        [Header("敵サイズ")]
        [SerializeField] Vector3 _enemySize;

        public float IdleToWanderPercent => _idleToWanderPercent;
        [Header("待機からうろつき遷移確立")]
        [SerializeField] float _idleToWanderPercent;

        public float WanderToIdlePercent => _wanderToIdlePercent;
        [Header("うろつきから待機遷移確立")]
        [SerializeField] float _wanderToIdlePercent;

        public float WanderSpeed => _wanderSpeed;
        [Header("うろつき速度")]
        [SerializeField] float _wanderSpeed;

        public float WanderRotSpeed => _wanderRotSpeed;
        [Header("うろつき回転速度")]
        [SerializeField] float _wanderRotSpeed;

        public float WanderStoppingDistance => _wanderStoppingDistance;
        [Header("うろつき時に目標到達とする目標までの距離")]
        [SerializeField] float _wanderStoppingDistance;

        public float WanderRange => _wanderRange;
        [Header("うろつき時目標設定範囲")]
        [SerializeField] float _wanderRange;
    }
}
