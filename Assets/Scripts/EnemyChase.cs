using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class EnemyChase : MonoBehaviour 
{
    public GameObject Player;
    int MoveSpeed = 33;
    int MaxDist = 10;
    int MinDist = 5;


    private void Start()
    {
        
    }

    private void Update()
    {
        transform.LookAt(Player.transform.position);
        if (Vector3.Distance(transform.position, Player.transform.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, Player.transform.position) <= MaxDist)
            {

            }
        }
    }
}
