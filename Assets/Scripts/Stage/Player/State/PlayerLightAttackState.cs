using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーライト攻撃状態
    /// </summary>
    public class PlayerLightAttackState : IState
    {
        Player _player;     // プレイヤークラス
        float _elapseTime;  // コンボ間猶予経過時間
        float _hitStartRatio;
        float _chainTime;
        float _afterImageEndRatio;


        public PlayerLightAttackState(Player player)
        {
            _player = player;
            _hitStartRatio = WeaponData.Data.LightAttackHitStartRatio;
            _chainTime = PlayerData.Data.ChainTime;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.LightAttack();
        }

        public void Update()
        {
            // === 当たり判定 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) >= _hitStartRatio)
            {
                if (_player.HitChecker.IsCollideBoxOBB(_player.HitChecker.GreatSwordOBB, _player.HitChecker.EnemyOBB))
                {
                    _player.IncreaseHitNum();
                    Debug.Log("ライト攻撃ヒット");
                }
            }

            // === 残像の生成 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);

            // === 状態遷移 ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashLightAttack))
            {
                _elapseTime += Time.deltaTime;
                // ヘビー攻撃
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.HeavyAttackState);
                }
                // 待機
                else
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapseTime = 0.0f;
            _player.HitChecker.GreatSwordOBB.ResetHitInfo();
            _player.HitChecker.EnemyOBB.ResetHitInfo();
        }
    }
}
