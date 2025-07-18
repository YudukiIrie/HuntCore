using UnityEngine;

namespace Stage.Players
{
    public class PlayerParryState : IState
    {
        Player _player;

        // データキャッシュ用
        float _moveSpeed;
        float _rotSpeed;

        public PlayerParryState(Player player)
        {
            _player = player;

            _moveSpeed = PlayerData.Data.MoveSpeedOfParry;
            _rotSpeed = PlayerData.Data.RotSpeedOfParry;
        }

        public void Enter()
        {
            _player.Animation.Parry();
        }

        public void Update()
        {
            // === 移動計算 ===


            // === 状態遷移 ===
            
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}