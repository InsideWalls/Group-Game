using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunSwitch : MonoBehaviour
{
    private List<GameObject> weapons; //contains player's weapons
    private int currentWeapon; //keeps track of currently-used gun
    
    void Start()
    {
        weapons = new List<GameObject>();
        currentWeapon = 0; 
    }

    public void AddToInventory(GameObject g)
    {
        weapons.Add(g);
        Debug.Log("added " + g.name);
        g.SetActive(false);
    }

    //switches between guns based on key-presses
    void FixedUpdate()
    {
        if(weapons.Count > 0) //checks if weapons is empty
        {
            if (Input.GetKey(KeyCode.Alpha1) && currentWeapon != 0) //checks for key & currentweapon
            {
                weapons[currentWeapon].SetActive(false);
                currentWeapon = 0;
                Debug.Log("Switched to Gun #" + (currentWeapon + 1));
            }
            if (Input.GetKey(KeyCode.Alpha2) && currentWeapon != 1 && weapons.Count > 1) //only passes if weapons size is big enough
            {
                weapons[currentWeapon].SetActive(false);
                currentWeapon = 1;
                Debug.Log("Switched to Gun #" + (currentWeapon + 1));
            }
            if (Input.GetKey(KeyCode.Alpha3) && currentWeapon != 2 & weapons.Count > 2)
            {
                weapons[currentWeapon].SetActive(false);
                currentWeapon = 2;
                Debug.Log("Switched to Gun #"+(currentWeapon+1));
            }
            weapons[currentWeapon].SetActive(true);
        }
    }
}
