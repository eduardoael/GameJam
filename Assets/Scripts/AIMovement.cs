using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;
    
    public Transform[] waypoints;
    private int nextWaypoint = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < .4f)
        {
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.Log("No waypoints set for" + this.gameObject.name);
            return;
        }

        //set new waypoint
        agent.destination = waypoints[nextWaypoint].position;

        //find next waypoint   
        nextWaypoint = (nextWaypoint + 1) % waypoints.Length;
        
    }
}
