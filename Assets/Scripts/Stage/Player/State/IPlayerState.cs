using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// 各プレイヤーステートの基礎
    /// </summary>
    public interface IPlayerState
    {
        void Enter()
        {
            // 各ステートに入った際の処理
        }

        void Update()
        {
            // マイフレーム行う処理
        }

        void FixedUpdate()
        {
            // 移動などの重たい処理
        }

        void Exit()
        {
            // 各ステートを抜ける際の処理
        }
    }
}
