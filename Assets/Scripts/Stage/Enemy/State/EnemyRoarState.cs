using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “G™ôšKó‘Ô
    /// </summary>
    public class EnemyRoarState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX

        public EnemyRoarState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.ResetAll();
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
