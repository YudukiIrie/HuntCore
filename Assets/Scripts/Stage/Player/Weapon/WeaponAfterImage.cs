using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// 武器残像クラス
    /// </summary>
    public class WeaponAfterImage : ObjectPool
    {
        float _elapsedTime = 0.0f; // 生成後経過時間
        float _duration;

        void Start()
        {
            _duration = WeaponData.Data.AfterImageDuration;
        }

        void OnEnable()
        {
            _elapsedTime = 0.0f;
        }

        void Update()
        {
            _elapsedTime += Time.deltaTime;

            // 一定時間で自身を削除
            if (_elapsedTime >= _duration)
                ReleaseGameObject(gameObject);
        }
    }
}
