using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene loading

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button retryButton; // Assign button
    [SerializeField] private Button menu;


    private void Start()
    {
        checkbutton();
    }

    public void checkbutton()
    {
        retryButton.onClick.AddListener(redo); // Add click listener
        menu.onClick.AddListener(gomenu);
    }

public void redo()
    {
        SceneManager.LoadScene("Main Level Scene");
    }

    public void gomenu() {
        SceneManager.LoadScene("Main Menu Scene");
    }
}
