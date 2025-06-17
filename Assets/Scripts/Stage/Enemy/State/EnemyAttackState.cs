using Stage.HitCheck;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “GUŒ‚ƒXƒe[ƒg
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX
        float _hitStartRatio;
        
        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;
            _hitStartRatio = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackHitStartRatio;
        }

        public void Enter()
        {
            _enemy.Animation.Attack();
        }

        public void Update()
        {
            // === “–‚½‚è”»’è ===
            if (_enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack) >= _hitStartRatio)
            {
                if (OBBHitChecker.IsCollideBoxOBB(_enemy.EnemyHeadOBB, _enemy.Player.DamageableOBBs))
                    _enemy.IncreaseHitNum();
            }

            // === ó‘Ô‘JˆÚ ===
            // Œx‰ú
            if (_enemy.Animation.IsAnimFinished(EnemyAnimation.HashAttack))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_enemy.EnemyHeadOBB, _enemy.Player.DamageableOBBs);
        }
    }
}