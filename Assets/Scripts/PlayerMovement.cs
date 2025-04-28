using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public CharacterController controller;
    public GunSwitch gs; //references inventory script
    public Transform gunPos; //reference to where gun will appear relative to player
    public GameObject[] guns; //set of gun prefabs for reference

    private Vector3 velocity;
    

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
        if (other.gameObject.tag == "GunPickup") //is on each floating gun prefab
        {
            string str = other.gameObject.name;
            Debug.Log("name "+str);
            int index = -1;
            if (str.Equals("AR_L"))
                index = 0;
            else if (str.Equals("AR_M"))
                index = 1;
            else if (str.Equals("AR_N"))
                index = 2;
            else if (str.Equals("AR_O"))
                index = 3;
            else if (str.Equals("AR_P"))
                index = 4;

            if(index>=0 & index <= 4)
            {
                GameObject weapon = Instantiate(guns[index], gunPos.position, gunPos.rotation);
                weapon.transform.SetParent(gunPos, true);
                weapon.transform.localScale = new Vector3(10, 10, 10);
                gs.AddToInventory(weapon);
                Destroy(other.gameObject);
            }
        }
    }
}