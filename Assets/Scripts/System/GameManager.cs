using UnityEngine;

/// <summary>
/// �Q�[���I���Ǘ��N���X
/// </summary>
public class GameManager : MonoBehaviour
{
    // �ۑ���p�X
    const string PATH = "GameEndManager";

    public static PlayerAction Action { get; private set; }

    // Resources���烍�[�h����
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
    /// �J�[�\�����b�N&��\��
    /// </summary>
    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// �Q�[���I������
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
