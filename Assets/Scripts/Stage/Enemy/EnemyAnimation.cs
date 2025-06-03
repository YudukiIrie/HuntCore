using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�̃A�j���[�V�������Ǘ�
    /// </summary>
    public class EnemyAnimation
    {
        // Animator�̃p�����[�^�n�b�V��ID
        public static readonly int HashRoar = Animator.StringToHash("Roar");
        public static readonly int HashAlert = Animator.StringToHash("Alert");

        // �R���|�[�l���g
        Animator _animator;

        // �A�j���[�V�����X�e�[�g���ۑ��p
        AnimatorStateInfo _currentStateInfo;

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// �A�j���[�V�������Z�b�g
        /// </summary>
        public void ResetAll()
        {
            _animator.SetBool(HashRoar, false);
            _animator.SetBool(HashAlert, false);
        }

        /// <summary>
        /// ���K�A�j���[�V�����J�n
        /// </summary>
        public void Roar()
        {
            ResetAll();
            _animator.SetBool(HashRoar, true);
        }

        /// <summary>
        /// �x���A�j���[�V�����J�n
        /// </summary>
        public void Alert()
        {
            ResetAll();
            _animator.SetBool(HashAlert, true);
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
    }
}
