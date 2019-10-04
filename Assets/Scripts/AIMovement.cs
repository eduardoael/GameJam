using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    private int nextWaypoint;
    

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < .5f)
        {
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }
        
        //set new waypoint
        agent.destination = waypoints[nextWaypoint].position;
       
        //find next waypoint           
        if (nextWaypoint <= waypoints.Length)
        {
            nextWaypoint++;
        }
        else
        {
            nextWaypoint = 0;
        }
    }
}
