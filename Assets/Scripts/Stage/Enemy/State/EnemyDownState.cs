using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵ダウン状態
    /// </summary>
    public class EnemyDownState : IState
    {
        Enemy _enemy;
        bool _isDowned = false;     // ダウン状態
        float _downTimer = 0.0f;    // ダウン時間計測用

        // データキャッシュ用
        float _downDuration;

        public EnemyDownState(Enemy enemy)
        {
            _enemy = enemy;

            _downDuration = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).DownDuration;
        }

        public void Enter()
        {
            _isDowned = true;
            _enemy.Animation.Down();
        }

        public void Update()
        {
            _downTimer += Time.deltaTime;

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _downTimer = 0.0f;
            _isDowned = false;
        }

        void Transition()
        {
            // 起き上がり
            if (_downTimer >= _downDuration && _isDowned)
            {
                _enemy.Animation.GetUp();
                _isDowned = false;
            }
            // 警戒
            if (!_isDowned)
            {
                // 逆再生の終了 = 0.0f
                if (_enemy.Animation.CheckAnimRatio(EnemyAnimation.HashDown) <= 0.0f)
                    _enemy.StateMachine.TransitionTo(EnemyState.Alert);
            }
        }
    }
}