using UnityEngine;

/// <summary>
/// �Q�[���I���Ǘ��N���X
/// </summary>
public class GameEndManager : MonoBehaviour
{
    // �ۑ���p�X
    const string PATH = "GameEndManager";

    PlayerAction _action;

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

        _action = new PlayerAction();
        _action.Enable();
    }

    void Update()
    {
        EndGame();
    }

    /// <summary>
    /// �Q�[���I������
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
