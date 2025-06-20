using Stage.HitCheck;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵攻撃ステート
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // 敵クラス
        float _hitStartRatio;
        
        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;
            _hitStartRatio = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackHitStartRatio;
        }

        public void Enter()
        {
            _enemy.Animation.Attack();
        }

        public void Update()
        {
            // === 当たり判定 ===
            if (_enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack) >= _hitStartRatio)
            {
                if (OBBHitChecker.IsCollideBoxOBB(_enemy.EnemyHeadOBB, _enemy.Player.PlayerOBBs))
                {
                    // 接触OBBがガード(プレイヤーがガード中)の場合かそうでないかの分岐
                    if (_enemy.EnemyHeadOBB.HitInfo.targetType == OBB.OBBType.Guard)
                        _enemy.Player.TakeImpact();
                    else
                        _enemy.IncreaseHitNum();
                }
            }

            // === 状態遷移 ===
            // 警戒
            if (_enemy.Animation.IsAnimFinished(EnemyAnimation.HashAttack))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_enemy.EnemyHeadOBB, _enemy.Player.PlayerOBBs);
        }
    }
}