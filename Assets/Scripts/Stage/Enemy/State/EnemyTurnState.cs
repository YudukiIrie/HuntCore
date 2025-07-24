using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵方向転換状態
    /// </summary>
    public class EnemyTurnState : IState
    {
        Enemy _enemy;           // 敵クラス
        Quaternion _targetRot;  // 向くべき角度   

        // データキャッシュ用
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

            // 遷移した時点でのターゲット角度を取得
            _targetRot = Quaternion.LookRotation(_enemy.GetDirectionToPlayer());
        }

        public void Update()
        {
            Rotate();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }

        /// <summary>
        /// 回転
        /// </summary>
        void Rotate()
        {
            // ターゲット角度の取得
            _targetRot = Quaternion.LookRotation(_enemy.GetDirectionToPlayer());
            // 回転速度の取得
            float rotSpeed = _turnSpeed * Time.deltaTime;
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
            if (_enemy.GetAngleToPlayer() <= _attackAngle)
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }
    }
}