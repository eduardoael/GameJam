using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILineOfSight : MonoBehaviour
{

    NavMeshAgent agent;
    LayerMask mask; 
    public float viewingDistance = 10;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mask = LayerMask.GetMask("Player"); //"Player" Layer
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = transform.position + (Vector3.up * 1.3f);
       
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), out hit, viewingDistance, mask))
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            print("hit");
        }
        else 
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * viewingDistance, Color.yellow);
        }


    }
}
