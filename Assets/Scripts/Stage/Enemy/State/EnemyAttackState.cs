using Stage.HitCheck;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵攻撃ステート
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // 敵クラス
        
        // データキャッシュ用
        float _hitStartRatio;
        float _hitEndRatio;
        
        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;
            _hitStartRatio = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackHitStartRatio;
            _hitEndRatio = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackHitEndRatio;
        }

        public void Enter()
        {
            _enemy.Animation.Attack();
        }

        public void Update()
        {
            // === 当たり判定 ===
            if (_enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack) >= _hitStartRatio &&
                _enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack) <= _hitEndRatio)
            {
                if (OBBHitChecker.IsColliding(_enemy.EnemyHeadSphere, _enemy.Player.PlayerColliders))
                {
                    // 接触OBBの状態による分岐
                    // ガード
                    if (_enemy.EnemyHeadSphere.HitInfo.targetRole == HitCollider.ColliderRole.Guard)
                        _enemy.Player.TakeImpact();
                    // パリィ
                    else if (_enemy.EnemyHeadSphere.HitInfo.targetRole == HitCollider.ColliderRole.Parry)
                    {

                    }
                    else
                        _enemy.IncreaseHitNum();
                }
            }

            // === 状態遷移 ===
            // 警戒
            if (_enemy.Animation.IsAnimFinished(EnemyAnimation.HashAttack))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_enemy.EnemyHeadSphere, _enemy.Player.PlayerColliders);
        }
    }
}