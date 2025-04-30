using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

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

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;
    public GameObject Enemy6;

    public GameObject Enemy7;
    public GameObject Enemy8;
    public GameObject Enemy9;
    public GameObject Enemy10;
    public GameObject Enemy11;
    public GameObject Enemy12;

    public bool wave2 = false;
    public bool wave1 = true;
    public bool wave25 = false;
    private float wavespawn;

    private bool enemy2Spawned = false;
    private float enemy2SpawnTime;

    private bool enemy4Spawned = false;
    private float enemy4SpawnTime;



    void Start()
    {
        Enemy1.SetActive(false);
        Enemy2.SetActive(false);
        Enemy3.SetActive(false);
        Enemy4.SetActive(false);
        Enemy5.SetActive(false);
        Enemy6.SetActive(false);
        Enemy7.SetActive(false);
        Enemy8.SetActive(false);
        Enemy9.SetActive(false);
        Enemy10.SetActive(false);
        Enemy11.SetActive(false);
        Enemy12.SetActive(false);


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
        if (enemy2Spawned && Time.time - enemy2SpawnTime >= 10f)
        {
            enemy2Spawned = false; // Make it only run once
            Enemy3.SetActive(true);
            Enemy4.SetActive(true);

            enemy4Spawned = true;
            enemy4SpawnTime = Time.time;
        }
        if (enemy4Spawned && Time.time - enemy2SpawnTime >= 20f)
        {
            Enemy5.SetActive(true);
            Enemy6.SetActive(true);
            enemy4Spawned = false;

            isCountingDown = true;
            timeRemaining = 15f;
            Hider.SetActive(true);

            wave1 = false;
            wave2 = true;
        }
        if (wave25 && Time.time - wavespawn >= 10)
        {
            wave25 = false;

            Enemy10.SetActive(true);
            Enemy11.SetActive(true);
            Enemy12.SetActive(true);
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
        StartCoroutine(PlayAlertSound());
    }

    IEnumerator PlayAlertSound()
    {

        audioSource.Play();
        yield return new WaitForSeconds(alertSound.length);

        if (wave1 == true)
        {
            Enemy1.SetActive(true);
            Enemy2.SetActive(true);

            enemy2Spawned = true;
            enemy2SpawnTime = Time.time;
        }
        if (wave2 == true) { 
            Enemy7.SetActive(true);
            Enemy8.SetActive(true);
            Enemy9.SetActive(true);

            wave25 = true;
            wavespawn = Time.time;
        }

        // Causes enemies to spawn in another script
        //spawnEnemy = true;
    }
}