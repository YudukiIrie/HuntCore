using System.Collections.Generic;
using UnityEngine;

namespace Stage.Effects
{
    /// <summary>
    /// 血しぶきエフェクトの生成
    /// </summary>
    public class BloodFXSpawner : ObjectPool
    {
        [Header("血しぶきエフェクト")]
        [SerializeField] GameObject _bloodFX;

        // 初期生成プール関連
        const int INITIAL_POOL_SIZE = 5;
        List<GameObject> _initialPool = new(INITIAL_POOL_SIZE);

        void Start()
        {
            InitialSpawn();
            HideEffects();
        }

        /// <summary>
        /// 初期生成
        /// </summary>
        void InitialSpawn()
        {
            // プレイ中の高負荷を避けるため
            // あらかじめ必要数を生成
            for (int i = 0; i < INITIAL_POOL_SIZE; ++i)
            {
                GameObject fx = GetGameObject(_bloodFX, transform, Vector3.zero, Quaternion.identity);
                _initialPool.Add(fx);
            }
        }

        /// <summary>
        /// 初期生成したエフェクトの非表示
        /// </summary>
        void HideEffects()
        {
            foreach (GameObject fx in _initialPool)
                fx.SetActive(false);
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="pos">生成時座標</param>
        public void Spawn(Vector3 pos)
        {
            var fx = GetGameObject(_bloodFX, transform, pos, Quaternion.identity);
        }
    }
}