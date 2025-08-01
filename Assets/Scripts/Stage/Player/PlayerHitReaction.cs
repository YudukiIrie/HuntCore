using Stage.Enemies;
using Stage.HitDetection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// 被攻撃リアクション判別 & 発火クラス
    /// </summary>
    public class PlayerHitRaction
    {
        Player _player;
        Dictionary<ColliderRole, Action> _reactions;

        public PlayerHitRaction(Player player, int capacity)
        {
            _player = player;
            _reactions = 
                new Dictionary<ColliderRole, Action>(capacity);

            _reactions.Add(ColliderRole.Body, BodyReaction);
            _reactions.Add(ColliderRole.Guard, GuradReaction);
            _reactions.Add(ColliderRole.Parry, ParryReaction);
        }

        /// <summary>
        /// 被攻撃時のリアクション判別 & 発火
        /// </summary>
        /// <param name="own">判別用コライダー属性</param>
        public void ReactToHit(HitCollider own)
        {
            ColliderRole role = own.Role;

            if (_reactions.TryGetValue(role, out Action action))
                action();
            else
                Debug.LogError("The" + role + "of Action was not found.");
        }

        /// <summary>
        /// 体ヒット時リアクション
        /// </summary>
        void BodyReaction()
        {
            _player.IncreaseHitNum();
        }

        /// <summary>
        /// ガード時リアクション
        /// </summary>
        void GuradReaction()
        {
            _player.TakeImpact();
        }

        /// <summary>
        /// パリィ時リアクション
        /// </summary>
        void ParryReaction()
        {
            _player.Enemy.TakeImpact(EnemyState.GetHit);
            _player.Parry();
        }
    }
}