using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class GunBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawn;
    public float velocity;
    public PlayerController playerController;
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
        playerController = FindFirstObjectByType<PlayerController>();
        if (velocity < 0) 
        {
            velocity = 70;
        }
    }

    void Update()
    {
        Fire();
        fireCooldown -= Time.deltaTime;
    }

    public void Reload()
    {
        if (playerController.ammoCount != 0 || ammoInGun != magazineSize)
        {
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
        if (((fullAuto && Input.GetMouseButton(0)) || (Input.GetMouseButtonDown(0))) && ammoInGun != 0 && fireCooldown <= 0)
        {
            Debug.Log("pressed Left-Click");
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
                bullet.transform.parent = null;
                // "Quaternions came from [Sir William Rowan] Hamilton after his really good work had been done, and though beautifully ingenious, 
                // have been an unmixed evil to those who have touched them in any way."
                //          -- Lord Kelvin
                bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * velocity;
            }
        }
    }
}