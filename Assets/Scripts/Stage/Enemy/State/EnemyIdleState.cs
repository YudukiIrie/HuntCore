using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemy
{
    /// <summary>
    /// “G‘Ò‹@ó‘Ô
    /// </summary>
    public class EnemyIdleState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX

        public EnemyIdleState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.ResetAll();
        }

        public void Update()
        {
            // ‘Ò‹@‚©‚ç‚¤‚ë‚Â‚«‚Ö‚Ì‘JˆÚŠm—§‚ğæ“¾
            var percent = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).IdleToWanderPercent;

            // === ó‘Ô‘JˆÚ ===
            if (_enemy.IsTransitionHit(percent))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.WanderState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
