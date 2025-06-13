using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�U���X�e�[�g
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // �G�N���X
        
        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.Attack();
        }

        public void Update()
        {
            // === �����蔻�� ===
            //if ()
            //if (_enemy.HitChecker.IsCollideBoxOBB(_enemy.HitChecker.EnemyHeadOBB, _enemy.HitChecker.PlayerOBB))

            // === ��ԑJ�� ===
            // �x��
            if (_enemy.Animation.IsAnimFinished(EnemyAnimation.HashAttack))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}