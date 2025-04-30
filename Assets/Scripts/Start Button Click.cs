using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene loading
using System.Collections; // For IEnumerator

public class StartButtonClick : MonoBehaviour
{
    [SerializeField] private Button startButton; // Assign button in Inspector
    private GameObject player; // Player object
    private Camera mainCamera; // Main camera
    private bool isMoving = false; // To prevent multiple clicks

    public GameObject IBG;
    public Button Instruct;
    public GameObject message;
    public GameObject message2;
    public Button nextmessage;
    public Button nextmessage2;
    public Button QuitMenu;

    [Header("Audio")]
    public AudioClip menunoise;
    private AudioSource audioSource;

    void Start()
    {
        mainmenu();
    }
    private void mainmenu()
    {
        startButton.gameObject.SetActive(true); // Show buttons
        player = GameObject.FindGameObjectWithTag("Player"); // Find player
        mainCamera = Camera.main; // Get the main camera
        startButton.onClick.AddListener(OnStartButtonClicked); // Add click listener


        IBG.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
        message2.gameObject.SetActive(false);
        nextmessage.gameObject.SetActive(false);
        nextmessage2.gameObject.SetActive(false);
        QuitMenu.gameObject.SetActive(false);

        Instruct.onClick.AddListener(Instructclick); // Add click listener
    }
    private void OnStartButtonClicked()
    {
        if (!isMoving && player != null)
        {
            startButton.gameObject.SetActive(false); // Hide button
            isMoving = true; // Prevent multiple clicks
            menusound();
            StartCoroutine(MoveCameraThenPlayer());
        }}

    private void Instructclick()
    {
        if (!isMoving && player != null)
        {
            startButton.gameObject.SetActive(false);
            IBG.gameObject.SetActive(true);
            message.gameObject.SetActive(true);
            message2.gameObject.SetActive(false);
            QuitMenu.gameObject.SetActive(true);
            nextmessage.gameObject.SetActive(true);
            nextmessage2.gameObject.SetActive(false);
            menusound();

            QuitMenu.onClick.AddListener(mainmenu); // Add click listener
            nextmessage.onClick.AddListener(nexttext);
        }
    }
    private void nexttext()
    {
        nextmessage.gameObject.SetActive(false);
        nextmessage2.gameObject.SetActive(true);
        message2.gameObject.SetActive(true);
        message.gameObject.SetActive(false);

        nextmessage2.onClick.AddListener(Instructclick);
        QuitMenu.onClick.AddListener(mainmenu);
    }
    //private void nexttext2()
    //{
    //    message2.gameObject.SetActive(false);
    //    message.gameObject.SetActive(true);
    //    nextmessage2.gameObject.SetActive(false);
    //    //nextmessage.onClick.AddListener(nexttext);
    //}
    private IEnumerator MoveCameraThenPlayer()
    {
        // First, move the camera to target position
        Vector3 cameraStartPos = mainCamera.transform.position;
        Vector3 cameraTargetPos = new Vector3(223.9f, 63.2f, 391.3f);
        float cameraMoveDuration = 2f;
        float elapsed = 0f;

        while (elapsed < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(cameraStartPos, cameraTargetPos, elapsed / cameraMoveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = cameraTargetPos; // Ensure exact position

        yield return StartCoroutine(GlidePlayer());
    }

    private IEnumerator GlidePlayer()
    {
        Vector3 startPosition = player.transform.position; // Starting position
        Vector3 targetY = startPosition; //+ new Vector3(0, 8, 0); // Move up 8 units
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
        SceneManager.LoadScene("Cutscenes - Copy");
    }
    IEnumerator menusound()
    {
        audioSource.PlayOneShot(menunoise);
        yield return new WaitForSeconds(menunoise.length);

    }
}