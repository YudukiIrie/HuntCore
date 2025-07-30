using Stage.HitDetection;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵爪攻撃状態
    /// </summary>
    public class EnemyClawAttackState : IState
    {
        Enemy _enemy;

        // データキャッシュ用
        Vector2 _hitWindow;

        public EnemyClawAttackState(Enemy enemy)
        {
            _enemy = enemy;

            _hitWindow = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ClawHitWindow;
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
        /// 当たり判定
        /// </summary>
        void DetectHit()
        {
            float start = _hitWindow.x;
            float end   = _hitWindow.y;
            float progress = _enemy.Animation.CheckRatio(EnemyAnimation.HashClawAttack);
            if (start <= progress && progress <= end)
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
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // 警戒
            if (_enemy.Animation.CheckEnd(EnemyAnimation.HashClawAttack))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }
    }
}