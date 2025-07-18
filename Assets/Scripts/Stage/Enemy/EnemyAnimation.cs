using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�̃A�j���[�V�������Ǘ�
    /// </summary>
    public class EnemyAnimation : Animation
    {
        // Animator�̃p�����[�^�n�b�V��ID
        public static readonly int HashIdle = Animator.StringToHash("Base Layer.Idle");
        public static readonly int HashRoar = Animator.StringToHash("Base Layer.Roar");
        public static readonly int HashAlert = Animator.StringToHash("Base Layer.Alert");
        public static readonly int HashChase = Animator.StringToHash("Base Layer.Chase");
        public static readonly int HashAttack = Animator.StringToHash("Base Layer.Attack");
        public static readonly int HashWalk = Animator.StringToHash("Base Layer.Walk");

        float _animBlendTime;

        public EnemyAnimation(Animator animator) : base(animator)
        {
            _animBlendTime = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AnimBlendTime;
        }

        /// <summary>
        /// �ҋ@�A�j���[�V�����J�n
        /// </summary>
        public void Idle()
        {
            _animator.CrossFade(HashIdle, _animBlendTime);
        }

        /// <summary>
        /// ���K�A�j���[�V�����J�n
        /// </summary>
        public void Roar()
        {
            _animator.CrossFade(HashRoar, _animBlendTime);
        }

        /// <summary>
        /// �x���A�j���[�V�����J�n
        /// </summary>
        public void Alert()
        {
            _animator.CrossFade(HashAlert, _animBlendTime);
        }

        /// <summary>
        /// �ǐՃA�j���[�V�����J�n
        /// </summary>
        public void Chase()
        {
            _animator.CrossFade(HashChase, _animBlendTime);
        }

        /// <summary>
        /// �U���A�j���[�V�����J�n
        /// </summary>
        public void Attack()
        {
            _animator.CrossFade(HashAttack, _animBlendTime);
        }

        /// <summary>
        /// �����A�j���[�V�����J�n
        /// </summary>
        public void Walk()
        {
            _animator.CrossFade(HashWalk, _animBlendTime);
        }
    }
}
