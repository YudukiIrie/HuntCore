using Stage.Enemies;
using Stage.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Stage.UI
{
    /// <summary>
    /// �U���q�b�gUI
    /// </summary>
    public class HitUI : MonoBehaviour
    {
        [Header("�v���C���[�q�b�g���e�L�X�g")]
        [SerializeField] Text _playerHitText;

        [Header("�G�q�b�g���e�L�X�g")]
        [SerializeField] Text _enemyHitText;

        [Header("�v���C���[�N���X")]
        [SerializeField] Player _player;

        [Header("�G�N���X")]
        [SerializeField] Enemy _enemy;

        void LateUpdate()
        {
            UpdateHitUI();
        }

        /// <summary>
        /// �q�b�gUI�̍X�V
        /// </summary>
        void UpdateHitUI()
        {
            _playerHitText.text = _player.HitNum.ToString();
            _enemyHitText.text = _enemy.HitNum.ToString();
        }
    }
}