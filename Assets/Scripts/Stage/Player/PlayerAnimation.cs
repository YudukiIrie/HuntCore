using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// �v���C���[�̃A�j���[�V�������Ǘ�
    /// </summary>
    public class PlayerAnimation
    {
        // Animator�̃p�����[�^�n�b�V��ID
        // �萔�����s���ɒl�����܂邽��static readonly
        public static readonly int HashMove = Animator.StringToHash("Move");
        public static readonly int HashAttack1 = Animator.StringToHash("Attack1");
        public static readonly int HashAttack2 = Animator.StringToHash("Attack2");
        public static readonly int HashAttack3 = Animator.StringToHash("Attack3");

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
            _animator.SetBool(HashAttack1, false);
            _animator.SetBool(HashAttack2, false);
            _animator.SetBool(HashAttack3, false);
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
        /// �U���A�j���[�V�����J�n
        /// </summary>
        public void Attack1()
        {
            ResetAll();
            _animator.SetBool(HashAttack1, true);
        }

        /// <summary>
        /// �U��2�A�j���[�V�����J�n
        /// </summary>
        public void Attack2()
        {
            ResetAll();
            _animator.SetBool(HashAttack2, true);
        }

        /// <summary>
        /// �U��3�A�j���[�V�����J�n
        /// </summary>
        public void Attack3()
        {
            ResetAll();
            _animator.SetBool(HashAttack3, true);
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
