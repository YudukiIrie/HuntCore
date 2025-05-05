using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// �e�v���C���[�X�e�[�g�̊�b
    /// </summary>
    public interface IPlayerState
    {
        void Enter()
        {
            // �e�X�e�[�g�ɓ������ۂ̏���
        }

        void Update()
        {
            // �}�C�t���[���s������
        }

        void FixedUpdate()
        {
            // �ړ��Ȃǂ̏d��������
        }

        void Exit()
        {
            // �e�X�e�[�g�𔲂���ۂ̏���
        }
    }
}
