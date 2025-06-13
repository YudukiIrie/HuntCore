using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// 敵のアニメーションを管理
    /// </summary>
    public class EnemyAnimation
    {
        // AnimatorのパラメータハッシュID
        public static readonly int HashIdle = Animator.StringToHash("Base Layer.Idle");
        public static readonly int HashRoar = Animator.StringToHash("Base Layer.Roar");
        public static readonly int HashAlert = Animator.StringToHash("Base Layer.Alert");
        public static readonly int HashChase = Animator.StringToHash("Base Layer.Chase");
        public static readonly int HashAttack = Animator.StringToHash("Base Layer.Attack");

        // コンポーネント
        Animator _animator;

        // アニメーションステート情報保存用
        AnimatorStateInfo _currentStateInfo;

        float _animBlendTime;

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
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
    }
}
