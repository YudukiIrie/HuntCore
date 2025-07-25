using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム管理クラス
/// 役割：カーソルの削除
/// 　　　シーンの再読み込み
/// 　　　ゲーム終了
/// </summary>
public class GameManager : MonoBehaviour
{
    // 保存先パス
    const string PATH = "GameManager";

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
        ResetGame();

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
    /// ゲームリセット処理
    /// </summary>
    void ResetGame()
    {
        if (Action.Player.Reset.IsPressed())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
