using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “G™ôšKó‘Ô
    /// </summary>
    public class EnemyRoarState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX
        float _roarDistance;

        public EnemyRoarState(Enemy enemy)
        {
            _enemy = enemy;
            _roarDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).RoarDistance;
        }

        public void Enter()
        {
            _enemy.Animation.Roar();

            // ƒvƒŒƒCƒ„[‚ÉÕŒ‚‚ğ—^‚¦‚é
            if (_enemy.GetDistanceToPlayer() <= _roarDistance)
                _enemy.Player.TakeImpact();
        }

        public void Update()
        {
            // === ó‘Ô‘JˆÚ ===
            // Œx‰ú
            if (_enemy.Animation.CheckEnd(EnemyAnimation.HashRoar))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
