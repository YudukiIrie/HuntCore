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
        public static readonly int HashWalk = Animator.StringToHash("Base Layer.Walk");
        public static readonly int HashDown = Animator.StringToHash("Base Layer.Down");
        public static readonly int HashAlert = Animator.StringToHash("Base Layer.Alert");
        public static readonly int HashChase = Animator.StringToHash("Base Layer.Chase");
        public static readonly int HashAttack = Animator.StringToHash("Base Layer.Attack");
        public static readonly int HashGetHit = Animator.StringToHash("Base Layer.GetHit");
        public static readonly int HashClawAttack = Animator.StringToHash("Base Layer.ClawAttack");       
        static readonly int HashSpeed = Animator.StringToHash("Speed");

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
            ResetParam();
            _animator.CrossFade(HashIdle, _animBlendTime);
        }

        /// <summary>
        /// 咆哮アニメーション開始
        /// </summary>
        public void Roar()
        {
            ResetParam();
            _animator.CrossFade(HashRoar, _animBlendTime);
        }

        /// <summary>
        /// 警戒アニメーション開始
        /// </summary>
        public void Alert()
        {
            ResetParam();
            _animator.CrossFade(HashAlert, _animBlendTime);
        }

        /// <summary>
        /// 追跡アニメーション開始
        /// </summary>
        public void Chase()
        {
            ResetParam();
            _animator.CrossFade(HashChase, _animBlendTime);
        }

        /// <summary>
        /// 攻撃アニメーション開始
        /// </summary>
        public void Attack()
        {
            ResetParam();
            _animator.CrossFade(HashAttack, _animBlendTime);
        }

        /// <summary>
        /// 歩きアニメーション開始
        /// </summary>
        public void Walk()
        {
            ResetParam();
            _animator.CrossFade(HashWalk, _animBlendTime);
        }

        /// <summary>
        /// 被攻撃アニメーション開始
        /// </summary>
        public void GetHit()
        {
            ResetParam();
            _animator.CrossFade(HashGetHit, _animBlendTime);
        }

        /// <summary>
        /// ダウンアニメーション開始
        /// </summary>
        public void Down()
        {
            ResetParam();
            _animator.CrossFade(HashDown, _animBlendTime);
        }

        /// <summary>
        /// 起き上がりアニメーション開始
        /// </summary>
        /// <returns>true:再生終了, false;再生中</returns>
        public void GetUp()
        {
            ResetParam();
            // ダウンアニメーションを逆再生
            _animator.SetFloat(HashSpeed, -1);
            _animator.CrossFade(HashDown, _animBlendTime, 0, 1);
        }

        /// <summary>
        /// 爪攻撃アニメーション開始
        /// </summary>
        public void ClawAttack()
        {
            ResetParam();
            _animator.CrossFade(HashClawAttack, _animBlendTime);
        }

        /// <summary>
        /// パラメータのリセット
        /// </summary>
        void ResetParam()
        {
            _animator.SetFloat(HashSpeed, 1);
        }
    }
}
