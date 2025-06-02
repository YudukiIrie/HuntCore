using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G���p�X�N���v�^�u���I�u�W�F�N�g
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyDataList", menuName = "ScriptableObject/EnemyDataList")]
    [System.Serializable]
    public class EnemyDataList : ScriptableObject
    {
        // �G�f�[�^��List�ŕێ�
        public List<EnemyData> Enemies = new();

        // �ۑ���p�X
        const string PATH = "EnemyDataList";

        // �A�N�Z�X�p�̃C���X�^���X
        static EnemyDataList _data;
        public static EnemyDataList Data
        {
            get
            {
                if (_data == null)
                {
                    // �A�N�Z�X���ꂽ�烊�\�[�X�ɂ���p�X���̃I�u�W�F�N�g��ǂݍ���
                    _data = Resources.Load<EnemyDataList>(PATH);

                    // �ǂݍ��ݎ��s���̃G���[
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        /// <summary>
        /// �w�肵���G�^�C�v�̏����擾
        /// </summary>
        /// <param name="type">�w�肵���G�^�C�v</param>
        /// <returns>�G�f�[�^</returns>
        public EnemyData GetData(EnemyData.Type type)
        {
            return Enemies.Find(item => item.EnemyType == type);
        }
    }

    [System.Serializable]
    public class EnemyData
    {
        // �G���
        public enum Type
        {
            BossEnemy, // �{�X
        }

        public Type EnemyType => _enemyType;
        [Header("�G���")]
        [SerializeField] Type _enemyType;

        // �G���l���
        public Vector3 EnemySize => _enemySize;
        [Header("�G�T�C�Y")]
        [SerializeField] Vector3 _enemySize;

        public float FindDistance => _findDistance;
        [Header("�v���C���[��������")]
        [SerializeField] float _findDistance;

        public float RoarDistance => _roarDistance;
        [Header("���K����")]
        [SerializeField] float _roarDistance;
    }
}
