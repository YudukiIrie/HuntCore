using Stage.HitCheck;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵攻撃ステート
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // 敵クラス

        // データキャッシュ用
        Vector2 _hitWindow;
        
        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;

            _hitWindow = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackHitWindow;
        }

        public void Enter()
        {
            _enemy.Animation.Attack();
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
            OBBHitChecker.ResetHitInfo(_enemy.EnemyHeadSphere, _enemy.Player.PlayerColliders);
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        void DetectHit()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _enemy.Animation.CheckRatio(EnemyAnimation.HashAttack);
            if (progress >= start && progress <= end)
            {
                if (OBBHitChecker.IsColliding(_enemy.EnemyHeadSphere, _enemy.Player.PlayerColliders))
                {
                    HitCollider other = _enemy.EnemyHeadSphere.HitInfo.other;
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
            if (_enemy.Animation.CheckEnd(EnemyAnimation.HashAttack))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }
    }
}