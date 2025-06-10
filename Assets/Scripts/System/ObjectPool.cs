using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    /// <summary>
    /// �I�u�W�F�N�g�v�[���p�e�N���X
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        Dictionary<int, List<GameObject>> _pooledGameObjects = new();

        /// <summary>
        /// �w�肵���v���n�u�𐶐�
        /// �v�[���ɂ���:�A�N�e�B�u, �v�[���ɂȂ�:����
        /// </summary>
        /// <param name="prefab">��������v���n�u</param>
        /// <param name="parent">������̐e�I�u�W�F�N�g</param>
        /// <param name="position">��������W</param>
        /// <param name="rotation">�������]�l</param>
        /// <returns>�����ς݃Q�[���I�u�W�F�N�g</returns>
        protected GameObject GetGameObject(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            // �v���n�u�̃C���X�^���XID��key�Ƃ���
            int key = prefab.GetInstanceID();
            
            // Dictionary��key�����݂��Ȃ��ꍇList��V�K�쐬
            if (!_pooledGameObjects.ContainsKey(key))
                _pooledGameObjects.Add(key, new List<GameObject>());

            // �v���n�uID��key�Ƃ��ĊY��List���擾
            List<GameObject> gameObjects = _pooledGameObjects[key];

            foreach (GameObject pooledGO in gameObjects)
            {
                if (pooledGO == null) continue;

                // === �g�p�\�ȏꍇ ===
                if (!pooledGO.activeInHierarchy)
                {
                    pooledGO.transform.position = position;
                    pooledGO.transform.rotation = rotation;
                    pooledGO.SetActive(true);
                    return pooledGO;
                }
            }

            // === �g�p�s�̏ꍇ ===
            GameObject go = Instantiate(prefab, position, rotation, parent);

            // ���O�̕ύX
            go.name = prefab.name + "(" + string.Format("{0}", gameObjects.Count) + ")";

            gameObjects.Add(go);
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// �Q�[���I�u�W�F�N�g���v�[���ɖ߂�
        /// </summary>
        /// <param name="go">�߂��Q�[���I�u�W�F�N�g</param>
        protected void ReleaseGameObject(GameObject go)
        {
            go.SetActive(false);
        }
    }
}
