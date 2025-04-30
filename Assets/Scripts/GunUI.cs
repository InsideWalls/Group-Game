using UnityEngine;
using TMPro;


public class GunUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI ammoCounter;
    private PlayerMovement playerController;
    private GunSwitch gs;
    private GunBehavior gun;
    public int AmmoInGun => (gs.currentWeapon < gs.weapons.Count && gs.weapons.Count != 0)? gs.weapons[gs.currentWeapon].ammoInGun : 0;
    // Every time you access this variable, it points to the GunBehavior variable.
    // Ternary Operator: (true or false)? what happens if true : what happens if false
    public bool isRobotWeapon;

    /// <summary>
    /// if the camera is atiove that means that this gun ui should be enabled
    /// </summary>
    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        if(!ammoCounter)
        {
            ammoCounter = FindFirstObjectByType<PlayerMovement>().GetComponent<GunUI>().ammoCounter;
            if(ammoCounter)
            {
                Debug.Log("Found ammo counter");
            }
            else
            {
                Debug.Log("Did not find ammo counter");
            }
        }
        if (TryGetComponent(out playerController))
        {
            Debug.Log("playerController properly instantiated");
        }
        if (TryGetComponent(out gs))
        {
            Debug.Log("gunSwitch properly instantiated");
        }
        gun = GetComponentInChildren<GunBehavior>(includeInactive: true);
        if(gun)
        {
            Debug.Log("found gun in children");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!cam.gameObject.activeInHierarchy) return;

        if (gs != null && gs.weapons.Count > 0)
        {
            //Debug.Log(((float)AmmoInGun / gs.weapons[gs.currentWeapon].magazineSize));
            ammoCounter.text = ((float)AmmoInGun / gs.weapons[gs.currentWeapon].magazineSize <= 0.25f)? // Update the Ammo Counter + Ternary Check
                // If AmmoInGun < 20% of max capacity, make the AmmoInGun text red
                $"<color=#dc2b07>{AmmoInGun}</color> \n<size=50%>{playerController.ammoCount}</size>" :
                // Display AmmoCount normally otherwise
                $"{AmmoInGun}\n<size=50%>{playerController.ammoCount}</size>";
        }
        
        if (isRobotWeapon)
        {
            //Debug.Log(((float)AmmoInGun / gs.weapons[gs.currentWeapon].magazineSize));
            ammoCounter.text = ((float)gun.ammoInGun / gun.magazineSize <= 0.25f)? // Update the Ammo Counter + Ternary Check
                // If AmmoInGun < 20% of max capacity, make the AmmoInGun text red
                $"<color=#dc2b07>{gun.ammoInGun}</color> \n<size=50%>∞</size>" :
                // Display AmmoCount normally otherwise
                $"{gun.ammoInGun}\n<size=50%>∞</size>";
        }
    }
}
