using UnityEngine;

public enum PickupType
{
    None = -1,
    AR_L = 0,
    AR_M = 1,
    AR_N = 2,
    AR_O = 3,
    AR_P = 4,
}

public class HoverAndRotate : MonoBehaviour
{
    public float hoverHeight = 0.5f;
    public float hoverSpeed = 1f;
    public float rotationSpeed = 50f;
    public PickupType pickupType;
    public bool isAmmoPickup;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}