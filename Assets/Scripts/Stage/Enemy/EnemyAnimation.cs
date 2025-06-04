using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵のアニメーションを管理
    /// </summary>
    public class EnemyAnimation
    {
        // AnimatorのパラメータハッシュID
        public static readonly int HashRoar = Animator.StringToHash("Roar");
        public static readonly int HashAlert = Animator.StringToHash("Alert");
        public static readonly int HashChase = Animator.StringToHash("Chase");
        public static readonly int HashAttack = Animator.StringToHash("Attack");

        // コンポーネント
        Animator _animator;

        // アニメーションステート情報保存用
        AnimatorStateInfo _currentStateInfo;

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// アニメーションリセット
        /// </summary>
        public void ResetAll()
        {
            _animator.SetBool(HashRoar, false);
            _animator.SetBool(HashAlert, false);
            _animator.SetBool(HashChase, false);
            _animator.SetBool(HashAttack, false);
        }

        /// <summary>
        /// 咆哮アニメーション開始
        /// </summary>
        public void Roar()
        {
            ResetAll();
            _animator.SetBool(HashRoar, true);
        }

        /// <summary>
        /// 警戒アニメーション開始
        /// </summary>
        public void Alert()
        {
            ResetAll();
            _animator.SetBool(HashAlert, true);
        }

        /// <summary>
        /// 追跡アニメーション開始
        /// </summary>
        public void Chase()
        {
            ResetAll();
            _animator.SetBool(HashChase, true);
        }

        /// <summary>
        /// 攻撃アニメーション開始
        /// </summary>
        public void Attack()
        {
            ResetAll();
            _animator.SetBool(HashAttack, true);
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
    }
}
