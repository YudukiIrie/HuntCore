using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤー攻撃1段目状態
    /// </summary>
    public class PlayerAttack1State : IState
    {
        Player _player;     // プレイヤークラス
        float _elapseTime;  // コンボ間猶予経過時間
        float _hitStartRatio;
        float _chainTime;


        public PlayerAttack1State(Player player)
        {
            _player = player;
            _hitStartRatio = WeaponData.Data.Attack1HitStartRatio;
            _chainTime = PlayerData.Data.ChainTime;
        }

        public void Enter()
        {
            _player.Animation.Attack1();
        }

        public void Update()
        {
            // === 当たり判定 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack1) >= _hitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB, _player.HitCheck.EnemyOBB))
                {
                    Debug.Log("1当たった");
                }
            }

            // === 状態遷移 ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashAttack1))
            {
                _elapseTime += Time.deltaTime;
                // 攻撃2
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.Attack2State);
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
            _player.HitCheck.ResetHitInfo();
        }
    }
}
