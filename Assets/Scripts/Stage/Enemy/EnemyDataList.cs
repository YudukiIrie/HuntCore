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
            NormalEnemy, // �G���G
        }

        public Type EnemyType => _enemyType;
        [Header("�G���")]
        [SerializeField] Type _enemyType;

        // �G���l���
        public Vector3 EnemySize => _enemySize;
        [Header("�G�T�C�Y")]
        [SerializeField] Vector3 _enemySize;

        public float IdleToWanderPercent => _idleToWanderPercent;
        [Header("�ҋ@���炤����J�ڊm��")]
        [SerializeField] float _idleToWanderPercent;

        public float WanderToIdlePercent => _wanderToIdlePercent;
        [Header("���������ҋ@�J�ڊm��")]
        [SerializeField] float _wanderToIdlePercent;

        public float WanderSpeed => _wanderSpeed;
        [Header("��������x")]
        [SerializeField] float _wanderSpeed;

        public float WanderRotSpeed => _wanderRotSpeed;
        [Header("�������]���x")]
        [SerializeField] float _wanderRotSpeed;

        public float WanderStoppingDistance => _wanderStoppingDistance;
        [Header("��������ɖڕW���B�Ƃ���ڕW�܂ł̋���")]
        [SerializeField] float _wanderStoppingDistance;

        public float WanderRange => _wanderRange;
        [Header("��������ڕW�ݒ�͈�")]
        [SerializeField] float _wanderRange;
    }
}
