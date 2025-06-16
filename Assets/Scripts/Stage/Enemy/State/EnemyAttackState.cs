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
                if (_enemy.HitChecker.IsCollideBoxOBB(_enemy.HitChecker.EnemyHeadOBB, _enemy.HitChecker.PlayerOBB))
                {
                    Debug.Log("���݂��q�b�g");
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
            _enemy.HitChecker.EnemyHeadOBB.ResetHitInfo();
            _enemy.HitChecker.PlayerOBB.ResetHitInfo();
        }
    }
}