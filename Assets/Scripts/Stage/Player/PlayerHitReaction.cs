using Stage.Enemies;
using Stage.HitCheck;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// ��U�����A�N�V�������� & ���΃N���X
    /// </summary>
    public class PlayerHitRaction
    {
        Player _player;
        Dictionary<HitCollider.ColliderRole, Action> _reactions;

        public PlayerHitRaction(Player player, int capacity)
        {
            _player = player;
            _reactions = 
                new Dictionary<HitCollider.ColliderRole, Action>(capacity);

            _reactions.Add(HitCollider.ColliderRole.Body, BodyReaction);
            //_reactions.Add(HitCollider.ColliderRole.Roll, RollReaction);
            _reactions.Add(HitCollider.ColliderRole.Guard, GuradReaction);
            _reactions.Add(HitCollider.ColliderRole.Parry, ParryReaction);
        }

        /// <summary>
        /// ��U�����̃��A�N�V�������� & ����
        /// </summary>
        /// <param name="own">���ʗp�R���C�_�[����</param>
        public void ReactToHit(HitCollider own)
        {
            HitCollider.ColliderRole role = own.Role;

            if (_reactions.TryGetValue(role, out Action action))
                action();
            else
                Debug.LogError("The" + role + "of Action was not found.");
        }

        /// <summary>
        /// �̃q�b�g�����A�N�V����
        /// </summary>
        void BodyReaction()
        {
            _player.IncreaseHitNum();
        }

        /// <summary>
        /// ��������A�N�V����
        /// </summary>
        //void RollReaction()
        //{
        //    // ���G�̂��߃��A�N�V�����Ȃ�
        //}

        /// <summary>
        /// �K�[�h�����A�N�V����
        /// </summary>
        void GuradReaction()
        {
            _player.TakeImpact();
        }

        /// <summary>
        /// �p���B�����A�N�V����
        /// </summary>
        void ParryReaction()
        {
            _player.Enemy.TakeImpact(EnemyState.GetHit);
            _player.Parry();
        }
    }
}