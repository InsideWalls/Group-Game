using UnityEngine;

public class GunSpotTest : MonoBehaviour
{
    public GameObject obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(obj, this.transform);
    }
}
