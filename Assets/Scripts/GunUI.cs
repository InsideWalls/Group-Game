using UnityEngine;
using TMPro;


public class GunUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI ammoCounter;
    private PlayerMovement playerController;
    private GunSwitch gs;
    public int AmmoInGun => (gs.currentWeapon < gs.weapons.Count)? gs.weapons[gs.currentWeapon].ammoInGun : 0;
    // Every time you access this variable, it points to the GunBehavior variable.
    // Ternary Operator: (true or false)? what happens if true : what happens if false

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        if (TryGetComponent(out PlayerMovement playerMovement))
        {
            playerController = playerMovement;
            Debug.Log("playerController properly instantiated");
        }
        if (TryGetComponent(out GunSwitch gunSwitch))
        {
            gs = gunSwitch;
            Debug.Log("gunSwitch properly instantiated");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(((float)AmmoInGun / gs.weapons[gs.currentWeapon].magazineSize));
        ammoCounter.text = ((float)AmmoInGun / gs.weapons[gs.currentWeapon].magazineSize <= 0.25f)? // Update the Ammo Counter + Ternary Check
            // If AmmoInGun < 20% of max capacity, make the AmmoInGun text red
            $"<color=#dc2b07>{AmmoInGun}</color> \n<size=50%>{playerController.ammoCount}</size>" :
            // Display AmmoCount normally otherwise
            $"{AmmoInGun}\n<size=50%>{playerController.ammoCount}</size>";
    }
}
