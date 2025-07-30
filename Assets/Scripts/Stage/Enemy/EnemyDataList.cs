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
        public Vector3 Size => _size;
        [Header("�G�T�C�Y")]
        [SerializeField] Vector3 _size;

        public float HeadRadius => _headRadius;
        [Header("�G�����a")]
        [SerializeField] float _headRadius;

        public Vector3 WingSize => _wingSize;
        [Header("�G���T�C�Y")]
        [SerializeField] Vector3 _wingSize;

        public Vector3 WingRootSize => _wingRootSize;
        [Header("�G���t�����T�C�Y")]
        [SerializeField] Vector3 _wingRootSize;

        public Vector2 ClawSize => _clawSize;
        [Header("�܃T�C�Y(X:���a, Y:����)")]
        [SerializeField] Vector2 _clawSize;

        public Vector2 NeckSize => _neckSize;
        [Header("��T�C�Y(X:���a, Y:����)")]
        [SerializeField] Vector2 _neckSize;

        public Vector2 LegSize => _legSize;
        [Header("�O���T�C�Y(X:���a, Y:����)")]
        [SerializeField] Vector2 _legSize;

        public float FindDistance => _findDistance;
        [Header("�v���C���[��������")]
        [SerializeField] float _findDistance;

        public float RoarDistance => _roarDistance;
        [Header("���K����")]
        [SerializeField] float _roarDistance;

        public float StopDistance => _stopDistance;
        [Header("�ǐՒ�~����")]
        [SerializeField] float _stopDistance;

        public float AttackDistance => _attackDistance;
        [Header("�U���\�v���C���[�ԋ���")]
        [SerializeField] float _attackDistance;

        public float ChaseSpeed => _chaseSpeed;
        [Header("�ǐՑ��x")]
        [SerializeField] float _chaseSpeed;

        public float ChaseRotSpeed => _chaseRotSpeed;
        [Header("�ǐՎ���]���x")]
        [SerializeField] float _chaseRotSpeed;

        public float AttackInterval => _attackInterval;
        [Header("�U���\�Ԋu")]
        [SerializeField] float _attackInterval;

        public float AttackProb => _attackProb;
        [Header("�ʏ�U���g�p�m��")]
        [SerializeField] float _attackProb;

        public float AnimBlendTime => _animBlendTime;
        [Header("�A�j���[�V�����u�����h����")]
        [SerializeField] float _animBlendTime;

        public Vector2 AttackHitWindow => _attackHitWindow;
        [Header("�ʏ�U�������蔻��L�����(X:�J�n, Y:�I��)")]
        [SerializeField] Vector2 _attackHitWindow;

        public Vector2 ClawHitWindow => _clawHitWindow;
        [Header("�܍U�������蔻��L�����(X:�J�n, Y:�I��)")]
        [SerializeField] Vector2 _clawHitWindow;

        public float LimitAngle => _limitAngle;
        [Header("�ΏۂƂ̌��E�p�x")]
        [SerializeField] float _limitAngle;

        public float AttackAngle => _attackAngle;
        [Header("�U���\�p�x")]
        [SerializeField] float _attackAngle;

        public float TurnSpeed => _turnSpeed;
        [Header("�����]�����x")]
        [SerializeField] float _turnSpeed;

        public float DownDuration => _downDuration;
        [Header("�_�E������")]
        [SerializeField] float _downDuration;
    }
}
