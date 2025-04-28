using UnityEngine;

public class RobotSound : MonoBehaviour
{
    public AudioClip failSound; //--> Sounds/beep_warning
    public AudioClip successSound; //--> Sounds/transformers-sound
    public AudioClip switchSound;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void playSound(string sound)
    {
        if (sound=="failHack")
            audioSource.PlayOneShot(failSound);
        if (sound=="succeedHack")
            audioSource.PlayOneShot(successSound);
        if (sound=="switchRobot")
            audioSource.PlayOneShot(switchSound);
    }
}
