using UnityEngine;

public class RobotHandling : MonoBehaviour
{
    public PlayerMovement pControl;
    private PlayerMovement robControl;
    public GameObject playerCam;
    private GameObject robCam;

    private GameObject currentRobot;
    private bool hackable=false;
    private bool hacked=false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (hacked)
            {
                pControl.enabled=!pControl.enabled;
                robControl.enabled=!robControl.enabled;
                playerCam.SetActive(!playerCam.activeSelf);
                robCam.SetActive(!robCam.activeSelf);
                Debug.Log("switched");
            }
            else
            {
                Debug.Log("no robot");
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && hackable && !hacked)
        {
            Debug.Log("hacked!");
            hacked = true;
            robControl = currentRobot.GetComponent<PlayerMovement>();
            robCam = currentRobot.transform.Find("Main Camera").gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HackRange")
        {
            hackable = true;
            if (!hacked)
            {
                Debug.Log("Within Range");
                currentRobot = other.gameObject.transform.parent.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HackRange")
        {
            if (!hacked)
            {
                Debug.Log("Left Range");
                hackable = false;
                currentRobot = null;
            }
        }
    }
}
