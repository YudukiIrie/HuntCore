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
        public static readonly int HashWalk = Animator.StringToHash("Base Layer.Walk");
        public static readonly int HashDown = Animator.StringToHash("Base Layer.Down");
        public static readonly int HashAlert = Animator.StringToHash("Base Layer.Alert");
        public static readonly int HashChase = Animator.StringToHash("Base Layer.Chase");
        public static readonly int HashAttack = Animator.StringToHash("Base Layer.Attack");
        public static readonly int HashGetHit = Animator.StringToHash("Base Layer.GetHit");
        public static readonly int HashClawAttack = Animator.StringToHash("Base Layer.ClawAttack");       
        static readonly int HashSpeed = Animator.StringToHash("Speed");

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
            ResetParam();
            _animator.CrossFade(HashIdle, _animBlendTime);
        }

        /// <summary>
        /// ���K�A�j���[�V�����J�n
        /// </summary>
        public void Roar()
        {
            ResetParam();
            _animator.CrossFade(HashRoar, _animBlendTime);
        }

        /// <summary>
        /// �x���A�j���[�V�����J�n
        /// </summary>
        public void Alert()
        {
            ResetParam();
            _animator.CrossFade(HashAlert, _animBlendTime);
        }

        /// <summary>
        /// �ǐՃA�j���[�V�����J�n
        /// </summary>
        public void Chase()
        {
            ResetParam();
            _animator.CrossFade(HashChase, _animBlendTime);
        }

        /// <summary>
        /// �U���A�j���[�V�����J�n
        /// </summary>
        public void Attack()
        {
            ResetParam();
            _animator.CrossFade(HashAttack, _animBlendTime);
        }

        /// <summary>
        /// �����A�j���[�V�����J�n
        /// </summary>
        public void Walk()
        {
            ResetParam();
            _animator.CrossFade(HashWalk, _animBlendTime);
        }

        /// <summary>
        /// ��U���A�j���[�V�����J�n
        /// </summary>
        public void GetHit()
        {
            ResetParam();
            _animator.CrossFade(HashGetHit, _animBlendTime);
        }

        /// <summary>
        /// �_�E���A�j���[�V�����J�n
        /// </summary>
        public void Down()
        {
            ResetParam();
            _animator.CrossFade(HashDown, _animBlendTime);
        }

        /// <summary>
        /// �N���オ��A�j���[�V�����J�n
        /// </summary>
        /// <returns>true:�Đ��I��, false;�Đ���</returns>
        public void GetUp()
        {
            ResetParam();
            // �_�E���A�j���[�V�������t�Đ�
            _animator.SetFloat(HashSpeed, -1);
            _animator.CrossFade(HashDown, _animBlendTime, 0, 1);
        }

        /// <summary>
        /// �܍U���A�j���[�V�����J�n
        /// </summary>
        public void ClawAttack()
        {
            ResetParam();
            _animator.CrossFade(HashClawAttack, _animBlendTime);
        }

        /// <summary>
        /// �p�����[�^�̃��Z�b�g
        /// </summary>
        void ResetParam()
        {
            _animator.SetFloat(HashSpeed, 1);
        }
    }
}
