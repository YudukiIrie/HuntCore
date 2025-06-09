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
        public static readonly int HashMove = Animator.StringToHash("Move");
        public static readonly int HashLightAttack = Animator.StringToHash("LightAttack");
        public static readonly int HashHeavyAttack = Animator.StringToHash("HeavyAttack");
        public static readonly int HashSpecialAttack = Animator.StringToHash("SpecialAttack");
        public static readonly int HashImpacted = Animator.StringToHash("Impacted");

        // コンポーネント
        Animator _animator;

        // アニメーションステート情報保存用
        AnimatorStateInfo _currentStateInfo;

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// アニメーションリセット
        /// </summary>
        public void ResetAll()
        {
            _animator.SetBool(HashMove, false);
            _animator.SetBool(HashLightAttack, false);
            _animator.SetBool(HashHeavyAttack, false);
            _animator.SetBool(HashSpecialAttack, false);
            _animator.SetBool(HashImpacted, false);
        }

        /// <summary>
        /// 移動アニメーション開始
        /// </summary>
        public void Move()
        {
            ResetAll();
            _animator.SetBool(HashMove, true);
        }

        /// <summary>
        /// ライト攻撃アニメーション開始
        /// </summary>
        public void LightAttack()
        {
            ResetAll();
            _animator.SetBool(HashLightAttack, true);
        }

        /// <summary>
        /// ヘビー攻撃アニメーション開始
        /// </summary>
        public void HeavyAttack()
        {
            ResetAll();
            _animator.SetBool(HashHeavyAttack, true);
        }

        /// <summary>
        /// スペシャル攻撃アニメーション開始
        /// </summary>
        public void SpecialAttack()
        {
            ResetAll();
            _animator.SetBool(HashSpecialAttack, true);
        }

        /// <summary>
        /// 衝撃アニメーション開始
        /// </summary>
        public void Impacted()
        {
            ResetAll();
            _animator.SetBool(HashImpacted, true);
        }

        /// <summary>
        /// 指定したアニメーションステートが再生中かチェック
        /// </summary>
        bool CheckCurrentState(int currentStateHash)
        {
            // BaseLayerのステート情報を取得
            _currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            // 再生中のステートが指定したステートと同じかチェック
            bool check = (_currentStateInfo.shortNameHash == currentStateHash);
            
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
