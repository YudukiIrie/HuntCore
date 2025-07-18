using UnityEngine;

namespace Stage.Players
{
    public class PlayerParryState : IState
    {
        Player _player;

        // �f�[�^�L���b�V���p
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
            // === �ړ��v�Z ===


            // === ��ԑJ�� ===
            
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}