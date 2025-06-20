using Stage.HitCheck;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�U���X�e�[�g
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // �G�N���X
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
            // === �����蔻�� ===
            if (_enemy.Animation.CheckAnimRatio(EnemyAnimation.HashAttack) >= _hitStartRatio)
            {
                if (OBBHitChecker.IsCollideBoxOBB(_enemy.EnemyHeadOBB, _enemy.Player.PlayerOBBs))
                {
                    // �ڐGOBB���K�[�h(�v���C���[���K�[�h��)�̏ꍇ�������łȂ����̕���
                    if (_enemy.EnemyHeadOBB.HitInfo.targetType == OBB.OBBType.Guard)
                        _enemy.Player.TakeImpact();
                    else
                        _enemy.IncreaseHitNum();
                }
            }

            // === ��ԑJ�� ===
            // �x��
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