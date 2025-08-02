using UnityEngine;

namespace Stage.Effects
{
    /// <summary>
    /// 血しぶきエフェクト
    /// </summary>
    public class BloodFX : ObjectPool
    {
        [Header("非表示までの時間")]
        [SerializeField] float _lifeTime;

        float _lifeTimer;   // 生存時間タイマー

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
        /// 一定時間で自身を非表示
        /// </summary>
        void Hide()
        {
            if (_lifeTimer >= _lifeTime)
                ReleaseGameObject(gameObject);
        }
    }
}