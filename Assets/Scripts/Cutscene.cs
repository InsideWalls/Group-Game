using UnityEngine;
using UnityEngine.SceneManagement;

public class WakeUpSequence : MonoBehaviour
{
    [SerializeField] private Transform objectToRotate;
    [SerializeField] private AudioClip Alert;
    [SerializeField] private GameObject hiddenAlert;
    [SerializeField] private float soundVolume = 1f;

    [SerializeField] private float rotationDuration = 3f;
    [SerializeField] private float targetYRotation = 140f;
    [SerializeField] private float showObjectDelay = 0.1f;

    private float rotationProgress = -370f;
    private bool actionsTriggered = false;

    private void Start()
    {

        if (objectToRotate != null)
        {
            objectToRotate.rotation = Quaternion.Euler(objectToRotate.eulerAngles.x,0f,objectToRotate.eulerAngles.z);
        }

        Invoke("ChangeScene", rotationDuration);
    }

    private void Update()
    {
        if (rotationProgress < 1f && objectToRotate != null)
        {
            rotationProgress += Time.deltaTime / rotationDuration;
            rotationProgress = Mathf.Clamp01(rotationProgress);

            float currentYRotation = Mathf.Lerp(0f, targetYRotation, rotationProgress);
            objectToRotate.rotation = Quaternion.Euler(objectToRotate.eulerAngles.x,currentYRotation,objectToRotate.eulerAngles.z);

            if (rotationProgress >= 1f && !actionsTriggered)
            {
                actionsTriggered = true;
                TriggerEndActions();
            }
        }
    }

    private void TriggerEndActions()
    {
        if (hiddenAlert != null)
        {
            hiddenAlert.SetActive(true);
        }

        PlayAlert();
    }

    private void PlayAlert()
    {
        if (Alert != null)
        {
            AudioSource.PlayClipAtPoint(Alert,Camera.main.transform.position,soundVolume);
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Main Level Scene");
    }
}