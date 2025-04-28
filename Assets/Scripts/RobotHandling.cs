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

    //public MinigameScript hackGame;
    public GameObject hackCam;

    public GameObject robot1;
    public GameObject robot2;
    public GameObject robot3;
    public GameObject robot4;
    public GameObject robot5;
    public GameObject robot6;
    public GameObject robot7;


    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject circle4;
    public GameObject circle5;
    public GameObject circle6;
    public GameObject circle7;

    public void hideui()
    {
        robot1.SetActive(false);
        robot2.SetActive(false);
        robot3.SetActive(false);
        robot4.SetActive(false);
        robot5.SetActive(false);
        robot6.SetActive(false);
        robot7.SetActive(false);
    }
    public void hidecircle()
    {
        circle1.SetActive(false);
        circle2.SetActive(false);
        circle3.SetActive(false);
        circle4.SetActive(false);
        circle5.SetActive(false);
        circle6.SetActive(false);
        circle7.SetActive(false);
    }

    public void showui(int nextRobotIndex)
    {
        if (nextRobotIndex == 1)
        {
            robot1.SetActive(true);
        }
        if (nextRobotIndex == 2)
        {
            robot2.SetActive(true);
        }
        if (nextRobotIndex == 3)
        {
            robot3.SetActive(true);
        }
        if (nextRobotIndex == 4)
        {
            robot4.SetActive(true);
        }
        if (nextRobotIndex == 5)
        {
            robot5.SetActive(true);
        }
        if (nextRobotIndex == 6)
        {
            robot6.SetActive(true);
        }
        if (nextRobotIndex == 7)
        {
            robot7.SetActive(true);
        }
    }

    void Start()
    {
        //hackGame.enabled = false;
        //hackCam.SetActive(false);
        Debug.Log(pControl.enabled + " | " + playerCam.activeSelf);
        nextRobotIndex = 0;
        currentRobotIndex = 0;

        hideui();
        hidecircle();
    }

    void Update()
    {
        if (robots.Count > 0)
        {
            robControl = robots[currentRobotIndex].GetComponent<PlayerMovement>();
            robCam = robots[currentRobotIndex].transform.Find("Main Camera").gameObject;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (robots.Count > 0)
            {
                pControl.enabled = !pControl.enabled;
                playerCam.SetActive(!playerCam.activeSelf);
                robControl.enabled = !robControl.enabled;
                robCam.SetActive(!robCam.activeSelf);
                Debug.Log("switched");
            }
            else
            {
                Debug.Log("no robot");
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && newRobot != null && pControl.enabled)
        {
            newRobot.tag = "HackedRobot";
            newRobot.transform.Find("hackRange").gameObject.SetActive(false);
            Debug.Log("hacked! robot was set to the " + (nextRobotIndex + 1) + " key");

            robots.Add(newRobot);
            nextRobotIndex++;
            newRobot = null;

            showui(nextRobotIndex);
        }

            if (Input.GetKeyDown(KeyCode.E))
        {
            //hackGame.enabled = true;
            hackCam.SetActive(true);
            pControl.enabled = false;
            playerCam.SetActive(false);
        }
    }


    //*
    void FixedUpdate()
    {
        if (pControl.enabled)
        {
            if (Input.GetKey(KeyCode.Alpha1) & robots.Count > 0)
            {
                currentRobotIndex = 0;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circle1.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Alpha2) & robots.Count > 1)
            {
                currentRobotIndex = 1;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circle2.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Alpha3) & robots.Count > 2)
            {
                currentRobotIndex = 2;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circle3.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Alpha4) & robots.Count > 3)
            {
                currentRobotIndex = 3;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circle4.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Alpha5) & robots.Count > 4)
            {
                currentRobotIndex = 4;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circle5.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Alpha6) & robots.Count > 5)
            {
                currentRobotIndex = 5;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circle6.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Alpha7) & robots.Count > 6)
            {
                currentRobotIndex = 6;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circle7.SetActive(true);
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