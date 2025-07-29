using Stage.Enemies;
using Stage.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// OBB�����p�N���X
    /// </summary>
    public class OBBVisualizer : MonoBehaviour
    {
        [Header("OBB�����p�Q�[���I�u�W�F�N�g")]
        [SerializeField] GameObject _visualOBB;

        [Header("HitSphere�����p�Q�[���I�u�W�F�N�g")]
        [SerializeField] GameObject _visualSphere;

        [Header("HitCapsule�����p�Q�[���I�u�W�F�N�g")]
        [SerializeField] GameObject _visualCapsule;

        [Header("��ڐG�}�e���A��")]
        [SerializeField] Material _noHitImage;

        [Header("�ڐG�}�e���A��")]
        [SerializeField] Material _hitImage;

        [Header("�v���C���[�N���X")]
        [SerializeField] Player _player;

        [Header("�G�N���X")]
        [SerializeField] Enemy _enemy;

        // �����Q�[���I�u�W�F�N�g��List�ŕێ�
        List<GameObject> _visualColliders = new();

        // �\���ؑ֊֘A
        const float SWITCH_DISPLAY_INTERVAL = 0.3f;
        float _switchDisplayTimer;
        bool _isActive = true;

        void Start()
        {
            CreateVisualColliders();
        }

        void Update()
        {
            SwitchDisplay();
        }

        /// <summary>
        /// �����Q�[���I�u�W�F�N�g�̍쐬
        /// </summary>
        void CreateVisualColliders()
        {
            // === �v���C���[VisualCollider ===
            foreach (var collider in _player.PlayerColliders)
            {
                GameObject visualGO;
                if (collider.Shape == HitCollider.ColliderShape.OBB)
                {
                    _visualColliders.Add(visualGO = Instantiate(_visualOBB, transform));
                    collider.CreateVisualCollider(visualGO, _noHitImage, _hitImage);
                }
                else
                {
                    _visualColliders.Add(visualGO = Instantiate(_visualCapsule, transform));
                    collider.CreateVisualCollider(visualGO, _noHitImage, _hitImage);
                }
            }

            // === �GVisualCollider ===
            foreach (var collider in _enemy.EnemyColliders)
            {
                GameObject visualCollider;
                if (collider.Shape == HitCollider.ColliderShape.OBB)
                {
                    _visualColliders.Add(visualCollider = Instantiate(_visualOBB, transform));
                    collider.CreateVisualCollider(visualCollider, _noHitImage, _hitImage);
                }
                else
                {
                    _visualColliders.Add(visualCollider = Instantiate(_visualSphere, transform));
                    collider.CreateVisualCollider(visualCollider, _noHitImage, _hitImage);
                }
            }

            // �e�X�g�p
            foreach (var collider in _player.TestColliders)
            {
                GameObject visualCollider;
                if (collider.Shape == HitCollider.ColliderShape.OBB)
                {
                    _visualColliders.Add(visualCollider = Instantiate(_visualOBB, transform));
                    collider.CreateVisualCollider(visualCollider, _noHitImage, _hitImage);
                }
                else
                {
                    _visualColliders.Add(visualCollider = Instantiate(_visualSphere, transform));
                    collider.CreateVisualCollider(visualCollider, _noHitImage, _hitImage);
                }
            }
        }

        /// <summary>
        /// �����Q�[���I�u�W�F�N�g�̕\���ؑ�
        /// </summary>
        void SwitchDisplay()
        {
            _switchDisplayTimer += Time.deltaTime;

            if (_switchDisplayTimer >= SWITCH_DISPLAY_INTERVAL)
            {
                if (GameManager.Action.Player.SwitchDisplay.IsPressed())
                {
                    _isActive = !_isActive;
                    foreach (GameObject visualBox in _visualColliders)
                        visualBox.SetActive(_isActive);

                    _switchDisplayTimer = 0.0f;
                }
            }
        }
    }
}