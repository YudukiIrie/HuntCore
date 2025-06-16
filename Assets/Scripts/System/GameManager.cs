using UnityEngine;

/// <summary>
/// ゲーム終了管理クラス
/// </summary>
public class GameManager : MonoBehaviour
{
    // 保存先パス
    const string PATH = "GameEndManager";

    public static PlayerAction Action { get; private set; }

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

        Action = new PlayerAction();
        Action.Enable();

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
        if (Action.Player.End.IsPressed())
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
