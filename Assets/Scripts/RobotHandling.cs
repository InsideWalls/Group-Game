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

    public MinigameScript hackGame;
    public GameObject hackCam;

    public List<GameObject> robotUI;
    public List<GameObject> circleUI;

    void Start()
    {
        hackGame.enabled = true;
        hackCam.SetActive(false);
        //Debug.Log(pControl.enabled + " | " + playerCam.activeSelf);
        nextRobotIndex = 0;
        currentRobotIndex = 0;

        hideui();
        hidecircle();
    }

    public void hideui()
    {
        foreach(GameObject robUI in robotUI)
        {
            robUI.SetActive(false);
        }
    }
    public void hidecircle()
    {
        foreach (GameObject circUI in circleUI)
        {
            circUI.SetActive(false);
        }
    }

    public void showui(int robIndex)
    {
        robotUI[robIndex].SetActive(true);
    }

    void Update()
    {
        if (pControl.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) & robots.Count > 0)
            {
                currentRobotIndex = 0;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) & robots.Count > 1)
            {
                currentRobotIndex = 1;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) & robots.Count > 2)
            {
                currentRobotIndex = 2;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) & robots.Count > 3)
            {
                currentRobotIndex = 3;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) & robots.Count > 4)
            {
                currentRobotIndex = 4;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) & robots.Count > 5)
            {
                currentRobotIndex = 5;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) & robots.Count > 6)
            {
                currentRobotIndex = 6;
                Debug.Log("current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
        }

        if (robots.Count >0)
        {
            robControl = robots[currentRobotIndex].GetComponent<PlayerMovement>();
            robCam = robots[currentRobotIndex].transform.Find("Main Camera").gameObject;
        }

        if (Input.GetKeyDown(KeyCode.R) && !hackCam.activeSelf)
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

        if (Input.GetKeyDown(KeyCode.F) && newRobot != null && pControl.enabled)
        {
            //*
            hackCam.SetActive(true);
            pControl.enabled = false;
            playerCam.SetActive(false);
            hackGame.gameStart();
        }
    }

    public void endOfHack(bool success)
    {
        if(success)
        {
            newRobot.tag = "HackedRobot";
            newRobot.transform.Find("hackRange").gameObject.SetActive(false);
            Debug.Log("hacked! robot was set to the " + (nextRobotIndex + 1) + " key");

            robots.Add(newRobot);
            showui(nextRobotIndex);
            nextRobotIndex++;
            newRobot = null;
        }
        else
        {
            Debug.Log("failed the hack");
        }

        hackCam.SetActive(false);
        pControl.enabled = true;
        playerCam.SetActive(true);
    }

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
