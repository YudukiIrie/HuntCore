using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���Ǘ��N���X
/// �����F�J�[�\���̍폜
/// �@�@�@�V�[���̍ēǂݍ���
/// �@�@�@�Q�[���I��
/// </summary>
public class GameManager : MonoBehaviour
{
    // �ۑ���p�X
    const string PATH = "GameManager";

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
        ResetGame();

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
    /// �Q�[�����Z�b�g����
    /// </summary>
    void ResetGame()
    {
        if (Action.Player.Reset.IsPressed())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
