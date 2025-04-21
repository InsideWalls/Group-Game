using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int currentHealth = 5;
    public float bumpForce = 2f;

    private void Start()
    {
        Debug.Log("Player health: " + currentHealth);
    }

    public void TakeDamage(int damageAmount, GameObject damageSource)
    {
        int previousHealth = currentHealth;
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);

        Debug.Log("Player health changed from " + previousHealth + " to " + currentHealth);

        // Push the enemy away
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            Vector3 direction = (transform.position - damageSource.transform.position).normalized;
            rb.AddForce(direction * bumpForce, ForceMode.Impulse);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Dead!");
        // Need to make death screen and switch to it here
    }

    // Test taking damage
    public void TestDamage()
    {
        TakeDamage(1, gameObject); // Test with 1 damage
    }
}