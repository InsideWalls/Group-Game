using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawn;
    public ParticleSystem muzzleFlash; // reference to the muzzleFlash particle system
    public PlayerMovement playerController;
    public GunSwitch gs;
    public int ammoInGun; // How many bullets are currently in the weapon
    public float fireCooldown; // Indicator of how much time is left in the firing cooldown
    

    [Header("Weapon Stats")]
    /// <summary>
    /// How many projectiles are fired per shot/click
    /// </summary>
    public int projectiles;

    /// <summary>
    /// Speed of the bullets
    /// </summary>
    public float velocity;

    /// <summary>
    /// Rounds per minute or how fast you can fire
    /// </summary>
    public float roundsPerMinute; //rpm/mintute * 1 min/60 sec

    /// <summary>
    /// Whether you can hold the button down to keep firing
    /// </summary>
    public bool fullAuto;

    /// <summary>
    /// Maximum number of bullets you can fire without having to reload
    /// </summary>
    public int magazineSize; 
    
    /// <summary>
    /// Maximum Spread Angle of Bullets in Degrees
    /// </summary>
    public float spreadAngle;

    //Input.GetMouseButtonDown(0)

    [Header("Sound")]
    public AudioClip gunshot; // --> Sounds/gunshot-effect
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponentInParent<PlayerMovement>();
        gs = GetComponentInParent<GunSwitch>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = gunshot;
        if (velocity < 0) 
        {
            velocity = 70;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reloading");
            Reload();
        }
        Fire();
        fireCooldown -= Time.deltaTime;
    }

    public void Reload()
    {
        if (playerController.ammoCount != 0 || ammoInGun != magazineSize)
        {
            Debug.Log("Now Reloading");
            int ammoToAdd = magazineSize - ammoInGun;
            if (playerController.ammoCount < ammoToAdd)
            {
                ammoInGun += playerController.ammoCount;
                playerController.ammoCount = 0;
            }
            else
            {
                playerController.ammoCount -= ammoToAdd;
                ammoInGun = magazineSize;
            }
        }
    }

    private void Fire()
    {
        if ((fullAuto && Input.GetMouseButton(0)) || Input.GetMouseButtonDown(0)) Debug.Log("pressed Left-Click");

        if (((fullAuto && Input.GetMouseButton(0)) || Input.GetMouseButtonDown(0)) && ammoInGun != 0 && fireCooldown <= 0)
        {
            if (muzzleFlash) muzzleFlash.Play(); //null check before playing the effect. It's a single line, so no brackets needed 
            ammoInGun--;
            fireCooldown = 1/roundsPerMinute*60;

            //The spread code shown here is largely inspired from the video at https://youtu.be/zHDcpvYxjJo.
            //Relevant information begins at about 8:13
            int i = 0;
            for (i = 0; i < projectiles; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, spawn.position, spawn.rotation);
                float x = Random.Range(-spreadAngle,spreadAngle);
                float y = Random.Range(-spreadAngle,spreadAngle);
                bullet.transform.parent = spawn;
                bullet.transform.localRotation = Quaternion.Euler(x,y,0);
                if (this.gameObject.name != "M870(Clone)")
                    bullet.transform.localScale = new Vector3(2, 2, 2); //the bullets were basically impossible to see
                bullet.transform.parent = null;
                bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * velocity;
            }
            audioSource.Play();            
        }
    }
}
