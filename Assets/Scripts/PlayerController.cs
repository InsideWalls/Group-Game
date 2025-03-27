using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public CharacterController controller;
    public float runMult;
    public bool isRunning;
    public float jumpVelocity;

    [Header("Camera")]
    public float mouseSensitivity = 100f;
    public float xRotation = 0f;
    public float yRotation = 0f;
    public Camera cam;


    [Header("Inventory")]
    public List<GameObject> weapons; //contains player's weapons
    public int currentWeapon; //keeps track of currently-used gun
    public Transform gunPos; //reference to where gun will appear relative to player
    public GameObject[] guns; //set of gun prefabs for reference

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        weapons = new List<GameObject>();
        currentWeapon = 0;
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
        HandleWeapons();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpVelocity;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = !isRunning;
        }
        if(moveX == 0 && moveZ == 0)
        {
            isRunning = false;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(moveSpeed * Time.deltaTime * (isRunning? runMult : 1) * move);
        // Re-ordered operands so it only has to multiply by a vector3 once instead of twice
        // This makes performance better. Yes, It matters. In the long run, these things add up.

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    /// <summary>
    /// Switches between guns based on key-presses
    /// </summary>
    void HandleWeapons()
    {
        if(weapons.Count > 0) //checks if weapons is empty
        {
            if (Input.GetKey(KeyCode.Alpha1) & currentWeapon != 0) //checks for key & currentweapon
            {
                weapons[currentWeapon].SetActive(false);
                currentWeapon = 0;
                Debug.Log("Switched to Gun #" + (currentWeapon + 1));
            }
            if (Input.GetKey(KeyCode.Alpha2) & currentWeapon != 1 & weapons.Count > 1) //only passes if weapons size is big enough
            {
                weapons[currentWeapon].SetActive(false);
                currentWeapon = 1;
                Debug.Log("Switched to Gun #" + (currentWeapon + 1));
            }
            if (Input.GetKey(KeyCode.Alpha3) & currentWeapon != 2 & weapons.Count > 2)
            {
                weapons[currentWeapon].SetActive(false);
                currentWeapon = 2;
                Debug.Log("Switched to Gun #"+(currentWeapon+1));
            }
            weapons[currentWeapon].SetActive(true);
        }
    }

    public void AddToInventory(GameObject g)
    {
        weapons.Add(g);
        Debug.Log("added " + g.name);
        g.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.gameObject.TryGetComponent<HoverAndRotate>(out HoverAndRotate component)) //is on each floating gun prefab
        {
            string str = other.gameObject.name;
            Debug.Log($"name {str}"); //Interpollated Stirng. Don't worry about using a bunch of plus signs
            
            int index = (int)component.pickupType;

            if (index >= 0 && index <= 4) //& checks both sides no matter what.
            // && will immediately returning false if the left is false. This is called "short-circuiting".
            {
                GameObject weapon = Instantiate(guns[index], gunPos.position, gunPos.rotation);
                weapon.transform.SetParent(gunPos, true);
                // weapon.transform.localScale = new Vector3(10, 10, 10);
                AddToInventory(weapon);
                Destroy(other.gameObject);
            }
        }
    }
}