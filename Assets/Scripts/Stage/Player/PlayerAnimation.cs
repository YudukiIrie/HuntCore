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
        public static readonly int HashMove = Animator.StringToHash("Move");
        public static readonly int HashLightAttack = Animator.StringToHash("LightAttack");
        public static readonly int HashHeavyAttack = Animator.StringToHash("HeavyAttack");
        public static readonly int HashSpecialAttack = Animator.StringToHash("SpecialAttack");
        public static readonly int HashImpacted = Animator.StringToHash("Impacted");

        // �R���|�[�l���g
        Animator _animator;

        // �A�j���[�V�����X�e�[�g���ۑ��p
        AnimatorStateInfo _currentStateInfo;

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// �A�j���[�V�������Z�b�g
        /// </summary>
        public void ResetAll()
        {
            _animator.SetBool(HashMove, false);
            _animator.SetBool(HashLightAttack, false);
            _animator.SetBool(HashHeavyAttack, false);
            _animator.SetBool(HashSpecialAttack, false);
            _animator.SetBool(HashImpacted, false);
        }

        /// <summary>
        /// �ړ��A�j���[�V�����J�n
        /// </summary>
        public void Move()
        {
            ResetAll();
            _animator.SetBool(HashMove, true);
        }

        /// <summary>
        /// ���C�g�U���A�j���[�V�����J�n
        /// </summary>
        public void LightAttack()
        {
            ResetAll();
            _animator.SetBool(HashLightAttack, true);
        }

        /// <summary>
        /// �w�r�[�U���A�j���[�V�����J�n
        /// </summary>
        public void HeavyAttack()
        {
            ResetAll();
            _animator.SetBool(HashHeavyAttack, true);
        }

        /// <summary>
        /// �X�y�V�����U���A�j���[�V�����J�n
        /// </summary>
        public void SpecialAttack()
        {
            ResetAll();
            _animator.SetBool(HashSpecialAttack, true);
        }

        /// <summary>
        /// �Ռ��A�j���[�V�����J�n
        /// </summary>
        public void Impacted()
        {
            ResetAll();
            _animator.SetBool(HashImpacted, true);
        }

        /// <summary>
        /// �w�肵���A�j���[�V�����X�e�[�g���Đ������`�F�b�N
        /// </summary>
        bool CheckCurrentState(int currentStateHash)
        {
            // BaseLayer�̃X�e�[�g�����擾
            _currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            // �Đ����̃X�e�[�g���w�肵���X�e�[�g�Ɠ������`�F�b�N
            bool check = (_currentStateInfo.shortNameHash == currentStateHash);
            
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
