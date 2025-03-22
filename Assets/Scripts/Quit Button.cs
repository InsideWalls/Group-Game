using UnityEngine;
using UnityEngine.UI;

public class QuitButtonClick : MonoBehaviour
{
    [SerializeField] private Button quitButton; // Assign button in Inspector

    void Start()
    {
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnQuitButtonClicked()
    {
        QuitGame();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        // If running in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a standalone build
        Application.Quit();
#endif
    }
}