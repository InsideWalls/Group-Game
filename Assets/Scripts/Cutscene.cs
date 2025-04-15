using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public GameObject player; // Player object
    public float zMovementAmount = -18f; // Movement distance
    public float movementDuration = 2f; // Time to move

    public Camera targetCamera; // Camera
    public float cameraRotationAmount = 100f;
    public float rotationDuration = 1f;

    public AudioClip soundEffect; // Alert sound
    public float soundDelayAfterRotation = 0.5f;

    public string nextSceneName = "Main Level Scene";
    public float sceneLoadDelayAfterSound = 1.5f;

    private AudioSource audioSource;
    private Vector3 initialPlayerPosition;
    private Vector3 initialCameraRotation;

    public GameObject alert;

    private void Awake()
    {
        alert.gameObject.SetActive(false);

        // Set up audio source
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Store initial positions
        if (player != null)
        {
            initialPlayerPosition = player.transform.position;
        }

        Camera cam = targetCamera != null ? targetCamera : Camera.main;
        if (cam != null)
        {
            initialCameraRotation = cam.transform.eulerAngles;
        }
    }

    private void Start()
    {
        StartCoroutine(ExecuteSequence());
    }

    private IEnumerator ExecuteSequence()
    {
        // Slowly move the player
        if (player != null)
        {
            Vector3 targetPosition = initialPlayerPosition + new Vector3(0, 0, zMovementAmount);
            yield return StartCoroutine(MoveObject(player.transform, targetPosition, movementDuration));
        }

        // Rotate the camera so the player can see the security camera
        Camera cam = targetCamera != null ? targetCamera : Camera.main;
        if (cam != null)
        {
            Vector3 targetRotation = initialCameraRotation - new Vector3(0, cameraRotationAmount, 0);
            yield return StartCoroutine(RotateCamera(cam.transform, targetRotation, rotationDuration));
        }

        // Play the alert sound once the player sees the camera
        if (soundEffect != null)
        {
            yield return new WaitForSeconds(soundDelayAfterRotation);
            audioSource.PlayOneShot(soundEffect);
            alert.gameObject.SetActive(true);
        }

        // Load the next scene
        yield return new WaitForSeconds(sceneLoadDelayAfterSound);
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator MoveObject(Transform objectToMove, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.position;

        while (elapsedTime < duration)
        {
            objectToMove.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // See if the player has moved far enough
        objectToMove.position = targetPosition;
    }

    private IEnumerator RotateCamera(Transform cameraTransform, Vector3 targetRotation, float duration)
    {
        float elapsedTime = 0;
        Quaternion startingRot = cameraTransform.rotation;
        Quaternion targetRot = Quaternion.Euler(targetRotation);

        while (elapsedTime < duration)
        {
            cameraTransform.rotation = Quaternion.Slerp(startingRot, targetRot, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure it has roated enough
        cameraTransform.rotation = targetRot;
    }
}