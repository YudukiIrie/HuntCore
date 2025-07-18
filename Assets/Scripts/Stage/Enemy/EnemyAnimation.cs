using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵のアニメーションを管理
    /// </summary>
    public class EnemyAnimation : Animation
    {
        // AnimatorのパラメータハッシュID
        public static readonly int HashIdle = Animator.StringToHash("Base Layer.Idle");
        public static readonly int HashRoar = Animator.StringToHash("Base Layer.Roar");
        public static readonly int HashAlert = Animator.StringToHash("Base Layer.Alert");
        public static readonly int HashChase = Animator.StringToHash("Base Layer.Chase");
        public static readonly int HashAttack = Animator.StringToHash("Base Layer.Attack");
        public static readonly int HashWalk = Animator.StringToHash("Base Layer.Walk");

        float _animBlendTime;

        public EnemyAnimation(Animator animator) : base(animator)
        {
            _animBlendTime = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AnimBlendTime;
        }

        /// <summary>
        /// 待機アニメーション開始
        /// </summary>
        public void Idle()
        {
            _animator.CrossFade(HashIdle, _animBlendTime);
        }

        /// <summary>
        /// 咆哮アニメーション開始
        /// </summary>
        public void Roar()
        {
            _animator.CrossFade(HashRoar, _animBlendTime);
        }

        /// <summary>
        /// 警戒アニメーション開始
        /// </summary>
        public void Alert()
        {
            _animator.CrossFade(HashAlert, _animBlendTime);
        }

        /// <summary>
        /// 追跡アニメーション開始
        /// </summary>
        public void Chase()
        {
            _animator.CrossFade(HashChase, _animBlendTime);
        }

        /// <summary>
        /// 攻撃アニメーション開始
        /// </summary>
        public void Attack()
        {
            _animator.CrossFade(HashAttack, _animBlendTime);
        }

        /// <summary>
        /// 歩きアニメーション開始
        /// </summary>
        public void Walk()
        {
            _animator.CrossFade(HashWalk, _animBlendTime);
        }
    }
}
