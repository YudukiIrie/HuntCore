using UnityEngine;

/// <summary>
/// 各アニメーション親クラス
/// </summary>
public class Animation
{
    // コンポーネント
    protected Animator _animator;

    // アニメーションステート情報保存用
    AnimatorStateInfo _currentStateInfo;

    public Animation(Animator animator)
    {
        _animator = animator;
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
