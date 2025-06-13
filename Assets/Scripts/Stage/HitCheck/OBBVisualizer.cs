using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitCheck
{
    /// <summary>
    /// OBB�����p�N���X
    /// </summary>
    public class OBBVisualizer : MonoBehaviour
    {
        [Header("OBB�����p�Q�[���I�u�W�F�N�g")]
        [SerializeField] GameObject _obbVisualBox;

        [Header("OBB��ڐG�}�e���A��")]
        [SerializeField] Material _obbNoHitImage;

        [Header("OBB�ڐG�}�e���A��")]
        [SerializeField] Material _obbHitImage;

        [Header("�����蔻��N���X")]
        [SerializeField] OBBHitChecker _hitChecker;

        // �eOBB�����Q�[���I�u�W�F�N�g
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
            // �eOBB�����Q�[���I�u�W�F�N�g�̍쐬
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
        /// OBB�����Q�[���I�u�W�F�N�g�̍쐬
        /// </summary>
        /// <param name="center">�쐬�����S���W</param>
        /// <param name="rotation">�쐬����]�l</param>
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
        /// OBB�����Q�[���I�u�W�F�N�g���̍X�V
        /// </summary>
        /// <param name="go">�X�V����Q�[���I�u�W�F�N�g</param>
        /// <param name="obb">�X�V��OBB�I�u�W�F�N�g</param>
        /// <param name="rotation">�X�V����]�l</param>
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