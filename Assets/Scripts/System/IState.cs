/// <summary>
/// 各ステートの基礎
/// </summary>
public interface IState
{
    void Enter()
    {
        // 各ステートに入った際の処理
    }

    void Update()
    {
        // マイフレーム行う処理
    }

    void FixedUpdate()
    {
        // 移動などの重たい処理
    }

    void Exit()
    {
        // 各ステートを抜ける際の処理
    }
}
