using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “G•ûŒü“]Š·ó‘Ô
    /// </summary>
    public class EnemyTurnState : IState
    {
        Enemy _enemy;           // “GƒNƒ‰ƒX
        Quaternion _targetRot;  // Œü‚­‚×‚«Šp“x   
        float _turnSpeed;
        float _attackAngle;

        public EnemyTurnState(Enemy enemy)
        {
            _enemy = enemy;
            _turnSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).TurnSpeed;
            _attackAngle = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackAngle;
        }

        public void Enter()
        {
            _enemy.Animation.Walk();

            // ‘JˆÚ‚µ‚½“_‚Å‚Ìƒ^[ƒQƒbƒgŠp“x‚ğæ“¾
            _targetRot = Quaternion.LookRotation(_enemy.GetDirectionToPlayer());
        }

        public void Update()
        {
            // === •ûŒü“]Š· ===
            // ƒ^[ƒQƒbƒgŠp“x‚Ìæ“¾
            _targetRot = Quaternion.LookRotation(_enemy.GetDirectionToPlayer());
            // ‰ñ“]‘¬“x‚Ìæ“¾
            float rotSpeed = _turnSpeed * Time.deltaTime;
            // ‰ñ“]
            Quaternion rot = _enemy.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _enemy.transform.rotation = rot;

            // === ó‘Ô‘JˆÚ ===
            // Œx‰ú
            if (_enemy.GetAngleToPlayer() <= _attackAngle)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}