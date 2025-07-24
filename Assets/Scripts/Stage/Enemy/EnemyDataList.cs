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

        public Vector3 EnemyHeadSize => _enemyHeadSize;
        [Header("敵頭サイズ")]
        [SerializeField] Vector3 _enemyHeadSize;

        public float EnemyHeadRadius => _enemyHeadRadius;
        [Header("敵頭半径")]
        [SerializeField] float _enemyHeadRadius;

        public Vector3 EnemyWingSize => _enemyWingSize;
        [Header("敵翼サイズ")]
        [SerializeField] Vector3 _enemyWingSize;

        public Vector3 EnemyWingRootSize => _enemyWingRootSize;
        [Header("敵翼付け根サイズ")]
        [SerializeField] Vector3 _enemyWingRootSize;

        public float FindDistance => _findDistance;
        [Header("プレイヤー発見距離")]
        [SerializeField] float _findDistance;

        public float RoarDistance => _roarDistance;
        [Header("咆哮距離")]
        [SerializeField] float _roarDistance;

        public float StopDistance => _stopDistance;
        [Header("追跡停止距離")]
        [SerializeField] float _stopDistance;

        public float AttackDistance => _attackDistance;
        [Header("攻撃可能プレイヤー間距離")]
        [SerializeField] float _attackDistance;

        public float ChaseSpeed => _chaseSpeed;
        [Header("追跡速度")]
        [SerializeField] float _chaseSpeed;

        public float ChaseRotSpeed => _chaseRotSpeed;
        [Header("追跡時回転速度")]
        [SerializeField] float _chaseRotSpeed;

        public float AttackInterval => _attackInterval;
        [Header("攻撃可能間隔")]
        [SerializeField] float _attackInterval;

        public float AnimBlendTime => _animBlendTime;
        [Header("アニメーションブレンド時間")]
        [SerializeField] float _animBlendTime;

        public Vector2 AttackHitWindow => _attackHitWindow;
        [Header("攻撃当たり判定有効区間(X:開始, Y:終了)")]
        [SerializeField] Vector2 _attackHitWindow;

        public float LimitAngle => _limitAngle;
        [Header("対象との限界角度")]
        [SerializeField] float _limitAngle;

        public float AttackAngle => _attackAngle;
        [Header("攻撃可能角度")]
        [SerializeField] float _attackAngle;

        public float TurnSpeed => _turnSpeed;
        [Header("方向転換速度")]
        [SerializeField] float _turnSpeed;

        public float DownDuration => _downDuration;
        [Header("ダウン時間")]
        [SerializeField] float _downDuration;
    }
}
