using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// ����c���N���X
    /// </summary>
    public class WeaponAfterImage : ObjectPool
    {
        float _elapsedTime = 0.0f; // ������o�ߎ���
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

            // ��莞�ԂŎ��g���폜
            if (_elapsedTime >= _duration)
                ReleaseGameObject(gameObject);
        }
    }
}
