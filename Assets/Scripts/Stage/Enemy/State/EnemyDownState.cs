using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�_�E�����
    /// </summary>
    public class EnemyDownState : IState
    {
        Enemy _enemy;
        bool _isDowned = false;     // �_�E�����
        float _downTimer = 0.0f;    // �_�E�����Ԍv���p

        // �f�[�^�L���b�V���p
        float _downDuration;

        public EnemyDownState(Enemy enemy)
        {
            _enemy = enemy;

            _downDuration = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).DownDuration;
        }

        public void Enter()
        {
            _isDowned = true;
            _enemy.Animation.Down();
        }

        public void Update()
        {
            _downTimer += Time.deltaTime;

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _downTimer = 0.0f;
            _isDowned = false;
        }

        void Transition()
        {
            // �N���オ��
            if (_downTimer >= _downDuration && _isDowned)
            {
                _enemy.Animation.GetUp();
                _isDowned = false;
            }
            // �x��
            if (!_isDowned)
            {
                // �t�Đ��̏I�� = 0.0f
                if (_enemy.Animation.CheckAnimRatio(EnemyAnimation.HashDown) <= 0.0f)
                    _enemy.StateMachine.TransitionTo(EnemyState.Alert);
            }
        }
    }
}