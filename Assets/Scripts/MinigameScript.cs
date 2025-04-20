using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinigameScript : MonoBehaviour
{
    public RobotHandling robHandle;

    public Canvas canvas;
    private RectTransform canvasRect;
    public RectTransform playerSquare;
    public RectTransform endSquare;

    private float centerX;
    private float centerY;
    private float centerZ;
    private float leftX;
    private float rightX;
    private float topY;
    private float bottomY;

    private Vector3 startPos;

    public List<RectTransform> blocks;

    private bool gameOn=false;
    public float timeLimit = 15;
    private float timeLeft;
    private int timeInt;
    
    void Start()
    {
        centerX = canvas.transform.position.x;
        centerY = canvas.transform.position.y;
        centerZ = canvas.transform.position.z;

        //Debug.Log(centerX + ", " + centerY+", "+centerZ);
        startPos = playerSquare.anchoredPosition;
        //Debug.Log("start: " + startPos.x + ", " + startPos.y + ", " + startPos.z);
        //startPos = new Vector3(playerSquare.rect.x, playerSquare.rect.y, 0);

        canvasRect = canvas.GetComponent<RectTransform>();
        //Debug.Log(canvasRect.position);
        //Debug.Log(canvasRect.rect.position);
        float offset = playerSquare.rect.width/2.0f;
        leftX = centerX - canvasRect.rect.width/2 + offset;
        rightX = centerX + canvasRect.rect.width/2-offset;
        topY = centerY + canvasRect.rect.height/2-offset;
        bottomY = centerY - canvasRect.rect.height/2 + offset;
        //Debug.Log(leftX + ", " + rightX + ", " + bottomY + ", " + topY);
        timeLeft = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn)
        {
            timeInt = (int)timeLeft;
            timeLeft -= Time.deltaTime;

            if(timeInt > timeLeft)
            {
                Debug.Log(timeInt);
            }

            Vector3 lastPos = playerSquare.position;
            if (Input.GetKeyDown("right"))
            {
                playerSquare.position += new Vector3(-5f, 0, 0);
            }
            else if (Input.GetKeyDown("left"))
            {
                playerSquare.position += new Vector3(5f, 0, 0);
            }
            else if (Input.GetKeyDown("up"))
            {
                playerSquare.position += new Vector3(0, 5f, 0);
            }
            else if (Input.GetKeyDown("down"))
            {
                playerSquare.position += new Vector3(0, -5f, 0);
            }

            foreach (RectTransform block in blocks)
            {
                if (block.position == playerSquare.position)
                {
                    playerSquare.position = lastPos; break;
                }
            }

            if (endSquare.position == playerSquare.position)
            {
                //Debug.Log("you did it :)"); 
                gameOn = false;
                robHandle.endOfHack(true);
            }

            playerSquare.position = keepInBounds(playerSquare);

            if (timeLeft <= 0)
            {
                Debug.Log("Time's up!");
                Debug.Log(timeLeft);
                gameOn = false;
                robHandle.endOfHack(false);
            }
        }
    }

    //*
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
    //*/
    public void gameStart()
    {
        //Debug.Log("start: " + startPos.x + ", " + startPos.y + ", " + startPos.z);
        playerSquare.anchoredPosition = startPos;
        timeLeft = timeLimit;
        gameOn = true;
    }
}
