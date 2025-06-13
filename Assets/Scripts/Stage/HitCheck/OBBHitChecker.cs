using UnityEngine;
using Stage.Players;
using Stage.Enemies;
using UnityEngine.UI;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB同士による当たり判定
    /// </summary>
    public class OBBHitChecker : MonoBehaviour
    {
        [Header("プレイヤーオブジェクト")]
        [SerializeField] GameObject _player;

        [Header("大剣オブジェクト")]
        [SerializeField] GameObject _greatSword;

        [Header("敵オブジェクト")]
        [SerializeField] GameObject _enemy;

        [Header("敵頭オブジェクト")]
        [SerializeField] GameObject _enemyHead;

        [Header("ヒット数テキスト")]
        [SerializeField] Text _hitText;

        // 各オブジェクトOBB
        public OBB PlayerOBB { get; private set; }
        public OBB GreatSwordOBB { get; private set; }
        public OBB EnemyOBB {  get; private set; }
        public OBB EnemyHeadOBB {  get; private set; }

        int _hitNum;
        
        void Start()
        {
            // 各オブジェクトOBB情報の登録と実体の作成
            PlayerOBB     = new OBB(_player.transform, PlayerData.Data.PlayerSize);
            GreatSwordOBB = new OBB(_greatSword.transform, WeaponData.Data.GreatSwordSize);
            EnemyOBB      = new OBB(_enemy.transform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemySize);
            EnemyHeadOBB  = new OBB(_enemyHead.transform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyHeadSize);
        }

        void Update()
        {
            UpdateOBBInfo(PlayerOBB, _player);
            UpdateOBBInfo(GreatSwordOBB, _greatSword);
            UpdateOBBInfo(EnemyOBB, _enemy);
            UpdateOBBInfo(EnemyHeadOBB, _enemyHead);

            UpdateUI();
        }

        /// <summary>
        /// OBB情報の更新
        /// </summary>
        /// <param name="obb">更新対象のOBB</param>
        /// <param name="go">obbの元となるオブジェクト</param>
        void UpdateOBBInfo(OBB obb, GameObject go)
        {
            // 座標の更新
            obb.UpdateCenter(go.transform.position);
            // 分離軸の更新
            obb.UpdateAxes(go.transform);
        }

        void UpdateUI()
        {
            _hitText.text = _hitNum.ToString();
        }

        /// <summary>
        /// 指定したOBB同士の当たり判定
        /// </summary>
        /// <param name="obbA">能動的OBB</param>
        /// <param name="obbB">受動的OBB</param>
        /// <returns>true:接触、false:非接触</returns>
        public bool IsCollideBoxOBB(OBB obbA, OBB obbB)
        {
            // 判定済みのOBBは無視
            if (obbB.IsHit) return false;

            // 中心間の距離の取得
            Vector3 distance = obbA.Center - obbB.Center;

            // 検証軸を用いた距離の比較
            // Aを検証軸とした場合
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbA.AxisZ, distance)) return false;
            // 敵を検証軸とした場合
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisX, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisY, distance)) return false;
            if (!CompareLengthOBB(obbA, obbB, obbB.AxisZ, distance)) return false;

            // 分離軸同士の外積を用いた距離の比較
            Vector3 cross = Vector3.zero;
            // 武器のX軸との比較
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisX, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // 武器のY軸との比較
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisY, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            // 武器のZ軸との比較
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisX);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisY);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;
            cross = Vector3.Cross(obbA.AxisZ, obbB.AxisZ);
            if (!CompareLengthOBB(obbA, obbB, cross, distance)) return false;

            // 接触済みOBBの情報を更新
            obbB.Hit();
            _hitNum++;

            return true;
        }

        /// <summary>
        /// 指定した武器OBBと敵OBBの距離比較
        /// </summary>
        /// <param name="separating">指定した分離軸</param>
        /// <param name="distance">2点間の距離</param>
        /// <returns></returns>
        bool CompareLengthOBB(OBB obbA, OBB obbB, Vector3 separating, Vector3 distance)
        {
            // 検証軸上の武器と敵の距離
            // マイナスのベクトルである可能性があるため絶対値化
            float length = Mathf.Abs(Vector3.Dot(separating, distance));

            // 特定の検証軸上における武器の距離の半分を求める
            float weaponDist =
                Mathf.Abs(Vector3.Dot(obbA.AxisX, separating)) * obbA.Radius.x +
                Mathf.Abs(Vector3.Dot(obbA.AxisY, separating)) * obbA.Radius.y +
                Mathf.Abs(Vector3.Dot(obbA.AxisZ, separating)) * obbA.Radius.z;

            // 特定の検証軸上における敵の距離の半分を求める
            float enemyDist =
                Mathf.Abs(Vector3.Dot(obbB.AxisX, separating)) * obbB.Radius.x +
                Mathf.Abs(Vector3.Dot(obbB.AxisY, separating)) * obbB.Radius.y +
                Mathf.Abs(Vector3.Dot(obbB.AxisZ, separating)) * obbB.Radius.z;

            // 武器と敵の距離と、上記の距離の合計を比較
            if (length > weaponDist + enemyDist)
                return false;

            return true;
        }

        /// <summary>
        /// 判定のリセット
        /// </summary>
        public void ResetHitInfo()
        {
            GreatSwordOBB.ResetHitInfo();
            EnemyOBB.ResetHitInfo();
        }

        #region デバッグ用
        void OnDrawGizmos()
        {
            // 初期マトリックスの保存
            Matrix4x4 oldMatrix = Gizmos.matrix;

            // 色の作成
            Color red = new Color(1.0f, 0.0f, 0.0f, 0.5f);
            Color white = new Color(1.0f, 1.0f, 1.0f, 0.5f);

            OBB obb = null;
            Vector3 pos, scale = Vector3.zero;
            Quaternion rot = Quaternion.identity;

            // === 敵OBB ===
            if (EnemyOBB != null)
            {
                obb = EnemyOBB;

                // マトリックス情報の取得
                pos = obb.Center;
                rot = _enemy.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // 色の変更
                Gizmos.color = obb.IsHit ? red : white;

                // マトリックスの更新
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // 描画
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }

            // === 大剣OBB ===
            if (GreatSwordOBB != null)
            {
                obb = GreatSwordOBB;

                // マトリックス情報の取得
                pos = obb.Center;
                rot = _greatSword.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // 色の変更
                Gizmos.color = obb.IsHit ? red : white;

                // マトリックスの更新
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // 描画
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }

            // === 敵頭OBB ===
            if (EnemyHeadOBB != null)
            {
                obb = EnemyHeadOBB;

                // マトリックス情報の取得
                pos = obb.Center;
                rot = _enemyHead.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // 色の変更
                Gizmos.color = obb.IsHit ? red : white;

                // マトリックスの更新
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // 描画
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }

            // === プレイヤーOBB ===
            if (PlayerOBB != null)
            {
                obb = PlayerOBB;

                // マトリックス情報の取得
                pos = obb.Center;
                rot = _player.transform.rotation;
                scale = new Vector3(obb.Radius.x, obb.Radius.y, obb.Radius.z) * 2;

                // 色の変更
                Gizmos.color = obb.IsHit ? red : white;

                // マトリックスの更新
                Gizmos.matrix = Matrix4x4.TRS(pos, rot, scale);

                // 描画
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
                Gizmos.matrix = oldMatrix;
            }
        }
        #endregion
    }
}
