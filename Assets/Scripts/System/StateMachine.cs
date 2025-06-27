using System;
using System.Collections.Generic;

/// <summary>
/// ステートマシーン親クラス
/// </summary>
/// <typeparam name="TState">対応するステート列挙体</typeparam>
public abstract class StateMachine<TState> where TState : Enum
{
    // 現在のステート
    protected IState _currentState;

    // ステート指定用辞書
    protected readonly Dictionary<TState, IState> _states;

    public StateMachine(int capacity)
    {
        _states = new Dictionary<TState, IState>(capacity);
    }

    /// <summary>
    /// ステートの初期化
    /// </summary>
    /// <param name="key">初期ステートkey</param>
    public abstract void Initialize(TState key);

    /// <summary>
    /// 各ステートのUpdate()を動かす
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// 各ステートのFixedUpdate()を動かす
    /// </summary>
    public abstract void FixedUpdate();

    /// <summary>
    /// 状態遷移
    /// </summary>
    /// <param name="key">切り替え後ステートkey</param>
    public abstract void TransitionTo(TState key);
}
