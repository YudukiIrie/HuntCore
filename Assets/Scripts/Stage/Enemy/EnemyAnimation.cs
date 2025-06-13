using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�̃A�j���[�V�������Ǘ�
    /// </summary>
    public class EnemyAnimation
    {
        // Animator�̃p�����[�^�n�b�V��ID
        public static readonly int HashIdle = Animator.StringToHash("Base Layer.Idle");
        public static readonly int HashRoar = Animator.StringToHash("Base Layer.Roar");
        public static readonly int HashAlert = Animator.StringToHash("Base Layer.Alert");
        public static readonly int HashChase = Animator.StringToHash("Base Layer.Chase");
        public static readonly int HashAttack = Animator.StringToHash("Base Layer.Attack");

        // �R���|�[�l���g
        Animator _animator;

        // �A�j���[�V�����X�e�[�g���ۑ��p
        AnimatorStateInfo _currentStateInfo;

        float _animBlendTime;

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
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
        /// �w�肵���A�j���[�V�����X�e�[�g���Đ������`�F�b�N
        /// </summary>
        bool CheckCurrentState(int currentStateHash)
        {
            // BaseLayer�̃X�e�[�g�����擾
            _currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            // �Đ����̃X�e�[�g���w�肵���X�e�[�g�Ɠ������`�F�b�N
            bool check = (_currentStateInfo.fullPathHash == currentStateHash);

            return check;
        }

        /// <summary>
        /// �w�肵���A�j���[�V�����̏I���`�F�b�N
        /// </summary>
        /// <returns>true:�Đ��I��, false:�Đ���</returns>
        public bool IsAnimFinished(int stateHash)
        {
            if (CheckCurrentState(stateHash))
            {
                // �A�j���[�V�����I���҂�
                float time = _currentStateInfo.normalizedTime;
                if (time >= 1.0f)
                    return true;
            }
            return false;
        }
    }
}
