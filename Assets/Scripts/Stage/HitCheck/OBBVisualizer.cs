using Stage.Enemies;
using Stage.Players;
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

        [Header("�v���C���[�N���X")]
        [SerializeField] Player _player;

        [Header("�G�N���X")]
        [SerializeField] Enemy _enemy;

        // �����Q�[���I�u�W�F�N�g��List�ŕێ�
        List<GameObject> _visualBoxes = new();

        // �\���ؑ֊֘A
        const float SWITCH_DISPLAY_INTERVAL = 0.3f;
        float _switchDisplayTimer;
        bool _isActive = true;

        void Start()
        {
            CreateVisualBoxes();
        }

        void Update()
        {
            SwitchDisplay();
        }

        /// <summary>
        /// �����Q�[���I�u�W�F�N�g�̍쐬
        /// </summary>
        void CreateVisualBoxes()
        {
            GameObject visualBox;
            _visualBoxes.Add(visualBox = Instantiate(_obbVisualBox, transform));
            _player.PlayerOBB.VisualBox.SetGameObjectInfo(visualBox, _obbNoHitImage, _obbHitImage);

            _visualBoxes.Add(visualBox = Instantiate(_obbVisualBox, transform));
            _player.WeaponOBB.VisualBox.SetGameObjectInfo(visualBox, _obbNoHitImage, _obbHitImage);

            _visualBoxes.Add(visualBox = Instantiate(_obbVisualBox, transform));
            _enemy.EnemyOBB.VisualBox.SetGameObjectInfo(visualBox, _obbNoHitImage, _obbHitImage);

            _visualBoxes.Add(visualBox = Instantiate(_obbVisualBox, transform));
            _enemy.EnemyHeadOBB.VisualBox.SetGameObjectInfo(visualBox, _obbNoHitImage, _obbHitImage);

            _visualBoxes.Add(visualBox = Instantiate(_obbVisualBox, transform));
            _enemy.EnemyRWingOBB.VisualBox.SetGameObjectInfo(visualBox, _obbNoHitImage, _obbHitImage);

            _visualBoxes.Add(visualBox = Instantiate(_obbVisualBox, transform));
            _enemy.EnemyLWingOBB.VisualBox.SetGameObjectInfo(visualBox, _obbNoHitImage, _obbHitImage);
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
                    foreach (GameObject visualBox in _visualBoxes)
                        visualBox.SetActive(_isActive);

                    _switchDisplayTimer = 0.0f;
                }
            }
        }
    }
}