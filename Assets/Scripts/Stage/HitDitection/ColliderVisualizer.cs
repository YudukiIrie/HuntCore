using Stage.Enemies;
using Stage.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.HitDetection
{
    /// <summary>
    /// OBB可視化用クラス
    /// </summary>
    public class ColliderVisualizer : MonoBehaviour
    {
        [Header("OBB可視化用ゲームオブジェクト")]
        [SerializeField] GameObject _visualOBB;

        [Header("HitSphere可視化用ゲームオブジェクト")]
        [SerializeField] GameObject _visualSphere;

        [Header("HitCapsule可視化用ゲームオブジェクト")]
        [SerializeField] GameObject _visualCapsule;

        [Header("非接触マテリアル")]
        [SerializeField] Material _noHitImage;

        [Header("接触マテリアル")]
        [SerializeField] Material _hitImage;

        [Header("プレイヤークラス")]
        [SerializeField] Player _player;

        [Header("敵クラス")]
        [SerializeField] Enemy _enemy;

        // 可視化ゲームオブジェクトをListで保持
        List<GameObject> _visualColliders = new();

        // 表示切替関連
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
        /// 可視化ゲームオブジェクトの作成
        /// </summary>
        void CreateVisualColliders()
        {
            // === プレイヤーVisualCollider ===
            foreach (var collider in _player.Collider.Colliders)
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

            // === 敵VisualCollider ===
            foreach (var collider in _enemy.Collider.Colliders)
            {
                GameObject visualCollider;
                if (collider.Shape == HitCollider.ColliderShape.OBB)
                {
                    _visualColliders.Add(visualCollider = Instantiate(_visualOBB, transform));
                    collider.CreateVisualCollider(visualCollider, _noHitImage, _hitImage);
                }
                else if (collider.Shape == HitCollider.ColliderShape.Sphere)
                {
                    _visualColliders.Add(visualCollider = Instantiate(_visualSphere, transform));
                    collider.CreateVisualCollider(visualCollider, _noHitImage, _hitImage);
                }
                else if (collider.Shape == HitCollider.ColliderShape.Capsule)
                {
                    _visualColliders.Add(visualCollider = Instantiate(_visualCapsule, transform));
                    collider.CreateVisualCollider(visualCollider, _noHitImage, _hitImage);
                }
            }
        }

        /// <summary>
        /// 可視化ゲームオブジェクトの表示切替
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