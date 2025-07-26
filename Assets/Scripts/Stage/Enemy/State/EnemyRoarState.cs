using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G���K���
    /// </summary>
    public class EnemyRoarState : IState
    {
        Enemy _enemy;   // �G�N���X
        float _roarDistance;

        public EnemyRoarState(Enemy enemy)
        {
            _enemy = enemy;
            _roarDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).RoarDistance;
        }

        public void Enter()
        {
            _enemy.Animation.Roar();

            // �v���C���[�ɏՌ���^����
            if (_enemy.GetDistanceToPlayer() <= _roarDistance)
                _enemy.Player.TakeImpact();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �x��
            if (_enemy.Animation.CheckEnd(EnemyAnimation.HashRoar))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
