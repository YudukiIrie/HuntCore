using System.Collections.Generic;
using UnityEngine;

namespace Stage.Effects
{
    /// <summary>
    /// �����Ԃ��G�t�F�N�g�̐���
    /// </summary>
    public class BloodFXSpawner : ObjectPool
    {
        [Header("�����Ԃ��G�t�F�N�g")]
        [SerializeField] GameObject _bloodFX;

        // ���������v�[���֘A
        const int INITIAL_POOL_SIZE = 5;
        List<GameObject> _initialPool = new(INITIAL_POOL_SIZE);

        void Start()
        {
            InitialSpawn();
            HideEffects();
        }

        /// <summary>
        /// ��������
        /// </summary>
        void InitialSpawn()
        {
            // �v���C���̍����ׂ�����邽��
            // ���炩���ߕK�v���𐶐�
            for (int i = 0; i < INITIAL_POOL_SIZE; ++i)
            {
                GameObject fx = GetGameObject(_bloodFX, transform, Vector3.zero, Quaternion.identity);
                _initialPool.Add(fx);
            }
        }

        /// <summary>
        /// �������������G�t�F�N�g�̔�\��
        /// </summary>
        void HideEffects()
        {
            foreach (GameObject fx in _initialPool)
                fx.SetActive(false);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pos">���������W</param>
        public void Spawn(Vector3 pos)
        {
            var fx = GetGameObject(_bloodFX, transform, pos, Quaternion.identity);
        }
    }
}