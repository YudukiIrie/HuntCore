using Stage.Enemies;
using Stage.Players;
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

        [Header("プレイヤークラス")]
        [SerializeField] Player _player;

        [Header("敵クラス")]
        [SerializeField] Enemy _enemy;

        // 可視化ゲームオブジェクトをListで保持
        List<GameObject> _visualBoxes = new();

        // 表示切替関連
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
        /// 可視化ゲームオブジェクトの作成
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
        /// OBB可視化ゲームオブジェクトの表示切替
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