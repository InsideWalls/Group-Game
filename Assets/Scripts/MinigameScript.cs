using UnityEngine;

public class MinigameScript : MonoBehaviour
{
    public RectTransform playerSquare;

    public Canvas canvas;
    private float centerX;
    private float centerY;
    private float centerZ;
    
    void Start()
    {
        centerX = canvas.transform.position.x;
        centerY = canvas.transform.position.y;
        centerZ = canvas.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right"))
        {
            playerSquare.position += new Vector3(-5f, 0, 0);
        }
        else if (Input.GetKeyDown("left"))
        {
            playerSquare.position += new Vector3(5f, 0, 0);
        }
        else if(Input.GetKeyDown("up"))
        {
            playerSquare.position += new Vector3(0, 5f, 0);
        }
        else if (Input.GetKeyDown("down"))
        {
            playerSquare.position += new Vector3(0, -5f, 0);
        }

        playerSquare.position = keepInBounds(playerSquare);
    }

    Vector3 keepInBounds(RectTransform rect)
    {
        if (rect.position.x > centerX + 30 - 2.5f)
        {
            return new Vector3(centerX + 30 - 2.5f, rect.position.y, centerZ);
        }
        if (rect.position.x < centerX - 30 + 2.5f)
        {
            return new Vector3(centerX - 30 + 2.5f, rect.position.y, centerZ);
        }
        if (rect.position.y > centerY + 20 - 2.5f)
        {
            return new Vector3(rect.position.x, centerY + 20 - 2.5f, centerZ);
        }
        if (rect.position.y < centerY - 20 + 2.5f)
        {
            return new Vector3(rect.position.x, centerY - 20 + 2.5f, centerZ);
        }

        return rect.position;
    }
}
