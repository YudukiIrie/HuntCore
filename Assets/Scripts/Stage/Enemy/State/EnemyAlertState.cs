using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “GŒx‰úó‘Ô
    /// </summary>
    public class EnemyAlertState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX

        public EnemyAlertState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.Alert();
        }

        public void Update()
        {
            // === ‘JˆÚ ===
            
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}