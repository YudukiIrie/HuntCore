using UnityEngine;

/// <summary>
/// ゲーム終了管理クラス
/// </summary>
public class GameManager : MonoBehaviour
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

        LockCursor();
    }

    void Update()
    {
        EndGame();
    }

    /// <summary>
    /// カーソルロック&非表示
    /// </summary>
    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
