using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To load scenes
using System.Collections;
using NUnit.Framework.Constraints;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button retryButton; // Assign button
    [SerializeField] private Button menu;

    [Header("Audio")]
    public AudioClip menunoise;
    public AudioSource audioSource;

    public bool sound = false;


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = menunoise;
        checkbutton();
    }
    public void checkbutton()
    {
        retryButton.onClick.AddListener(redo); // Add click listener
        menu.onClick.AddListener(gomenu2);
    }

public void redo()
    {
        menusound();
        Invoke("rety2", 1.5f);
        
    }
    public void rety2()
    {
        menusound();
        SceneManager.LoadScene("Main Level Scene");
    }

    public void gomenu2()
    {
        menusound();
        Invoke("gomenu", 1.5f);
    }
    public void gomenu() {
        SceneManager.LoadScene("Main Menu Scene");
    }
    // Plays noise on button click and then waits for it to finish before the system will read the next bit of code
    public void menusound()
    {
            audioSource.Play();
            
        
    }
}
