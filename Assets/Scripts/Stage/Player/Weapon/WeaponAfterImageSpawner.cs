using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// 武器残像生成用クラス
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
        /// 初期生成
        /// </summary>
        void InitialSpawn()
        {
            // プレイ中の高負荷を防ぐため
            // あらかじめ必要数を生成
            for (int i = 0; i < _initialPoolSize; ++i)
            {
                GameObject go = GetGameObject(_prefab, transform, Vector3.zero, Quaternion.identity);
                _initialPool.Add(go);
            }
        }

        /// <summary>
        /// 初期生成したゲームオブジェクトの非表示
        /// </summary>
        void HideAfterImages()
        {
            foreach (GameObject go in _initialPool)
                ReleaseGameObject(go);
        }

        /// <summary>
        /// 一定間隔で残像を生成
        /// </summary>
        /// <param name="transform">生成時Transform</param>
        public void Spawn(Transform transform)
        {
            if (_canSpawn)
            {
                // === 座標と回転値の取得 ===
                var position = transform.position;
                var rotation = transform.rotation;

                // === 生成 ===
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
