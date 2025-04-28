using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotHandling : MonoBehaviour
{
    public PlayerMovement pControl; //player's PlayerMovement
    private PlayerMovement robControl; //this robot's PlayerMovement
    public GameObject playerCam; //player's Camera object
    private GameObject robCam; //this robot's Camera object
    private RobotSound robSound; //plays when switching

    public float switchCooldown = 1f;
    private float switchTime;

    private GameObject newRobot; //robot that is currently within hacking range
    public List<GameObject> robots; //player's available robots
    private int nextRobotIndex;
    private int currentRobotIndex;

    public MinigameScript hackGame; //References minigame sequence
    public GameObject hackCam; //minigame's Camera object

    //references UI images in Canvas
    public List<GameObject> robotUI;
    public List<GameObject> circleUI;

    //Used in handling robot switching
    private float scrollChange;
    private int scrollVal;
    public float scrollScale=0.5f;

    void Start()
    {
        hackGame.enabled = true;
        hackCam.SetActive(false);
        nextRobotIndex = 0;
        currentRobotIndex = 0;

        switchTime = 0;

        scrollChange = 0f;
        scrollVal = 0;

        hideui();
        hidecircle();
    }

    //hides robot images
    public void hideui()
    {
        foreach(GameObject robUI in robotUI)
        {
            robUI.SetActive(false);
        }
    }
    //hides circle images
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
        //only runs if player is active, i.e., the minigame or a robot isn't
        if (pControl.enabled && robots.Count !=0)
        {
            scrollChange += Input.mouseScrollDelta.y * scrollScale; //Stores total change in scroll distance
            if (scrollChange >= 1f || scrollChange <= -1f) //if scrollChange reaches 1 it increments scrollVal, resets scrollChange
            {
                scrollVal += (int)scrollChange;
                if (scrollVal > robots.Count - 1)
                    scrollVal = robots.Count - 1;
                else if (scrollVal < 0)
                    scrollVal = 0;

                scrollChange = 0f;
            }

            //current robot is set based on scrollVal
            if (scrollVal != currentRobotIndex)
            {
                currentRobotIndex = scrollVal;
                Debug.Log("Current robot: #" + (currentRobotIndex + 1));
                hidecircle();
                circleUI[currentRobotIndex].SetActive(true);
            }
        }

        if (robots.Count >0)
        {
            //sets references to current robot stuff
            robControl = robots[currentRobotIndex].GetComponent<PlayerMovement>();
            robCam = robots[currentRobotIndex].transform.Find("Main Camera").gameObject;
            robSound = robots[currentRobotIndex].GetComponent<RobotSound>();
        }

        if (switchTime > 0)
        {
            switchTime -= Time.deltaTime;
            if (switchTime <= 0)
                Debug.Log("you can now switch");
        }

        //swaps between robots and player
        if (Input.GetKeyDown(KeyCode.R) && !hackCam.activeSelf && switchTime <=0)
        {
            if (robots.Count>0)
            {
                //switches player's things off & current robot's things on,
                //or vice versa
                pControl.enabled=!pControl.enabled;
                playerCam.SetActive(!playerCam.activeSelf);
                robControl.enabled=!robControl.enabled;                
                robCam.SetActive(!robCam.activeSelf);
                robSound.playSound("switchRobot");
                switchTime = switchCooldown;
                Debug.Log("Switched");
            }
            else
            {
                Debug.Log("No robots found");
            }
        }

        //Hacking
        if (Input.GetKeyDown(KeyCode.F) && newRobot != null && pControl.enabled)
        {
            float p = Random.Range(0.0f, 1.0f);
            Debug.Log("p="+p);
            if (p > .7f) //30% chance for hack to trigger
            {
                Debug.Log("minigame triggered");
                hackCam.SetActive(true);
                pControl.enabled = false;
                playerCam.SetActive(false);
                hackGame.gameStart();
            }
            else
            {
                endOfHack(true);
            }
            
        }
    }

    public void endOfHack(bool success)
    {
        if (success)
        {
            //Sets various components for newRobot as appropriate
            newRobot.tag = "HackedRobot";
            newRobot.GetComponent<RobotSound>().playSound("succeedHack");
            newRobot.transform.Find("hackRange").gameObject.SetActive(false); //disables hack trigger
            Debug.Log("hacked!");

            robots.Add(newRobot);
            showui(nextRobotIndex);
            nextRobotIndex++;
            newRobot = null;
        }
        else
        {
            Debug.Log("failed the hack");
            newRobot.GetComponent<RobotSound>().playSound("failHack");
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
            newRobot = other.gameObject.transform.parent.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HackRange")
        {
            Debug.Log("Left Range");
            newRobot = null;
        }
    }
}
