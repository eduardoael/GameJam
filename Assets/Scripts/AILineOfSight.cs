using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILineOfSight : MonoBehaviour
{

    NavMeshAgent agent;
    LayerMask mask; 
    public float viewingDistance = 10;
    AIMovement movement;


   
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mask = LayerMask.GetMask("Player"); // Layer to rycast
        movement = GetComponent<AIMovement>();
    }

  
    void Update()
    {
        Vector3 rayOrigin = transform.position + (Vector3.up * 10f);
       
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), out hit, viewingDistance, mask))
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            print("hit");
            movement.state = AIMovement.State.Alert;
        }
        else 
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * viewingDistance, Color.yellow);
        }


    }
}
