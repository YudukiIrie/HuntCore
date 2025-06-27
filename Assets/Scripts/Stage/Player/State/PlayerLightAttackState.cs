using Stage.HitCheck;
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

        // データキャッシュ用
        float _hitStartRatio;
        float _hitEndRatio;
        float _chainTime;
        float _afterImageEndRatio;


        public PlayerLightAttackState(Player player)
        {
            _player = player;
            _hitStartRatio = WeaponData.Data.LightAttackHitStartRatio;
            _hitEndRatio   = WeaponData.Data.LightAttackHitEndRatio;
            _chainTime     = PlayerData.Data.ChainTime;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.LightAttack();
        }

        public void Update()
        {
            // === 当たり判定 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) >= _hitStartRatio &&
                _player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) <= _hitEndRatio)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                    _player.IncreaseHitNum();
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
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.EnemyColliders);
        }
    }
}
