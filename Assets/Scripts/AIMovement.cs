using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Alert,
        Wait,
    }

    NavMeshAgent agent;
    public State state;
    
    [Tooltip("How long is the AI-Agent alerted?")]
    public float alertTime = 1;

    //rayast for line of sight
    LayerMask mask;
    public float viewingDistance = 10;
    bool playerDetected = true;

    public Transform[] waypoints;
    private int nextWaypoint = 0;
     
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        //starting state of agent
        state = State.Patrol;
        GoToNextWaypoint();
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Alert:
                StartCoroutine(Alert());
                break;
            case State.Wait:
                break;
            default:
                Debug.Log("Not in any valid State");
                break;
        }  
    }

    void Patrol()
    {
        agent.isStopped = false;
        if (PlayerInSight()) 
        { 
            state = State.Alert; 
        }
        CheckForWaypoint();
    }

    IEnumerator Alert()
    {
        print("alerted");
        state = State.Wait;
        agent.isStopped = true;
        yield return new WaitForSeconds(alertTime);
        print("Waited");
        //player still in sight?
        if (PlayerInSight())
        {
            GameOver();
        }
        else
        {
            state = State.Patrol;
        }
        
    }


    private void CheckForWaypoint()
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

    private bool PlayerInSight()
    {
        Vector3 rayOrigin = transform.position + (Vector3.up * 10f);

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), out hit, viewingDistance))
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            print("hit " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag != "Player")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * viewingDistance, Color.yellow);
            return false;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
