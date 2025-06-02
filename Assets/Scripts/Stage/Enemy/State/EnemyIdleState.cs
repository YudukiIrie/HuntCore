using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�ҋ@���
    /// </summary>
    public class EnemyIdleState : IState
    {
        Enemy _enemy;   // �G�N���X

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
            // === ��ԑJ�� ===
            // ���K

        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
