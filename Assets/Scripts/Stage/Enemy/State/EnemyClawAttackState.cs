using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “G’ÜUŒ‚ó‘Ô
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
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }

        /// <summary>
        /// “–‚½‚è”»’è
        /// </summary>
        void DetectHit()
        {

        }
    }
}