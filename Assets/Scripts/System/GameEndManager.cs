using UnityEngine;

/// <summary>
/// ゲーム終了管理クラス
/// </summary>
public class GameEndManager : MonoBehaviour
{
    // 保存先パス
    const string PATH = "GameEndManager";

    PlayerAction _action;

    // Resourcesからロードする
    [RuntimeInitializeOnLoadMethod]
    static void CreateInstance()
    {
        var prefab = Resources.Load<GameObject>(PATH);
        Instantiate(prefab);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _action = new PlayerAction();
        _action.Enable();
    }

    void Update()
    {
        EndGame();
    }

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    void EndGame()
    {
        if (_action.Player.End.IsPressed())
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
