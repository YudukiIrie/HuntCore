using Stage.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーが衝撃を受けた状態
    /// </summary>
    public class PlayerImpactedState : IState
    {
        Player _player; // プレイヤークラス

        public PlayerImpactedState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            
        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
