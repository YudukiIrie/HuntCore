/// <summary>
/// �e�X�e�[�g�̊�b
/// </summary>
public interface IState
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
