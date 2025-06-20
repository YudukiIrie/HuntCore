using Stage.HitCheck;
using Stage.Players;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�̋@�\�𑍊�
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        [field: Header("�v���C���[�N���X")]
        [field: SerializeField] public Player Player { get; private set; }

        [Header("�GOBB��Transform")]
        [SerializeField] Transform _enemyOBBTransform;

        [Header("�G��OBB��Transform")]
        [SerializeField] Transform _headOBBTransform;

        [Header("�G�E���rOBB��Transform")]
        [SerializeField] Transform _rWingOBBTransform;

        [Header("�G�����rOBB��Transform")]
        [SerializeField] Transform _lWingOBBTransform;

        [Header("�G�E���r�t����OBB��Transform")]
        [SerializeField] Transform _rWingRootOBBTransform;

        [Header("�G�����r�t����OBB��Transform")]
        [SerializeField] Transform _lWingRootOBBTransform;

        // �G�֘A�N���X
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // �R���|�[�l���g
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        // OBB
        public OBB EnemyOBB {  get; private set; }
        public OBB EnemyHeadOBB { get; private set; }
        public OBB EnemyRWingOBB { get; private set; }
        public OBB EnemyLWingOBB { get; private set; }
        public OBB EnemyRWingRootOBB { get; private set; }
        public OBB EnemyLWingRootOBB { get; private set; }
        // �GOBB�ꊇ�Ǘ��pList
        public List<OBB> EnemyOBBs { get; private set; } = new();

        // �U���֘A
        float _attackInterval;
        float _attackTimer; // �U���p�^�C�}�[
        bool _canAttack;    // �U����ԃt���O
        public int HitNum { get; private set; } // �U���q�b�g��

        void Awake()
        {
            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(_animator);

            CreateOBBs();
            
            _attackInterval = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackInterval;
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);    
        }

        void Update()
        {
            UpdateOBBInfo();

            UpdateAttackTimer();

            StateMachine.Update();
        }

        void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        /// <summary>
        /// OBB�̍쐬
        /// </summary>
        void CreateOBBs()
        {
            EnemyOBBs.Add(EnemyOBB =
                new OBB(_enemyOBBTransform,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemySize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyHeadOBB =
                new OBB(_headOBBTransform, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyHeadSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyRWingOBB =
                new OBB(_rWingOBBTransform, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyLWingOBB =
                new OBB(_lWingOBBTransform, 
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyRWingRootOBB =
                new OBB(_rWingRootOBBTransform,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingRootSize, OBB.OBBType.Body));

            EnemyOBBs.Add(EnemyLWingRootOBB =
                new OBB(_lWingRootOBBTransform,
                EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyWingRootSize, OBB.OBBType.Body));
        }

        /// <summary>
        /// OBB���̍X�V
        /// </summary>
        void UpdateOBBInfo()
        {
            EnemyOBB.UpdateInfo(_enemyOBBTransform);
            EnemyHeadOBB.UpdateInfo(_headOBBTransform);
            EnemyRWingOBB.UpdateInfo(_rWingOBBTransform);
            EnemyLWingOBB.UpdateInfo(_lWingOBBTransform);
            EnemyRWingRootOBB.UpdateInfo(_rWingRootOBBTransform);
            EnemyLWingRootOBB.UpdateInfo(_lWingRootOBBTransform);
        }

        /// <summary>
        /// �v���C���[�Ƃ̋�����Ԃ�
        /// </summary>
        public float GetDistanceToPlayer()
        {
            // ���W�̎擾
            var a = transform.position;
            var b = Player.transform.position;

            // �e�����̍��ق��擾
            var x = a.x - b.x;
            var y = a.y - b.y;
            var z = a.z - b.z;

            // �����̎Z�o
            return Mathf.Sqrt((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// �v���C���[�ւ̕����x�N�g����Ԃ�
        /// </summary>
        public Vector3 GetDirectionToPlayer()
        {
            return (Player.transform.position - transform.position).normalized;
        }

        /// <summary>
        /// ���g�̐��ʂƃv���C���[�Ƃ̊p�x��Ԃ�
        /// </summary>
        public float GetAngleToPlayer()
        {
            // ���ʃx�N�g���ƃv���C���[�ւ̕����x�N�g�������
            var v0 = transform.forward;
            var v1 = GetDirectionToPlayer();
            var dot = Vector3.Dot(v0, v1);

            // �p�x�����ߕԋp(�x���@)
            return Mathf.Acos(dot) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// �U���^�C�}�[�̍X�V
        /// </summary>
        void UpdateAttackTimer()
        {
            if (!_canAttack)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= _attackInterval)
                {
                    _attackTimer = 0.0f;
                    _canAttack = true;
                }
            }
        }

        /// <summary>
        /// �U����Ԃ̊m�F�Ɛ؂�ւ�
        /// </summary>
        /// <returns>true:�U����, false:�U���s��</returns>
        public bool CheckAttackState()
        {
            if (_canAttack)
            {
                _canAttack = false;
                return true;
            }

            return false;
        }

        public void IncreaseHitNum()
        {
            HitNum++;
        }
    }
}
