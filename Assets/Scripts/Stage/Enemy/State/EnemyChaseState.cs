using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “G’ÇÕó‘Ô
    /// </summary>
    public class EnemyChaseState : IState
    {
        Enemy _enemy;           // “GƒNƒ‰ƒX
        Vector3 _velocity;      // ˆÚ“®•ûŒü‚Æ‘¬“x
        Quaternion _targetRot;  // Œü‚­‚×‚«Šp“x
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
            // === ˆÚ“®ŒvZ ===
            _velocity = _enemy.Player.transform.position - _enemy.transform.position;
            _velocity = Vector3.ProjectOnPlane(_velocity, Vector3.up);
            _velocity = _velocity.normalized * _chaseSpeed;

            // === ‰ñ“]ŒvZ ===
            // ƒxƒNƒgƒ‹‚Ì‘å‚«‚³‚ª0‚É“™‚µ‚¢ê‡‚ÍœŠO
            if (_velocity.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity);

            // === •ûŒü“]Š· ===
            // ‰ñ“]‘¬“x‚Ìæ“¾
            float rotSpeed = _chaseRotSpeed * Time.deltaTime;
            // ‰ñ“]
            Quaternion rot = _enemy.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _enemy.transform.rotation = rot;

            // === ó‘Ô‘JˆÚ ===
            // Œx‰ú
            if (_enemy.GetDistanceToPlayer() <= _stopDistance)
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }

        public void FixedUpdate()
        {
            // === ˆÚ“® ===
            var vel = _velocity;
            vel.y = _enemy.Rigidbody.velocity.y;
            _enemy.Rigidbody.velocity = vel;
        }

        public void Exit()
        {

        }
    }
}