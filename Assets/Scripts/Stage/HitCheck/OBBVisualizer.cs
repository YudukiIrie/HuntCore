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

        // �\���ؑ֊֘A
        const float SWITCH_DISPLAY_INTERVAL = 0.3f;
        float _switchDisplayTimer;
        bool _isActive = true;

        //void Start()
        //{
        //    // �eOBB�����Q�[���I�u�W�F�N�g�̍쐬
        //    _playerOBBVisualBox = CreateVisualBox(_hitChecker.PlayerOBB.VisualBox);
        //    _swordOBBVisualBox  = CreateVisualBox(_hitChecker.GreatSwordOBB.VisualBox);
        //    _enemyOBBVisualBox  = CreateVisualBox(_hitChecker.EnemyOBB.VisualBox);
        //    _enemyHeadOBBVisualBox = CreateVisualBox(_hitChecker.EnemyHeadOBB.VisualBox);
        //}

        //void Update()
        //{
        //    SwitchDisplay();
          
        //    // �eOBB�����Q�[���I�u�W�F�N�g���̍X�V
        //    UpdateVisualBoxInfo(_playerOBBVisualBox, _hitChecker.PlayerOBB);
        //    UpdateVisualBoxInfo(_swordOBBVisualBox, _hitChecker.GreatSwordOBB);
        //    UpdateVisualBoxInfo(_enemyOBBVisualBox, _hitChecker.EnemyOBB);
        //    UpdateVisualBoxInfo(_enemyHeadOBBVisualBox, _hitChecker.EnemyHeadOBB);
        //}

        /// <summary>
        /// OBB�����Q�[���I�u�W�F�N�g�̐���
        /// </summary>
        /// <param name="box">������OBBVisualbox</param>
        /// <returns>�����ς݃Q�[���I�u�W�F�N�g</returns>
        GameObject CreateVisualBox(OBBVisualBox box)
        {
            // ����
            var go = Instantiate(_obbVisualBox, box.Position, box.Rotation, transform);
            go.transform.localScale = box.Scale;

            // MeshRenderer�̓o�^
            box.SetMeshRenderer(go.GetComponent<MeshRenderer>());

            return go;
        }

        /// <summary>
        /// OBB�����Q�[���I�u�W�F�N�g���̍X�V
        /// </summary>
        /// <param name="go">�X�V����Q�[���I�u�W�F�N�g</param>
        /// <param name="obb">�X�V��OBB</param>
        void UpdateVisualBoxInfo(GameObject go, OBB obb)
        {
            // Transform���̍X�V
            go.transform.position = obb.VisualBox.Position;
            go.transform.rotation = obb.VisualBox.Rotation;

            // �}�e���A���̍X�V
            var mr = obb.VisualBox.Renderer;
            Material[] mats = mr.materials;
            mats[0] = obb.IsHit ? _obbHitImage : _obbNoHitImage;
            mr.materials = mats;
        }

        /// <summary>
        /// OBB�����Q�[���I�u�W�F�N�g�̕\���ؑ�
        /// </summary>
        void SwitchDisplay()
        {
            _switchDisplayTimer += Time.deltaTime;
            
            if (_switchDisplayTimer >= SWITCH_DISPLAY_INTERVAL)
            {
                if (GameManager.Action.Player.SwitchDisplay.IsPressed())
                {
                    _isActive = !_isActive;
                    _playerOBBVisualBox.SetActive(_isActive);
                    _swordOBBVisualBox.SetActive(_isActive);
                    _enemyOBBVisualBox.SetActive(_isActive);
                    _enemyHeadOBBVisualBox.SetActive(_isActive);

                    _switchDisplayTimer = 0.0f;
                }
            }
        }
    }
}