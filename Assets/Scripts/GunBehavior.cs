using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawn;
    public float velocity;
    public PlayerMovement playerController;
    public int ammoInGun;
    public float fireCooldown; //rpm/mintute * 1 min/60 sec
    //private Random rand = new Random();
    
    public ParticleSystem muzzleFlash;

    [Header("Weapon Stats")]
    public int projectiles;
    public float roundsPerMinute; //Rounds per minute
    public bool fullAuto;
    public int magazineSize;
    
    /// <summary>
    /// Maximum Spread Angle of Bullets in Degrees
    /// </summary>
    public float spreadAngle;
    //Input.GetMouseButtonDown(0)
    void Start()
    {
        if (velocity <= 0) 
            velocity = 70;
    }

    void Update()
    {
        Fire();
        fireCooldown -= Time.deltaTime;
    }

    void Fire()
    {
        if (((fullAuto && Input.GetMouseButton(0)) || Input.GetMouseButtonDown(0)) && ammoInGun != 0 && fireCooldown <= 0)
        {
            Debug.Log("pressed Left-Click");
            if (muzzleFlash) muzzleFlash.Play(); //null check before playing the effect. It's a single line, so no brackets needed 
            ammoInGun--;
            fireCooldown = 1/roundsPerMinute*60;

            int i = 0;
            for (i = 0; i < projectiles; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, spawn.position, spawn.rotation);
                //bullet.transform.localScale = new Vector3(20,20,20);
                float x = Random.Range(-spreadAngle,spreadAngle);
                float y = Random.Range(-spreadAngle,spreadAngle);
                bullet.transform.parent = spawn;
                bullet.transform.localRotation = Quaternion.Euler(x,y,0);
                bullet.transform.parent = null;
                bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * velocity;
            }
        }
    }
}
