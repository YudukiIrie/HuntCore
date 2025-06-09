using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// オブジェクトプール用親クラス
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        Dictionary<int, List<GameObject>> _pooledGameObjects = new();

        /// <summary>
        /// 指定したプレハブを生成
        /// プールにあり:アクティブ, プールになし:生成
        /// </summary>
        /// <param name="prefab">生成するプレハブ</param>
        /// <param name="parent">生成後の親オブジェクト</param>
        /// <param name="position">生成後座標</param>
        /// <param name="rotation">生成後回転値</param>
        /// <returns>生成済みゲームオブジェクト</returns>
        protected GameObject GetGameObject(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            // プレハブのインスタンスIDをkeyとする
            int key = prefab.GetInstanceID();
            
            // Dictionaryにkeyが存在しない場合Listを新規作成
            if (!_pooledGameObjects.ContainsKey(key))
                _pooledGameObjects.Add(key, new List<GameObject>());

            // プレハブIDをkeyとして該当Listを取得
            List<GameObject> gameObjects = _pooledGameObjects[key];

            foreach (GameObject pooledGO in gameObjects)
            {
                if (pooledGO == null) continue;

                // === 使用可能な場合 ===
                if (!pooledGO.activeInHierarchy)
                {
                    pooledGO.transform.position = position;
                    pooledGO.transform.rotation = rotation;
                    pooledGO.SetActive(true);
                    return pooledGO;
                }
            }

            // === 使用不可の場合 ===
            GameObject go = Instantiate(prefab, position, rotation, parent);

            // 名前の変更
            go.name = prefab.name + "(" + string.Format("{0}", gameObjects.Count) + ")";

            gameObjects.Add(go);
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// ゲームオブジェクトをプールに戻す
        /// </summary>
        /// <param name="go">戻すゲームオブジェクト</param>
        protected void ReleaseGameObject(GameObject go)
        {
            go.SetActive(false);
        }
    }
}
