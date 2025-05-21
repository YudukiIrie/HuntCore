using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemy
{
    /// <summary>
    /// 敵のアニメーションを管理
    /// </summary>
    public class EnemyAnimation
    {
        // AnimatorのパラメータハッシュID
        public static readonly int HashWander = Animator.StringToHash("Wander");

        // コンポーネント
        Animator _animator;

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// アニメーションリセット
        /// </summary>
        public void ResetAll()
        {
            _animator.SetBool(HashWander, false);
        }

        /// <summary>
        /// うろつきアニメーション開始
        /// </summary>
        public void Wander()
        {
            ResetAll();
            _animator.SetBool(HashWander, true);
        }
    }
}
