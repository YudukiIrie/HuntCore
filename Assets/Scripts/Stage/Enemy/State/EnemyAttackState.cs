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
            // === �����蔻�� ===
            if (_enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack) >= _hitStartRatio &&
                _enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack) <= _hitEndRatio)
            {
                if (OBBHitChecker.IsColliding(_enemy.EnemyHeadSphere, _enemy.Player.PlayerColliders))
                {
                    // �ڐGOBB���K�[�h(�v���C���[���K�[�h��)�̏ꍇ�������łȂ����̕���
                    if (_enemy.EnemyHeadSphere.HitInfo.targetRole == HitCollider.ColliderRole.Guard)
                        _enemy.Player.TakeImpact();
                    else
                        _enemy.IncreaseHitNum();
                }
            }

            // === ��ԑJ�� ===
            // �x��
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