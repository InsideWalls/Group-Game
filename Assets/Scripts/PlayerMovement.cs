using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public CharacterController controller;
    public GunSwitch gs; //references inventory script
    private Vector3 velocity;


    [Header("Inventory")]
    public List<GunBehavior> weapons; //contains player's weapons
    public int currentWeapon; //keeps track of currently-used gun
    public Transform gunPos; //reference to where gun will appear relative to player
    public GunBehavior[] guns; //set of gun prefabs for reference
    public int ammoCount;
    public int AmmoInGun => (currentWeapon < weapons.Count)? weapons[currentWeapon].ammoInGun : 0;
    // Every time you access this variable, it points to the GunBehavior variable.
    // Ternary Operator (true or false)? what happens if true : what happens if false


    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.gameObject.TryGetComponent<HoverAndRotate>(out HoverAndRotate component)) //is on each floating gun prefab
        {
            if(component.isAmmoPickup)
            {
                ammoCount += 30;
            }
            else
            {
                string str = other.gameObject.name;
                Debug.Log($"name {str}"); //Interpollated Stirng. Don't worry about using a bunch of plus signs
                
                int index = (int)component.pickupType;

                if (index >= 0 && index <= 10) //& checks both sides no matter what.
                // && will immediately returning false if the left is false. This is called "short-circuiting".
                {
                    Debug.Log(index);
                    GunBehavior weapon = Instantiate(guns[index], gunPos.position, gunPos.rotation);
                    weapon.transform.SetParent(gunPos, true);
                    // weapon.transform.localScale = new Vector3(10, 10, 10);
                    gs.AddToInventory(weapon);
                }
            }
            Destroy(other.gameObject);
        }
    }
}