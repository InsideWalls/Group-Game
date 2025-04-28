using UnityEngine;
using System.Collections;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Settings")]
    public GameObject Player;
    public int MoveSpeed = 33;
    public int MaxDist = 10;
    public int MinDist = 5;

    [Header("Combat Settings")]
    public int DamageAmount = 1; // Deal 1 damage every collision
    public float BumpForce = 35f; // Force for bumping the enemy away from the player to stop player from losing all health at once
    public float BumpCooldown = 0.5f; // Stops bumping from happening repeadetly
    public float PlayerBumpForce = 3f;

    private bool canBump = true;
    private Rigidbody rb;

    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = Player.GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (Player == null) return;

        // Chase behavior
        transform.LookAt(Player.transform.position);
        if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canBump)
        {
            // Get the player's health component
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Notify player to lose health
                playerHealth.TakeDamage(DamageAmount, gameObject);

                // Apply bump
                BumpCharactersApart(collision.gameObject);

                // Start cooldown
                StartCoroutine(BumpCooldownRoutine());
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("bullet hit");


            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            playerHealth.IncrementWinCondition();
        }
    }

    private void BumpCharactersApart(GameObject player)
    {
        // Bump enemy away
        Vector3 enemyBumpDirection = (transform.position - player.transform.position).normalized;
        rb.AddForce(enemyBumpDirection * BumpForce, ForceMode.Impulse);

        // Bump player away (handled in PlayerHealth)
        if (player.TryGetComponent<Rigidbody>(out var playerRb))
        {
            Vector3 playerBumpDirection = (player.transform.position - transform.position).normalized;
            playerRb.AddForce(playerBumpDirection * PlayerBumpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator BumpCooldownRoutine()
    {
        canBump = false;
        yield return new WaitForSeconds(BumpCooldown);
        canBump = true;
    }
}