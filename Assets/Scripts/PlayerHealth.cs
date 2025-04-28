using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int currentHealth = 5;
    public float bumpForce = 2f;

    [Header("Audio")]
    public AudioClip damageSound;
    private AudioSource audioSource;

    [Header("Hearts")]
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject Heart4;
    public GameObject Heart5;

    public int winCondition = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Debug.Log("Player health: " + currentHealth);
        allhearts();
    }

    public void availablehearts()
    {
        if (currentHealth == 4)
        {
            Heart5.SetActive(false);
            PlayDamageSound();
        }
        if (currentHealth == 3)
        {
            Heart4.SetActive(false);
            PlayDamageSound();
        }
        if (currentHealth == 2)
        {
            Heart3.SetActive(false);
            PlayDamageSound();
        }
        if (currentHealth == 1)
        {
            Heart2.SetActive(false);
            PlayDamageSound();
        }
    }

    private void PlayDamageSound()
    {
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }
public void allhearts()
    {
        Heart1.SetActive(true);
        Heart2.SetActive(true);
        Heart3.SetActive(true);
        Heart4.SetActive(true);
        Heart5.SetActive(true);
    }
    public void TakeDamage(int damageAmount, GameObject damageSource)
    {
        int previousHealth = currentHealth;
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);

        Debug.Log("Player health changed from " + previousHealth + " to " + currentHealth);

        availablehearts();

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
        SceneManager.LoadScene("Death");
    }


     

    public void IncrementWinCondition()
    {
        winCondition++;
        Debug.Log("Win Condition: " + winCondition + " out of 6");
        if (winCondition == 6)
        {
            SceneManager.LoadScene("Win");
        }   
    }
}