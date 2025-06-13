using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̃A�j���[�V�������Ǘ�
    /// </summary>
    public class PlayerAnimation
    {
        // Animator�̃p�����[�^�n�b�V��ID
        // �萔�����s���ɒl�����܂邽��static readonly
        public static readonly int HashIdle = Animator.StringToHash("Base Layer.Idle");
        public static readonly int HashMove = Animator.StringToHash("Base Layer.Move");
        public static readonly int HashLightAttack = Animator.StringToHash("Base Layer.LightAttack");
        public static readonly int HashHeavyAttack = Animator.StringToHash("Base Layer.HeavyAttack");
        public static readonly int HashSpecialAttack = Animator.StringToHash("Base Layer.SpecialAttack");
        public static readonly int HashImpacted = Animator.StringToHash("Base Layer.Impacted");

        // �R���|�[�l���g
        Animator _animator;

        // �A�j���[�V�����X�e�[�g���ۑ��p
        AnimatorStateInfo _currentStateInfo;

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// �ҋ@�A�j���[�V�����J�n
        /// </summary>
        public void Idle()
        {
            _animator.CrossFade(HashIdle, 0.1f);
        }

        /// <summary>
        /// �ړ��A�j���[�V�����J�n
        /// </summary>
        public void Move()
        {
            _animator.CrossFade(HashMove, 0.1f);
        }

        /// <summary>
        /// ���C�g�U���A�j���[�V�����J�n
        /// </summary>
        public void LightAttack()
        {
            _animator.CrossFade(HashLightAttack, 0.1f);
        }

        /// <summary>
        /// �w�r�[�U���A�j���[�V�����J�n
        /// </summary>
        public void HeavyAttack()
        {
            _animator.CrossFade(HashHeavyAttack, 0.1f);
        }

        /// <summary>
        /// �X�y�V�����U���A�j���[�V�����J�n
        /// </summary>
        public void SpecialAttack()
        {
            _animator.CrossFade(HashSpecialAttack, 0.1f);
        }

        /// <summary>
        /// �Ռ��A�j���[�V�����J�n
        /// </summary>
        public void Impacted()
        {
            _animator.CrossFade(HashImpacted, 0.1f);
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

        /// <summary>
        /// �w�肵���A�j���[�V�����Đ����Ԃ�0�`1�̊����ɕϊ������l��ԋp
        /// </summary>
        public float CheckAnimRatio(int stateHash)
        {
            if (CheckCurrentState(stateHash))
                return _currentStateInfo.normalizedTime;

            return 0.0f;
        }
    }
}
