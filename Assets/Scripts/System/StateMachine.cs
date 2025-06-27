using System;
using System.Collections.Generic;

/// <summary>
/// �X�e�[�g�}�V�[���e�N���X
/// </summary>
/// <typeparam name="TState">�Ή�����X�e�[�g�񋓑�</typeparam>
public abstract class StateMachine<TState> where TState : Enum
{
    // ���݂̃X�e�[�g
    protected IState _currentState;

    // �X�e�[�g�w��p����
    protected readonly Dictionary<TState, IState> _states;

    public StateMachine(int capacity)
    {
        _states = new Dictionary<TState, IState>(capacity);
    }

    /// <summary>
    /// �X�e�[�g�̏�����
    /// </summary>
    /// <param name="key">�����X�e�[�gkey</param>
    public abstract void Initialize(TState key);

    /// <summary>
    /// �e�X�e�[�g��Update()�𓮂���
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// �e�X�e�[�g��FixedUpdate()�𓮂���
    /// </summary>
    public abstract void FixedUpdate();

    /// <summary>
    /// ��ԑJ��
    /// </summary>
    /// <param name="key">�؂�ւ���X�e�[�gkey</param>
    public abstract void TransitionTo(TState key);
}
