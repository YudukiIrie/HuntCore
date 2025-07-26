using UnityEngine;

/// <summary>
/// �e�A�j���[�V�����e�N���X
/// </summary>
public class Animation
{
    // �R���|�[�l���g
    protected Animator _animator;

    // �A�j���[�V�����X�e�[�g���ۑ��p
    AnimatorStateInfo _currentStateInfo;

    public Animation(Animator animator)
    {
        _animator = animator;
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
    public bool CheckEnd(int stateHash)
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
    public float CheckRatio(int stateHash)
    {
        if (CheckCurrentState(stateHash))
            return _currentStateInfo.normalizedTime;

        return 0.0f;
    }
    
    /// <summary>
    /// �Đ����̃A�j���[�V�����Đ�������
    /// �w�肵�������Ƃ̔�r
    /// </summary>
    /// <param name="ratio">�w�芄��</param>
    /// <returns>true:�w�芄���ȏ�, false:�w�芄������</returns>
    public bool CompareRatio(int stateHash, float ratio)
    {
        if (CheckCurrentState(stateHash))
            return _currentStateInfo.normalizedTime >= ratio;

        return false;
    }
}
