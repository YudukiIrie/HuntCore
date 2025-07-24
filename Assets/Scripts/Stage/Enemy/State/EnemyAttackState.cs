using Stage.HitCheck;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�U���X�e�[�g
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // �G�N���X

        // �f�[�^�L���b�V���p
        Vector2 _hitWindow;
        
        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;

            _hitWindow = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackHitWindow;
        }

        public void Enter()
        {
            _enemy.Animation.Attack();
        }

        public void Update()
        {
            DetectHit();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_enemy.EnemyHeadSphere, _enemy.Player.PlayerColliders);
        }

        /// <summary>
        /// �����蔻��
        /// </summary>
        void DetectHit()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack);
            if (progress >= start && progress <= end)
            {
                if (OBBHitChecker.IsColliding(_enemy.EnemyHeadSphere, _enemy.Player.PlayerColliders))
                {
                    // �ڐGOBB�̏�Ԃɂ�镪��
                    // �K�[�h
                    if (_enemy.EnemyHeadSphere.HitInfo.targetRole == HitCollider.ColliderRole.Guard)
                        _enemy.Player.TakeImpact();
                    // �p���B
                    else if (_enemy.EnemyHeadSphere.HitInfo.targetRole == HitCollider.ColliderRole.Parry)
                    {
                        _enemy.StateMachine.TransitionTo(EnemyState.GetHit);
                        _enemy.Player.Parry();
                    }
                    else
                        _enemy.IncreaseHitNum();
                }
            }
        }

        void Transition()
        {
            // �x��
            if (_enemy.Animation.IsAnimFinished(EnemyAnimation.HashAttack))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }
    }
}