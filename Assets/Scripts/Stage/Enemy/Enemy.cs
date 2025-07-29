using Stage.enemies;
using Stage.HitDetection;
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

        [field: Header("�R���C�_�[�Ǘ��N���X")]
        [field: SerializeField] public EnemyCollider Collider { get; private set; }

        // �G�֘A�N���X
        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyAnimation Animation { get; private set; }

        // �R���|�[�l���g
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [SerializeField] Animator _animator;

        // �U���֘A
        float _attackInterval;
        float _attackTimer; // �U���p�^�C�}�[
        bool _canAttack;    // �U����ԃt���O
        public int HitNum { get; private set; } // �U���q�b�g��

        void Awake()
        {
            StateMachine = new EnemyStateMachine(this);
            Animation = new EnemyAnimation(_animator);

            _attackInterval = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackInterval;
        }

        void Start()
        {
            StateMachine.Initialize(EnemyState.Idle);    
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

        /// <summary>
        /// �Ռ����󂯂�
        /// </summary>
        /// <param name="nextState">�J�ڌ�X�e�[�g</param>
        public void TakeImpact(EnemyState nextState)
        {
            StateMachine.TransitionTo(nextState);
        }

        public void IncreaseHitNum()
        {
            HitNum++;
        }
    }
}
