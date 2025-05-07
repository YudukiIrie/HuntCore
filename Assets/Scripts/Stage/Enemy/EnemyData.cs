using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemy
{
    /// <summary>
    /// �G���p�X�N���v�^�u���I�u�W�F�N�g
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData")]
    [System.Serializable]
    public class EnemyData : ScriptableObject
    {
        // �ۑ���p�X
        const string PATH = "EnemyData";

        // �A�N�Z�X�p�̃C���X�^���X
        static EnemyData _data;
        public static EnemyData Data
        {
            get
            {
                if (_data == null)
                {
                    // �A�N�Z�X���ꂽ�烊�\�[�X�ɂ���p�X���̃I�u�W�F�N�g��ǂݍ���
                    _data = Resources.Load<EnemyData>(PATH);

                    // �ǂݍ��ݎ��s���̃G���[
                    if (_data == null)
                        Debug.LogError(PATH + "is not found.");
                }
                return _data;
            }
        }

        // �G���l���
        public Vector3 EnemySize => _enemySize;
        [Header("�G�T�C�Y")]
        [SerializeField] Vector3 _enemySize;
    }
}
