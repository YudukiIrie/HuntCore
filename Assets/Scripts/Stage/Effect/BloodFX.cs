using UnityEngine;

namespace Stage.Effects
{
    /// <summary>
    /// �����Ԃ��G�t�F�N�g
    /// </summary>
    public class BloodFX : ObjectPool
    {
        [Header("��\���܂ł̎���")]
        [SerializeField] float _lifeTime;

        float _lifeTimer;   // �������ԃ^�C�}�[

        void OnEnable()
        {
            _lifeTimer = 0.0f;
        }

        void Update()
        {
            _lifeTimer += Time.deltaTime;

            Hide();
        }

        /// <summary>
        /// ��莞�ԂŎ��g���\��
        /// </summary>
        void Hide()
        {
            if (_lifeTimer >= _lifeTime)
                ReleaseGameObject(gameObject);
        }
    }
}