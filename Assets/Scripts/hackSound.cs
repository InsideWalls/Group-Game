using UnityEngine;

public class hackSound : MonoBehaviour
{
    public AudioClip failSound; //--> Sounds/beep_warning
    public AudioClip successSound; //--> Sounds/transformers-sound
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void playSound(bool wasHacked)
    {
        if (!wasHacked)
            audioSource.PlayOneShot(failSound);
        else
            audioSource.PlayOneShot(successSound);
    }
}
