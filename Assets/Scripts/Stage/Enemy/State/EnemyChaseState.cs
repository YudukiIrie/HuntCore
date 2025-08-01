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

        // データキャッシュ用
        float _chaseSpeed;
        float _chaseRotSpeed;
        float _stopDist;

        public EnemyChaseState(Enemy enemy)
        {
            _enemy = enemy;

            _chaseSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseSpeed;
            _chaseRotSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseRotSpeed;
            _stopDist = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).StopDist;
        }

        public void Enter()
        {
            _enemy.Animation.Chase();
            _targetRot = _enemy.transform.rotation;
        }

        public void Update()
        {
            MoveUpdate();

            Rotate();

            Transition();
        }

        public void FixedUpdate()
        {
            MoveFixedUpdate();
        }

        public void Exit()
        {
            // 遷移後に動いてほしくないため、加えた力をリセット
            _enemy.Rigidbody.velocity = Vector3.zero;
        }

        /// <summary>
        /// Update()用移動処理
        /// </summary>
        void MoveUpdate()
        {
            _velocity = _enemy.Player.transform.position - _enemy.transform.position;
            _velocity = Vector3.ProjectOnPlane(_velocity, Vector3.up);
            _velocity = _velocity.normalized * _chaseSpeed;
        }

        /// <summary>
        /// 回転
        /// </summary>
        void Rotate()
        {
            // ベクトルの大きさが0に等しい場合は除外
            if (_velocity.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity);
            // 回転速度の取得
            float rotSpeed = _chaseRotSpeed * Time.deltaTime;
            // 回転
            Quaternion rot = _enemy.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _enemy.transform.rotation = rot;
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // 警戒
            if (_enemy.GetDistanceToPlayer() <= _stopDist)
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }

        /// <summary>
        /// FixedUpdate()用移動処理
        /// </summary>
        void MoveFixedUpdate()
        {
            var vel = _velocity;
            vel.y = _enemy.Rigidbody.velocity.y;
            _enemy.Rigidbody.velocity = vel;
        }
    }
}