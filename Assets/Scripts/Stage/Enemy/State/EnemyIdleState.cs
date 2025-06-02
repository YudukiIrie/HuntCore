using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
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
            // === ó‘Ô‘JˆÚ ===
            // ™ôšK

        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
