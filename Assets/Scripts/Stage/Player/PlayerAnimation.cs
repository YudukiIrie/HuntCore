using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーのアニメーションを管理
    /// </summary>
    public class PlayerAnimation
    {
        // AnimatorのパラメータハッシュID
        // 定数かつ実行時に値が決まるためstatic readonly
        public static readonly int HashIdle = Animator.StringToHash("Base Layer.Idle");
        public static readonly int HashMove = Animator.StringToHash("Base Layer.Move");
        public static readonly int HashLightAttack = Animator.StringToHash("Base Layer.LightAttack");
        public static readonly int HashHeavyAttack = Animator.StringToHash("Base Layer.HeavyAttack");
        public static readonly int HashSpecialAttack = Animator.StringToHash("Base Layer.SpecialAttack");
        public static readonly int HashImpacted = Animator.StringToHash("Base Layer.Impacted");

        // コンポーネント
        Animator _animator;

        // アニメーションステート情報保存用
        AnimatorStateInfo _currentStateInfo;

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// 待機アニメーション開始
        /// </summary>
        public void Idle()
        {
            _animator.CrossFade(HashIdle, 0.1f);
        }

        /// <summary>
        /// 移動アニメーション開始
        /// </summary>
        public void Move()
        {
            _animator.CrossFade(HashMove, 0.1f);
        }

        /// <summary>
        /// ライト攻撃アニメーション開始
        /// </summary>
        public void LightAttack()
        {
            _animator.CrossFade(HashLightAttack, 0.1f);
        }

        /// <summary>
        /// ヘビー攻撃アニメーション開始
        /// </summary>
        public void HeavyAttack()
        {
            _animator.CrossFade(HashHeavyAttack, 0.1f);
        }

        /// <summary>
        /// スペシャル攻撃アニメーション開始
        /// </summary>
        public void SpecialAttack()
        {
            _animator.CrossFade(HashSpecialAttack, 0.1f);
        }

        /// <summary>
        /// 衝撃アニメーション開始
        /// </summary>
        public void Impacted()
        {
            _animator.CrossFade(HashImpacted, 0.1f);
        }

        /// <summary>
        /// 指定したアニメーションステートが再生中かチェック
        /// </summary>
        bool CheckCurrentState(int currentStateHash)
        {
            // BaseLayerのステート情報を取得
            _currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            // 再生中のステートが指定したステートと同じかチェック
            bool check = (_currentStateInfo.fullPathHash == currentStateHash);

            return check;
        }

        /// <summary>
        /// 指定したアニメーションの終了チェック
        /// </summary>
        /// <returns>true:再生終了, false:再生中</returns>
        public bool IsAnimFinished(int stateHash)
        {
            if (CheckCurrentState(stateHash))
            {
                // アニメーション終了待ち
                float time = _currentStateInfo.normalizedTime;
                if (time >= 1.0f)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 指定したアニメーション再生時間を0～1の割合に変換した値を返却
        /// </summary>
        public float CheckAnimRatio(int stateHash)
        {
            if (CheckCurrentState(stateHash))
                return _currentStateInfo.normalizedTime;

            return 0.0f;
        }
    }
}
