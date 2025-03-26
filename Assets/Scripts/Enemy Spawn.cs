using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public CountdownTimer timerScript; // Reference the timer script
    public GameObject enemyPrefab;
    private bool hasSpawned = false; // CHheck for only one spawn (Temporarly only doing 1 spawn for testing)

    // Enemy position
    private Vector3 spawnPosition = new Vector3(279f, 52.58f, 285f);

    void Update()
    {
        // See if timer is done and if an enemy has been spawned
        if (timerScript != null && timerScript.spawnEnemy && !hasSpawned)
        {
            SpawnEnemy();
            hasSpawned = true;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }}}