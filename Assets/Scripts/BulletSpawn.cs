using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawn;
    public float velocity;
    public float fireCooldown;
    private float cooldownTime;

    public AudioClip gunshot; // --> Sounds/cecil
    private AudioSource audioSource;

    void Start()
    {
        if (velocity <= 0) 
            velocity = 70;

        cooldownTime = 0;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = gunshot;
    }

    void Update()
    {
        cooldownTime-=Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && cooldownTime <= 0)
        {
            cooldownTime = fireCooldown;
            Debug.Log("pressed 0");
            GameObject shell = Instantiate(bullet, spawn.position, spawn.rotation);
            shell.transform.localScale = new Vector3(10, 10, 10);
            shell.GetComponent<Rigidbody>().linearVelocity = spawn.forward * velocity;
            audioSource.Play();
        }
    }
}
