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
        float _stopDistance;

        public EnemyChaseState(Enemy enemy)
        {
            _enemy = enemy;
            _chaseSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseSpeed;
            _chaseRotSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseRotSpeed;
            _stopDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).StopDistance;
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

            // === 方向転換 ===
            // 回転速度の取得
            float rotSpeed = _chaseRotSpeed * Time.deltaTime;
            // 回転
            Quaternion rot = _enemy.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _enemy.transform.rotation = rot;

            // === 状態遷移 ===
            // 警戒
            if (_enemy.GetDistanceToPlayer() <= _stopDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {
            // === 移動 ===
            var vel = _velocity;
            vel.y = _enemy.Rigidbody.velocity.y;
            _enemy.Rigidbody.velocity = vel;
        }

        public void Exit()
        {

        }
    }
}