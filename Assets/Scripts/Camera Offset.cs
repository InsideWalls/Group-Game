using UnityEngine;
using System.Collections;
public class CameraScript : MonoBehaviour
{
    public Transform Snake;
    public Vector3 offset;
    void Start()
    {}
    void Update()
    {
        this.transform.position = Snake.transform.position + offset;
    }}