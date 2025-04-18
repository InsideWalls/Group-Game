using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotHandling : MonoBehaviour
{
    public PlayerMovement pControl;
    private PlayerMovement robControl;
    public GameObject playerCam;
    private GameObject robCam;
    
    private GameObject newRobot;
    public List<GameObject> robots;
    private int nextRobotIndex;
    private int currentRobotIndex;

    void Start()
    {
        Debug.Log(pControl.enabled + " | " + playerCam.activeSelf);
        nextRobotIndex = 0;
        currentRobotIndex = 0;
    }

    void Update()
    {
        if (robots.Count >0)
        {
            robControl = robots[currentRobotIndex].GetComponent<PlayerMovement>();
            robCam = robots[currentRobotIndex].transform.Find("Main Camera").gameObject;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (robots.Count>0)
            {
                pControl.enabled=!pControl.enabled;
                playerCam.SetActive(!playerCam.activeSelf);
                robControl.enabled=!robControl.enabled;                
                robCam.SetActive(!robCam.activeSelf);
                Debug.Log("switched");
            }
            else
            {
                Debug.Log("no robot");
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && newRobot != null)
        {
            newRobot.tag = "HackedRobot";
            newRobot.transform.Find("hackRange").gameObject.SetActive(false);
            Debug.Log("hacked! robot was set to the " + (nextRobotIndex+1) + " key");

            robots.Add(newRobot);
            nextRobotIndex++;
            newRobot = null;
        }
    }


    //*
    void FixedUpdate()
    {
        if (pControl.enabled)
        {
            if (Input.GetKey(KeyCode.Alpha1) & robots.Count>0)
            {
                currentRobotIndex=0;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
            }
            if (Input.GetKey(KeyCode.Alpha2) & robots.Count > 1)
            {
                currentRobotIndex=1;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
            }
            if (Input.GetKey(KeyCode.Alpha3) & robots.Count > 2)
            {
                currentRobotIndex=2;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
            }
            if (Input.GetKey(KeyCode.Alpha4) & robots.Count > 3)
            {
                currentRobotIndex=3;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
            }
            if (Input.GetKey(KeyCode.Alpha5) & robots.Count > 4)
            {
                currentRobotIndex=4;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
            }
            if (Input.GetKey(KeyCode.Alpha6) & robots.Count > 5)
            {
                currentRobotIndex=5;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
            }
            if (Input.GetKey(KeyCode.Alpha7) & robots.Count > 6)
            {
                currentRobotIndex=6;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
            }
        }
    }//*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HackRange")
        {
            Debug.Log("Within Range");
            //hackable = true;
            newRobot = other.gameObject.transform.parent.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HackRange")
        {
            Debug.Log("Left Range");
            //hackable = false;
            newRobot = null;
        }
    }
}
