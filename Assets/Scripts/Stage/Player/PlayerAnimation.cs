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
        static readonly int _hashMove = Animator.StringToHash("Move");
        static readonly int _hashAttack1 = Animator.StringToHash("Attack1");
        static readonly int _hashAttack2 = Animator.StringToHash("Attack2");
        static readonly int _hashAttack3 = Animator.StringToHash("Attack3");

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
            _animator.SetBool(_hashMove, false);
            _animator.SetBool(_hashAttack1, false);
            _animator.SetBool(_hashAttack2, false);
            _animator.SetBool(_hashAttack3, false);
        }

        /// <summary>
        /// �ړ��A�j���[�V�����J�n
        /// </summary>
        public void Move()
        {
            ResetAll();
            _animator.SetBool(_hashMove, true);
        }

        /// <summary>
        /// �U���A�j���[�V�����J�n
        /// </summary>
        public void Attack1()
        {
            ResetAll();
            _animator.SetBool(_hashAttack1, true);
        }

        /// <summary>
        /// �U��2�A�j���[�V�����J�n
        /// </summary>
        public void Attack2()
        {
            ResetAll();
            _animator.SetBool(_hashAttack2, true);
        }

        /// <summary>
        /// �U��3�A�j���[�V�����J�n
        /// </summary>
        public void Attack3()
        {
            ResetAll();
            _animator.SetBool(_hashAttack3, true);
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
        /// �U��1�X�e�[�g�A�j���[�V�����I���`�F�b�N
        /// </summary>
        /// <returns>true:�Đ��I��, false:�Đ���</returns>
        public bool IsAttack1StateFinished()
        {
            if (CheckCurrentState(_hashAttack1))
            {
                // �A�j���[�V�����I���҂�
                float time = _currentStateInfo.normalizedTime;
                if (time >= 1.0f)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// �U��2�X�e�[�g�A�j���[�V�����I���`�F�b�N
        /// </summary>
        /// <returns>true:�Đ��I��, false:�Đ���</returns>
        public bool IsAttack2StateFinished()
        {
            if (CheckCurrentState(_hashAttack2))
            {
                float time = _currentStateInfo.normalizedTime;
                if (time >= 1.0f)
                    return true;
            }
            return false;
        }
    }
}
