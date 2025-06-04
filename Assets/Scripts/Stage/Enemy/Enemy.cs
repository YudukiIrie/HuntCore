using Stage.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�̋@�\�𑍊�
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public Player Player => _player;
        [Header("�v���C���[")]
        [SerializeField] Player _player;

        // �G�֘A�N���X
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // �R���|�[�l���g
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }

        float _attackInterval;
        float _attackTimer; // �U���p�^�C�}�[
        bool _canAttack;    // �U����ԃt���O

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();

            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(Animator);

            _attackInterval = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackInterval;
        }

        void Start()
        {
            StateMachine.Initialize(StateMachine.IdleState);    
        }

        void Update()
        {
            UpdateAttackTimer();

            StateMachine.Update();
        }

        void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        /// <summary>
        /// �v���C���[�Ƃ̋�����Ԃ�
        /// </summary>
        public float CheckDistanceToPlayer()
        {
            // ���W�̎擾
            var a = transform.position;
            var b = _player.transform.position;

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
    }
}
