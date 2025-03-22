using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene loading
using System.Collections; // For IEnumerator

public class StartButtonClick : MonoBehaviour
{
    [SerializeField] private Button startButton; // Assign button in Inspector
    private GameObject player; // Player object
    private bool isMoving = false; // To prevent multiple clicks

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find player
        startButton.onClick.AddListener(OnStartButtonClicked); // Add click listener
    }

    private void OnStartButtonClicked()
    {
        if (!isMoving && player != null)
        {
            startButton.gameObject.SetActive(false); // Hide button
            isMoving = true; // Prevent multiple clicks
            StartCoroutine(GlidePlayer());
        }
    }

    private IEnumerator GlidePlayer()
    {
        Vector3 startPosition = player.transform.position; // Starting position
        Vector3 targetY = startPosition + new Vector3(0, 8, 0); // Move up 8 units
        Vector3 targetXZ = targetY + new Vector3(53.4f, 0, -68.1f); // Move x+53.4, z-68.1

        float durationFirstGlide = 2f;
        float durationSecondGlide = 4f;
        float elapsed = 0f;

        // Go up 8 units on Y axis
        while (elapsed < durationFirstGlide)
        {
            player.transform.position = Vector3.Lerp(startPosition, targetY, elapsed / durationFirstGlide);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetY; // Ensure exact position
        elapsed = 0f; // Reset timer

        // Move x+53.4 and z-68.1
        while (elapsed < durationSecondGlide)
        {
            player.transform.position = Vector3.Lerp(targetY, targetXZ, elapsed / durationSecondGlide);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetXZ; // Ensure exact position

        // Load the level after moving
        SceneManager.LoadScene("Main Level Scene");
    }
}