using UnityEngine;
using UnityEngine.SceneManagement; 

public class ESCButton : MonoBehaviour
{
    private void Start()
    {
        
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Load scene
            SceneManager.LoadScene("win");
        }
    }
}
