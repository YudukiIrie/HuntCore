using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stage.Player;
using Stage.Enemy;

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

        // 各オブジェクトOBB
        OBB _greatSwordOBB;
        OBB _enemyOBB;

        void Start()
        {
            // 各オブジェクトOBB情報の登録と実体の作成
            _greatSwordOBB = new OBB(_greatSword.transform, WeaponData.Data.GreatSwordSize);
            _enemyOBB = new OBB(_enemy.transform, EnemyData.Data.EnemySize);
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
        public bool IsCollideBoxOBB(OBB obb, GameObject go)
        {


            return true;
        }
    }
}
