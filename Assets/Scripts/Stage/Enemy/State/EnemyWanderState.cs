using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵うろつき状態
    /// </summary>
    public class EnemyWanderState : IState
    {
        Enemy _enemy;   // 敵クラス

        public EnemyWanderState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.Wander();

            // === AI設定 ===
            // 移動速度と回転速度
            _enemy.Agent.speed = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderSpeed;
            _enemy.Agent.angularSpeed = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderRotSpeed;
            // 目標にたどり着いたとする目標までの距離
            _enemy.Agent.stoppingDistance = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderStoppingDistance;
        }

        public void Update()
        {
            // 目的地がないとき
            if (!_enemy.Agent.hasPath)
            {
                // 指定範囲内で目的地を設定
                var range = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderRange;
                var posX = _enemy.transform.position.x + Random.Range(-range, range);
                var posZ = _enemy.transform.position.z + Random.Range(-range, range);
                Vector3 nextPos = new(posX, _enemy.transform.position.y, posZ);
                _enemy.Agent.SetDestination(nextPos);
            }

            // うろつきから待機への遷移確立を取得
            var percent = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderToIdlePercent;

            // === 状態遷移 ===
            // 待機
            if (_enemy.IsTransitionHit(percent))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            // 目的のリセット
            _enemy.Agent.ResetPath();
        }
    }
}
