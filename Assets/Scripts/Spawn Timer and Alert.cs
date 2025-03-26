using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText;
    public AudioClip alertSound;
    public GameObject Hider;
    public TMP_Text enemyWarningText;

    private float timeRemaining = 30f;
    private bool isCountingDown = true;
    private AudioSource audioSource;
    public bool spawnEnemy = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = alertSound;

        // Show enemy warning
        if (enemyWarningText != null)
        {
            enemyWarningText.text = "Enemies Incoming!";
            enemyWarningText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (isCountingDown)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownDisplay();
            }
            else
            {
                timeRemaining = 0;
                isCountingDown = false;
                HandleCountdownEnd();
            }
        }
    }

    void UpdateCountdownDisplay()
    {
        countdownText.text = timeRemaining.ToString("0.0");
    }

    void HandleCountdownEnd()
    {
        // Hide the timer UI
        if (Hider != null)
        {
            Hider.SetActive(false);
        }

        // Play sound
        StartCoroutine(PlayAlertSoundTwice());
    }

    IEnumerator PlayAlertSoundTwice()
    {
        for (int i = 0; i < 2; i++)
        {
            audioSource.Play();
            yield return new WaitForSeconds(alertSound.length);
        }

        // Causes enemies to spawn in another script
        spawnEnemy = true;
    }
}