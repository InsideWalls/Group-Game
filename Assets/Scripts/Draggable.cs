using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    private RectTransform rect;
    private float centerX;
    private float centerY;
    private float centerZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        //Debug.Log(canvas.transform.position);
        centerX = canvas.transform.position.x;
        centerY = canvas.transform.position.y;
        centerZ = canvas.transform.position.z;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //Debug.Log(rect.position.x + ", " + rect.position.y);
        //*
        if (rect.position.x>centerX+30 - 2.5f)
        {
            rect.position = new Vector3(centerX + 30 - 2.5f, rect.position.y, centerZ);
        }
        if (rect.position.x<centerX-30 + 2.5f)
        {
            rect.position = new Vector3(centerX -30 + 2.5f, rect.position.y, centerZ);
        }
        if (rect.position.y >centerY+20 - 2.5f)
        {
            rect.position = new Vector3(rect.position.x, centerY +20 - 2.5f, centerZ);
        }
        if (rect.position.y<centerY-20 + 2.5f)
        {
            rect.position = new Vector3(rect.position.x, centerY - 20 + 2.5f, centerZ);
        }
        //*/

    }
}
