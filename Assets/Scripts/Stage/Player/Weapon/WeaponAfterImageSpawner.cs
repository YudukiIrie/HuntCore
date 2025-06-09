using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// ����c�������p�N���X
    /// </summary>
    public class WeaponAfterImageSpawner : ObjectPool
    {
        int _initialPoolSize;
        List<GameObject> _initialPool;

        GameObject _prefab;

        bool _canSpawn = true;
        float _spawnTimer = 0.0f;
        float _interval;

        void Start()
        {
            _initialPoolSize = WeaponData.Data.InitialPoolSize;
            _initialPool = new(_initialPoolSize);

            _prefab = WeaponData.Data.AfterImagePrefab;
            _interval = WeaponData.Data.AfterImageInterval;

            InitialSpawn();
            HideAfterImages();
        }

        /// <summary>
        /// ��������
        /// </summary>
        void InitialSpawn()
        {
            // �v���C���̍����ׂ�h������
            // ���炩���ߕK�v���𐶐�
            for (int i = 0; i < _initialPoolSize; ++i)
            {
                GameObject go = GetGameObject(_prefab, transform, Vector3.zero, Quaternion.identity);
                _initialPool.Add(go);
            }
        }

        /// <summary>
        /// �������������Q�[���I�u�W�F�N�g�̔�\��
        /// </summary>
        void HideAfterImages()
        {
            foreach (GameObject go in _initialPool)
                ReleaseGameObject(go);
        }

        /// <summary>
        /// ���Ԋu�Ŏc���𐶐�
        /// </summary>
        /// <param name="transform">������Transform</param>
        public void Spawn(Transform transform)
        {
            if (_canSpawn)
            {
                // === ���W�Ɖ�]�l�̎擾 ===
                var position = transform.position;
                var rotation = transform.rotation;

                // === ���� ===
                var go = GetGameObject(_prefab, this.transform, position, rotation);
                _canSpawn = false;
            }
            else
            {
                _spawnTimer += Time.deltaTime;
                if (_spawnTimer >= _interval)
                {
                    _spawnTimer = 0.0f;
                    _canSpawn = true;
                }
            }
        }
    }
}
