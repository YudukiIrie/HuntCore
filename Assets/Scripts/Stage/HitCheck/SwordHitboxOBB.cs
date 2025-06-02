using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage.Players;
using Stage.Enemies;
using Unity.VisualScripting;

namespace Stage.HitCheck
{
    /// <summary>
    /// 武器のOBBによる当たり判定
    /// </summary>
    public class SwordHitboxOBB : MonoBehaviour
    {
        [Header("大剣オブジェクト")]
        [SerializeField] GameObject _greatSword;

        [Header("敵オブジェクト")]
        [SerializeField] GameObject _enemy;

        #region デバッグ用
        [Header("敵非接触マテリアル")]
        [SerializeField] Material _noHitMaterial;

        [Header("敵接触マテリアル")]
        [SerializeField] Material _hitMaterial;

        MeshRenderer _enemyMeshRenderer;
        #endregion

        // 各オブジェクトOBB
        OBB _greatSwordOBB;
        OBB _enemyOBB;
        public OBB GreatSwordOBB => _greatSwordOBB;

        // 判定済み敵格納用
        HashSet<OBB> _hitEnemies = new HashSet<OBB>();

        void Start()
        {
            // 各オブジェクトOBB情報の登録と実体の作成
            _greatSwordOBB = new OBB(_greatSword.transform, WeaponData.Data.GreatSwordSize);
            _enemyOBB = new OBB(_enemy.transform, EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).EnemySize);

            #region デバッグ用
            _enemyMeshRenderer = _enemy.GetComponent<MeshRenderer>();
            #endregion
        }

        void Update()
        {
            UpdateOBBInfo(_greatSwordOBB, _greatSword);
            UpdateOBBInfo(_enemyOBB, _enemy);
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

        /// <summary>
        /// 指定した武器OBBと敵OBBとの当たり判定
        /// </summary>
        /// <param name="obb">武器OBB</param>
        /// <param name="go">obbの元となるオブジェクト</param>
        /// <returns>true:接触、false:非接触</returns>
        public bool IsCollideBoxOBB(OBB weaponOBB)
        {
            // 判定済みの敵は無視
            if (_hitEnemies.Contains(_enemyOBB))
                return false;

            // 中心間の距離の取得
            Vector3 distance  = weaponOBB.Center - _enemyOBB.Center;

            // 検証軸を用いた距離の比較
            // 武器を検証軸とした場合
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, weaponOBB.AxisX, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, weaponOBB.AxisY, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, weaponOBB.AxisZ, distance)) return false;
            // 敵を検証軸とした場合
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, _enemyOBB.AxisX, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, _enemyOBB.AxisY, distance)) return false;
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, _enemyOBB.AxisZ, distance)) return false;

            // 分離軸同士の外積を用いた距離の比較
            Vector3 cross = Vector3.zero;
            // 武器のX軸との比較
            cross = Vector3.Cross(weaponOBB.AxisX, _enemyOBB.AxisX);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisX, _enemyOBB.AxisY);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisX, _enemyOBB.AxisZ);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            // 武器のY軸との比較
            cross = Vector3.Cross(weaponOBB.AxisY, _enemyOBB.AxisX);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisY, _enemyOBB.AxisY);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisY, _enemyOBB.AxisZ);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            // 武器のZ軸との比較
            cross = Vector3.Cross(weaponOBB.AxisZ, _enemyOBB.AxisX);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisZ, _enemyOBB.AxisY);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;
            cross = Vector3.Cross(weaponOBB.AxisZ, _enemyOBB.AxisZ);
            if (!CompareLengthOBB(weaponOBB, _enemyOBB, cross, distance)) return false;

            // 接触済みの敵を判定Listに格納
            _hitEnemies.Add(_enemyOBB);

            return true;
        }

        /// <summary>
        /// 指定した武器OBBと敵OBBの距離比較
        /// </summary>
        /// <param name="separating">指定した分離軸</param>
        /// <param name="distance">2点間の距離</param>
        /// <returns></returns>
        bool CompareLengthOBB(OBB weaponOBB, OBB enemyOBB, Vector3 separating, Vector3 distance)
        {
            // 検証軸上の武器と敵の距離
            // マイナスのベクトルである可能性があるため絶対値化
            float length = Mathf.Abs(Vector3.Dot(separating, distance));

            // 特定の検証軸上における武器の距離の半分を求める
            float weaponDist =
                Mathf.Abs(Vector3.Dot(weaponOBB.AxisX, separating)) * weaponOBB.Radius.x +
                Mathf.Abs(Vector3.Dot(weaponOBB.AxisY, separating)) * weaponOBB.Radius.y +
                Mathf.Abs(Vector3.Dot(weaponOBB.AxisZ, separating)) * weaponOBB.Radius.z;

            // 特定の検証軸上における敵の距離の半分を求める
            float enemyDist =
                Mathf.Abs(Vector3.Dot(enemyOBB.AxisX, separating)) * enemyOBB.Radius.x +
                Mathf.Abs(Vector3.Dot(enemyOBB.AxisY, separating)) * enemyOBB.Radius.y +
                Mathf.Abs(Vector3.Dot(enemyOBB.AxisZ, separating)) * enemyOBB.Radius.z;

            // 武器と敵の距離と、上記の距離の合計を比較
            if (length > weaponDist + enemyDist)
                return false;

            return true;
        }

        /// <summary>
        /// 判定のリセット
        /// </summary>
        public void ResetHitEnemies()
        {
            _hitEnemies.Clear();
        }

        #region デバッグ用
        public void ChangeEnemyColor()
        {
            _enemyMeshRenderer.material = _hitMaterial;
        }

        public void ResetEnemyColor()
        {
            _enemyMeshRenderer.material = _noHitMaterial;
        }
        #endregion
    }
}
