using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵追跡状態
    /// </summary>
    public class EnemyChaseState : IState
    {
        Enemy _enemy;           // 敵クラス
        Vector3 _velocity;      // 移動方向と速度
        Quaternion _targetRot;  // 向くべき角度
        float _chaseSpeed;
        float _chaseRotSpeed;
        float _attackDistance;

        public EnemyChaseState(Enemy enemy)
        {
            _enemy = enemy;
            _chaseSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseSpeed;
            _chaseRotSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseRotSpeed;
            _attackDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackDistance;
        }

        public void Enter()
        {
            _enemy.Animation.Chase();
            _targetRot = _enemy.transform.rotation;
        }

        public void Update()
        {
            // === 移動計算 ===
            _velocity = _enemy.Player.transform.position - _enemy.transform.position;
            _velocity = Vector3.ProjectOnPlane(_velocity, Vector3.up);
            _velocity = _velocity.normalized * _chaseSpeed;

            // === 回転計算 ===
            // ベクトルの大きさが0に等しい場合は除外
            if (_velocity.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity);

            // === 状態遷移 ===
            // 警戒
            if (_enemy.CheckDistanceToPlayer() <= _attackDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {
            // === 移動 ===
            var vel = _velocity;
            vel.y = _enemy.Rigidbody.velocity.y;
            _enemy.Rigidbody.velocity = vel;

            // === 回転 ===
            _enemy.transform.rotation = Quaternion.RotateTowards(_enemy.transform.rotation, _targetRot, _chaseRotSpeed);
        }

        public void Exit()
        {

        }
    }
}