using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemy
{
    /// <summary>
    /// �G�̃A�j���[�V�������Ǘ�
    /// </summary>
    public class EnemyAnimation
    {
        // Animator�̃p�����[�^�n�b�V��ID
        public static readonly int HashWander = Animator.StringToHash("Wander");

        // �R���|�[�l���g
        Animator _animator;

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// �A�j���[�V�������Z�b�g
        /// </summary>
        public void ResetAll()
        {
            _animator.SetBool(HashWander, false);
        }

        /// <summary>
        /// ������A�j���[�V�����J�n
        /// </summary>
        public void Wander()
        {
            ResetAll();
            _animator.SetBool(HashWander, true);
        }
    }
}
