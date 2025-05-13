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
        public static readonly int HashMove = Animator.StringToHash("Move");
        public static readonly int HashAttack1 = Animator.StringToHash("Attack1");
        public static readonly int HashAttack2 = Animator.StringToHash("Attack2");
        public static readonly int HashAttack3 = Animator.StringToHash("Attack3");

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
            _animator.SetBool(HashAttack1, false);
            _animator.SetBool(HashAttack2, false);
            _animator.SetBool(HashAttack3, false);
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
        /// 攻撃アニメーション開始
        /// </summary>
        public void Attack1()
        {
            ResetAll();
            _animator.SetBool(HashAttack1, true);
        }

        /// <summary>
        /// 攻撃2アニメーション開始
        /// </summary>
        public void Attack2()
        {
            ResetAll();
            _animator.SetBool(HashAttack2, true);
        }

        /// <summary>
        /// 攻撃3アニメーション開始
        /// </summary>
        public void Attack3()
        {
            ResetAll();
            _animator.SetBool(HashAttack3, true);
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
