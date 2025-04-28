using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawn;
    public float velocity;

    public AudioClip gunshot; // --> Sounds/gunshot-effect
    private AudioSource audioSource;

    void Start()
    {
        if (velocity <= 0) 
            velocity = 70;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = gunshot;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("pressed 0");
            GameObject shell = Instantiate(bullet, spawn.position, spawn.rotation);
            shell.transform.localScale = new Vector3(20, 20, 20);
            shell.GetComponent<Rigidbody>().linearVelocity = spawn.forward * velocity;
            audioSource.Play();
        }
    }
}
