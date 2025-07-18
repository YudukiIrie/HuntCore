using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーのアニメーションを管理
    /// </summary>
    public class PlayerAnimation : Animation
    {
        // AnimatorのパラメータハッシュID
        // 定数かつ実行時に値が決まるためstatic readonly
        public static readonly int HashIdle    = Animator.StringToHash("Base Layer.Idle");
        public static readonly int HashMove    = Animator.StringToHash("Base Layer.Move");
        public static readonly int HashParry   = Animator.StringToHash("Base Layer.Parry");
        public static readonly int HashBlocked = Animator.StringToHash("Base Layer.Blocked");
        public static readonly int HashGuardBegin    = Animator.StringToHash("Base Layer.GuardBegin");
        public static readonly int HashImpacted      = Animator.StringToHash("Base Layer.Impacted");
        public static readonly int HashLightAttack   = Animator.StringToHash("Base Layer.LightAttack");
        public static readonly int HashHeavyAttack   = Animator.StringToHash("Base Layer.HeavyAttack");
        public static readonly int HashSpecialAttack = Animator.StringToHash("Base Layer.SpecialAttack");
        static readonly int HashSpeed = Animator.StringToHash("Speed");

        float _animBlendTime;

        public PlayerAnimation(Animator animator) : base(animator)
        {
            _animBlendTime = PlayerData.Data.AnimBlendTime;
        }

        /// <summary>
        /// 待機アニメーション開始
        /// </summary>
        public void Idle()
        {
            _animator.CrossFade(HashIdle, _animBlendTime);
        }

        /// <summary>
        /// 移動アニメーション開始
        /// </summary>
        public void Move()
        {
            _animator.CrossFade(HashMove, _animBlendTime);
        }

        /// <summary>
        /// ライト攻撃アニメーション開始
        /// </summary>
        public void LightAttack()
        {
            _animator.CrossFade(HashLightAttack, _animBlendTime);
        }

        /// <summary>
        /// ヘビー攻撃アニメーション開始
        /// </summary>
        public void HeavyAttack()
        {
            _animator.CrossFade(HashHeavyAttack, _animBlendTime);
        }

        /// <summary>
        /// スペシャル攻撃アニメーション開始
        /// </summary>
        public void SpecialAttack()
        {
            _animator.CrossFade(HashSpecialAttack, _animBlendTime);
        }

        /// <summary>
        /// 衝撃アニメーション開始
        /// </summary>
        public void Impacted()
        {
            _animator.CrossFade(HashImpacted, _animBlendTime);
        }

        /// <summary>
        /// ガードアニメーション開始
        /// </summary>
        public void Guard()
        {
            _animator.CrossFade(HashGuardBegin, _animBlendTime);
        }

        /// <summary>
        /// ガード後アニメーション開始
        /// </summary>
        public void Blocked()
        {
            _animator.CrossFade(HashBlocked, _animBlendTime);
        }

        /// <summary>
        /// ガードキャンセルアニメーション開始
        /// 引数未指定の場合は最大割合から(ガード後からの遷移など)
        /// </summary>
        /// <param name="normalizedTime">逆再生時開始割合</param>
        public void CancelGuard(float normalizedTime = 1.0f)
        {
            _animator.SetFloat(HashSpeed, -1);
            // 再生時間は1以上になる場合があるため制限を設ける
            float offset = Mathf.Clamp(normalizedTime, 0.0f, 1.0f);
            _animator.CrossFade(HashGuardBegin, _animBlendTime, 0, offset);
        }

        /// <summary>
        /// パリィアニメーション開始
        /// </summary>
        public void Parry()
        {
            _animator.CrossFade(HashParry, _animBlendTime);
        }

        /// <summary>
        /// パラメータのリセット
        /// </summary>
        public void ResetSpeed()
        {
            _animator.SetFloat(HashSpeed, 1);
        }
    }
}
