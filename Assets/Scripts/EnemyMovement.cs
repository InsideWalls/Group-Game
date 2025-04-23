using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    private bool canMove;
    private float maxSpeed;
    private NavMeshAgent agent;
    private List<Vector3> destinationPoints = new List<Vector3>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Transform _dpParent = this.transform.Find("DestinationPoints");
        if (_dpParent != null)
        {
            canMove = true;
            foreach (Transform _pos in _dpParent)
            {
                Vector3 destinationOption = _pos.position;
                destinationPoints.Add(destinationOption);
            }
            Debug.Log(destinationPoints.Count);
        } else canMove = false;
    
    }

    void Update()
    {
        
    }
}
