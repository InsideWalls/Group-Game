using UnityEngine;

public class hackSound : MonoBehaviour
{
    public AudioClip failSound;
    //public AudioClip successSound;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = failSound;
    }

    //https://stackoverflow.com/questions/70132609/unity-c-sharp-i-want-to-play-multiple-audio-sources-on-the-same-game-object-in
    public void playSound(bool wasHacked)
    {
        if (!wasHacked)
            audioSource.PlayOneShot(failSound);
        //else
        //  audioSource.PlayOneShot(successSound);
    }
}
