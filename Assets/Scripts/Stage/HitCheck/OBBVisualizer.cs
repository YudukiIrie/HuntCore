using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB可視化用クラス
    /// </summary>
    public class OBBVisualizer : MonoBehaviour
    {
        [Header("OBB可視化用ゲームオブジェクト")]
        [SerializeField] GameObject _obbVisualBox;

        [Header("OBB非接触マテリアル")]
        [SerializeField] Material _obbNoHitImage;

        [Header("OBB接触マテリアル")]
        [SerializeField] Material _obbHitImage;

        [Header("当たり判定クラス")]
        [SerializeField] OBBHitChecker _hitChecker;

        // 各OBB可視化ゲームオブジェクト
        GameObject _playerOBBVisualBox;
        GameObject _swordOBBVisualBox;
        GameObject _enemyOBBVisualBox;
        GameObject _enemyHeadOBBVisualBox;

        //MeshRenderer _playerOBBRenderer;
        //MeshRenderer _swordOBBRenderer;
        //MeshRenderer _enemyOBBRenderer;
        //MeshRenderer _enemyHeadOBBRenderer;

        void Start()
        {
            // 各OBB可視化ゲームオブジェクトの作成
            //OBB obb;
            //obb = _hitChecker.PlayerOBB;
            //_playerOBBVisualBox = 
            //    CreateVisualBox(obb.Center, _hitChecker.Player.transform.rotation, obb.Radius);
            //_playerOBBRenderer = _playerOBBVisualBox.GetComponent<MeshRenderer>();
            //obb = _hitChecker.GreatSwordOBB;
            //_swordOBBVisualBox = 
            //    CreateVisualBox(obb.Center, _hitChecker.GreatSword.transform.rotation, obb.Radius);
            //_swordOBBRenderer = _swordOBBVisualBox.GetComponent<MeshRenderer>();

            //obb = _hitChecker.EnemyOBB;
            //_enemyOBBVisualBox = 
            //    CreateVisualBox(obb.Center, _hitChecker.Enemy.transform.rotation, obb.Radius);
            //_enemyOBBRenderer = _enemyOBBVisualBox.GetComponent<MeshRenderer>();

            //obb = _hitChecker.EnemyHeadOBB;
            //_enemyHeadOBBVisualBox = 
            //    CreateVisualBox(obb.Center, _hitChecker.EnemyHead.transform.rotation, obb.Radius);
            //_enemyHeadOBBRenderer = _enemyHeadOBBVisualBox.GetComponent<MeshRenderer>();

            _playerOBBVisualBox = CreateVisualBox(_hitChecker.PlayerOBB.VisualBox);
            _swordOBBVisualBox = CreateVisualBox(_hitChecker.GreatSwordOBB.VisualBox);
            _enemyOBBVisualBox = CreateVisualBox(_hitChecker.EnemyOBB.VisualBox);
            _enemyHeadOBBVisualBox = CreateVisualBox(_hitChecker.EnemyHeadOBB.VisualBox);
        }

        void Update()
        {
            //UpdateVisualBoxInfo(_playerOBBVisualBox, _hitChecker.PlayerOBB, _hitChecker.Player.transform.rotation, _playerOBBRenderer);
            //UpdateVisualBoxInfo(_swordOBBVisualBox, _hitChecker.GreatSwordOBB, _hitChecker.GreatSword.transform.rotation, _swordOBBRenderer);
            //UpdateVisualBoxInfo(_enemyOBBVisualBox, _hitChecker.EnemyOBB, _hitChecker.Enemy.transform.rotation, _enemyOBBRenderer);
            //UpdateVisualBoxInfo(_enemyHeadOBBVisualBox, _hitChecker.EnemyHeadOBB, _hitChecker.EnemyHead.transform.rotation, _enemyHeadOBBRenderer);
            UpdateVisualBoxInfo(_playerOBBVisualBox, _hitChecker.PlayerOBB);
            UpdateVisualBoxInfo(_swordOBBVisualBox, _hitChecker.GreatSwordOBB);
            UpdateVisualBoxInfo(_enemyOBBVisualBox, _hitChecker.EnemyOBB);
            UpdateVisualBoxInfo(_enemyHeadOBBVisualBox, _hitChecker.EnemyHeadOBB);
        }

        /// <summary>
        /// OBB可視化ゲームオブジェクトの作成
        /// </summary>
        /// <param name="center">作成時中心座標</param>
        /// <param name="rotation">作成時回転値</param>
        //GameObject CreateVisualBox(Vector3 center, Quaternion rotation, Vector3 radius)
        //{
        //    var go = Instantiate(_obbVisualBox, center, rotation, transform);

        //    var scale = radius * 2;
        //    go.transform.localScale = scale;

        //    return go;
        //}

        GameObject CreateVisualBox(OBBVisualBox box)
        {
            var go = Instantiate(_obbVisualBox, box.Position, box.Rotation, transform);
            go.transform.localScale = box.Scale;

            box.SetMeshRenderer(go.GetComponent<MeshRenderer>());

            return go;
        }

        /// <summary>
        /// OBB可視化ゲームオブジェクト情報の更新
        /// </summary>
        /// <param name="go">更新するゲームオブジェクト</param>
        /// <param name="obb">更新元OBBオブジェクト</param>
        /// <param name="rotation">更新時回転値</param>
        //void UpdateVisualBoxInfo(GameObject go, OBB obb, Quaternion rotation, MeshRenderer mr)
        //{
        //    go.transform.position = obb.Center;
        //    go.transform.rotation = rotation;

        //    Material[] mats = mr.materials;
        //    mats[0] = obb.IsHit ? _obbHitImage : _obbNoHitImage;
        //    mr.materials = mats;
        //}

        void UpdateVisualBoxInfo(GameObject go, OBB obb)
        {
            go.transform.position = obb.VisualBox.Position;
            go.transform.rotation = obb.VisualBox.Rotation;

            var mr = obb.VisualBox.Renderer;
            Material[] mats = mr.materials;
            mats[0] = obb.IsHit ? _obbHitImage : _obbNoHitImage;
            mr.materials = mats;
        }
    }
}