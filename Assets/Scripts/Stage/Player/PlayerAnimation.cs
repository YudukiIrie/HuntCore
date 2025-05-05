using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// プレイヤーのアニメーションを管理
    /// </summary>
    public class PlayerAnimation
    {
        // AnimatorのパラメータハッシュID
        // 定数かつ実行時に値が決まるためstatic readonly
        static readonly int _hashMove = Animator.StringToHash("Move");
        static readonly int _hashAttack1 = Animator.StringToHash("Attack1");
        static readonly int _hashAttack2 = Animator.StringToHash("Attack2");
        static readonly int _hashAttack3 = Animator.StringToHash("Attack3");

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
            _animator.SetBool(_hashMove, false);
            _animator.SetBool(_hashAttack1, false);
            _animator.SetBool(_hashAttack2, false);
            _animator.SetBool(_hashAttack3, false);
        }

        /// <summary>
        /// 移動アニメーション開始
        /// </summary>
        public void Move()
        {
            ResetAll();
            _animator.SetBool(_hashMove, true);
        }

        /// <summary>
        /// 攻撃アニメーション開始
        /// </summary>
        public void Attack1()
        {
            ResetAll();
            _animator.SetBool(_hashAttack1, true);
        }

        /// <summary>
        /// 攻撃2アニメーション開始
        /// </summary>
        public void Attack2()
        {
            ResetAll();
            _animator.SetBool(_hashAttack2, true);
        }

        /// <summary>
        /// 攻撃3アニメーション開始
        /// </summary>
        public void Attack3()
        {
            ResetAll();
            _animator.SetBool(_hashAttack3, true);
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
        /// 攻撃1ステートアニメーション終了チェック
        /// </summary>
        /// <returns>true:再生終了, false:再生中</returns>
        public bool IsAttack1StateFinished()
        {
            if (CheckCurrentState(_hashAttack1))
            {
                // アニメーション終了待ち
                float time = _currentStateInfo.normalizedTime;
                if (time >= 1.0f)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 攻撃2ステートアニメーション終了チェック
        /// </summary>
        /// <returns>true:再生終了, false:再生中</returns>
        public bool IsAttack2StateFinished()
        {
            if (CheckCurrentState(_hashAttack2))
            {
                float time = _currentStateInfo.normalizedTime;
                if (time >= 1.0f)
                    return true;
            }
            return false;
        }
    }
}
