using Stage.Enemies;
using Stage.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Stage.UI
{
    /// <summary>
    /// 攻撃ヒットUI
    /// </summary>
    public class HitUI : MonoBehaviour
    {
        [Header("プレイヤーヒット数テキスト")]
        [SerializeField] Text _playerHitText;

        [Header("敵ヒット数テキスト")]
        [SerializeField] Text _enemyHitText;

        [Header("ブロック数テキスト")]
        [SerializeField] Text _blockNumText;

        [Header("プレイヤークラス")]
        [SerializeField] Player _player;

        [Header("敵クラス")]
        [SerializeField] Enemy _enemy;

        void LateUpdate()
        {
            UpdateHitUI();

            UpdateGuardUI();
        }

        /// <summary>
        /// ヒットUIの更新
        /// </summary>
        void UpdateHitUI()
        {
            _playerHitText.text = _player.HitNum.ToString();
            _enemyHitText.text = _enemy.HitNum.ToString();
        }

        /// <summary>
        /// ガードUIの更新
        /// </summary>
        void UpdateGuardUI()
        {
            _blockNumText.text = _player.BlockNum.ToString();
        }
    }
}