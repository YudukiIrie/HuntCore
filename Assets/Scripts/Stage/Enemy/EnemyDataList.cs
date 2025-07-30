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
        public Vector3 Size => _size;
        [Header("敵サイズ")]
        [SerializeField] Vector3 _size;

        public float HeadRadius => _headRadius;
        [Header("敵頭半径")]
        [SerializeField] float _headRadius;

        public Vector3 WingSize => _wingSize;
        [Header("敵翼サイズ")]
        [SerializeField] Vector3 _wingSize;

        public Vector3 WingRootSize => _wingRootSize;
        [Header("敵翼付け根サイズ")]
        [SerializeField] Vector3 _wingRootSize;

        public Vector2 ClawSize => _clawSize;
        [Header("爪サイズ(X:半径, Y:高さ)")]
        [SerializeField] Vector2 _clawSize;

        public Vector2 NeckSize => _neckSize;
        [Header("首サイズ(X:半径, Y:高さ)")]
        [SerializeField] Vector2 _neckSize;

        public Vector2 LegSize => _legSize;
        [Header("前足サイズ(X:半径, Y:高さ)")]
        [SerializeField] Vector2 _legSize;

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

        public float AttackProb => _attackProb;
        [Header("通常攻撃使用確率")]
        [SerializeField] float _attackProb;

        public float AnimBlendTime => _animBlendTime;
        [Header("アニメーションブレンド時間")]
        [SerializeField] float _animBlendTime;

        public Vector2 AttackHitWindow => _attackHitWindow;
        [Header("通常攻撃当たり判定有効区間(X:開始, Y:終了)")]
        [SerializeField] Vector2 _attackHitWindow;

        public Vector2 ClawHitWindow => _clawHitWindow;
        [Header("爪攻撃当たり判定有効区間(X:開始, Y:終了)")]
        [SerializeField] Vector2 _clawHitWindow;

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
