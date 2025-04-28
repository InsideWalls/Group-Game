using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MinigameScript : MonoBehaviour
{
    public RobotHandling robHandle;//player's RobotHandling script


    public Canvas canvas;
    private RectTransform canvasRect;
    public RectTransform playerSquare;
    public RectTransform endSquare;
    public TextMeshProUGUI timerText;

    //canvas info
    private float centerX;
    private float centerY;
    private float centerZ;
    private float leftX;
    private float rightX;
    private float topY;
    private float bottomY;

    private Vector3 startPos;

    public List<RectTransform> blocks; //set of blocks in the minigame

    private bool gameOn=false;
    public float timeLimit = 20;
    private float timeLeft;
    private int timeInt;

    public AudioClip blipSound; //--> Sounds/beep_sound
    private AudioSource audioSource;

    void Start()
    {
        centerX = canvas.transform.position.x;
        centerY = canvas.transform.position.y;
        centerZ = canvas.transform.position.z;

        startPos = playerSquare.anchoredPosition;
        canvasRect = canvas.GetComponent<RectTransform>();

        float offset = playerSquare.rect.width/2.0f;
        leftX = centerX - canvasRect.rect.width/2 + offset;
        rightX = centerX + canvasRect.rect.width/2-offset;
        topY = centerY + canvasRect.rect.height/2-offset;
        bottomY = centerY - canvasRect.rect.height/2 + offset;

        timeLeft = timeLimit;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = blipSound;
    }
    //
    void Update()
    {
        if (gameOn)
        {
            timeInt = (int)timeLeft;
            timeLeft -= Time.deltaTime;
            timerText.text = timeInt.ToString();

            if(timeInt > timeLeft)
            {
                Debug.Log(timeInt);
            }

            Vector3 lastPos = playerSquare.position;
            if (Input.GetKeyDown("right"))
            {
                playerSquare.position += new Vector3(-5f, 0, 0);
                audioSource.PlayOneShot(blipSound);
            }
            else if (Input.GetKeyDown("left"))
            {
                playerSquare.position += new Vector3(5f, 0, 0);
                audioSource.PlayOneShot(blipSound);
            }
            else if (Input.GetKeyDown("up"))
            {
                playerSquare.position += new Vector3(0, 5f, 0);
                audioSource.PlayOneShot(blipSound);
            }
            else if (Input.GetKeyDown("down"))
            {
                playerSquare.position += new Vector3(0, -5f, 0);
                audioSource.Play();
            }

            foreach (RectTransform block in blocks)
            {
                if (block.position == playerSquare.position)
                {
                    playerSquare.position = lastPos; //moves player back if they ran into a block
                    break;
                }
            }

            if (endSquare.position == playerSquare.position) //if player reaches the end
            {
                gameOn = false;
                robHandle.endOfHack(true);
            }

            playerSquare.position = keepInBounds(playerSquare);

            if (timeLeft <= 0)
            {
                Debug.Log("Time's up!");
                gameOn = false;
                robHandle.endOfHack(false);
            }
        }
    }

    Vector3 keepInBounds(RectTransform rect)
    {
        if (rect.position.x > rightX)
        {
            return new Vector3(rightX, rect.position.y, centerZ);
        }
        if (rect.position.x < leftX)
        {
            return new Vector3(leftX, rect.position.y, centerZ);
        }
        if (rect.position.y > topY)
        {
            return new Vector3(rect.position.x, topY, centerZ);
        }
        if (rect.position.y < bottomY)
        {
            return new Vector3(rect.position.x, bottomY, centerZ);
        }

        return rect.position;
    }
    
    //resets the game
    public void gameStart()
    {
        playerSquare.anchoredPosition = startPos;
        timeLeft = timeLimit;
        gameOn = true;
    }
}
