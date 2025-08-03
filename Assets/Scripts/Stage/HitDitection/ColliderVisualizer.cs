using Stage.Enemies;
using Stage.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// OBB�����p�N���X
    /// </summary>
    public class ColliderVisualizer : MonoBehaviour
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
            GameObject visualGO;
            // === �v���C���[VisualCollider ===
            foreach (var collider in _player.Collider.Colliders)
            {
                if (collider.Shape == ColliderShape.OBB)
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
            foreach (var collider in _enemy.Collider.Colliders)
            {
                if (collider.Shape == ColliderShape.OBB)
                {
                    _visualColliders.Add(visualGO = Instantiate(_visualOBB, transform));
                    collider.CreateVisualCollider(visualGO, _noHitImage, _hitImage);
                }
                else if (collider.Shape == ColliderShape.Sphere)
                {
                    _visualColliders.Add(visualGO = Instantiate(_visualSphere, transform));
                    collider.CreateVisualCollider(visualGO, _noHitImage, _hitImage);
                }
                else if (collider.Shape == ColliderShape.Capsule)
                {
                    _visualColliders.Add(visualGO = Instantiate(_visualCapsule, transform));
                    collider.CreateVisualCollider(visualGO, _noHitImage, _hitImage);
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