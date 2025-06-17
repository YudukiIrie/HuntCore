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
        [field: Header("�v���C���[")]
        [field: SerializeField] public Player Player { get; private set; }

        [field: Header("�����蔻��N���X")]
        [field: SerializeField] public OBBHitChecker HitChecker { get; private set; }

        [Header("�GOBB��Transform")]
        [SerializeField] Transform _enemyOBBTransform;

        [Header("�G��OBB��Transform")]
        [SerializeField] Transform _headOBBTransform;

        // �G�֘A�N���X
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // �R���|�[�l���g
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        // OBB
        public OBB EnemyOBB { get; private set; }
        public OBB EnemyHeadOBB { get; private set; }
        // ��U��OBB
        public List<OBB> DamageableOBBs { get; private set; } = new();

        // �U���֘A
        float _attackInterval;
        float _attackTimer; // �U���p�^�C�}�[
        bool _canAttack;    // �U����ԃt���O
        public int HitNum { get; private set; } // �U���q�b�g��

        void Awake()
        {
            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(_animator);

            DamageableOBBs.Add(EnemyOBB = 
                new OBB(_enemyOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemySize));
            DamageableOBBs.Add(EnemyHeadOBB = 
                new OBB(_headOBBTransform, EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).EnemyHeadSize));


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
        /// OBB���̍X�V
        /// </summary>
        void UpdateOBBInfo()
        {
            EnemyOBB.UpdateInfo(_enemyOBBTransform);
            EnemyHeadOBB.UpdateInfo(_headOBBTransform);
        }

        /// <summary>
        /// �v���C���[�Ƃ̋�����Ԃ�
        /// </summary>
        public float CheckDistanceToPlayer()
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
