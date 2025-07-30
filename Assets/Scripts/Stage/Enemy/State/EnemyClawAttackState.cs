using Stage.HitDetection;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�܍U�����
    /// </summary>
    public class EnemyClawAttackState : IState
    {
        Enemy _enemy;

        public EnemyClawAttackState(Enemy enemy)
        {
            _enemy = enemy;
        }
        
        public void Enter()
        {
            _enemy.Animation.ClawAttack();
        }

        public void Update()
        {
            DetectHit();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            HitChecker.ResetHitInfo(_enemy.Collider.RClaw, _enemy.Player.Collider.Colliders);
            HitChecker.ResetHitInfo(_enemy.Collider.LClaw, _enemy.Player.Collider.Colliders);
        }

        /// <summary>
        /// �����蔻��
        /// </summary>
        void DetectHit()
        {
            if (HitChecker.IsColliding(_enemy.Collider.RClaw, _enemy.Player.Collider.Colliders))
            {
                HitCollider other = _enemy.Collider.RClaw.HitInfo.other;
                _enemy.Player.HitReaction.ReactToHit(other);
            }
            else if (HitChecker.IsColliding(_enemy.Collider.LClaw, _enemy.Player.Collider.Colliders))
            {
                HitCollider other = _enemy.Collider.LClaw.HitInfo.other;
                _enemy.Player.HitReaction.ReactToHit(other);
            }
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �x��
            if (_enemy.Animation.CheckEnd(EnemyAnimation.HashClawAttack))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }
    }
}