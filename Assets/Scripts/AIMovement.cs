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

    public GameController gameController;

    AudioSource audioData;
    public AudioClip alertAudioFile;

    NavMeshAgent agent;
    public State state;

    [Tooltip("How long is the AI-Agent alerted?")]
    public float alertTime = 1;

    //rayast for line of sight
    LayerMask mask;
    public float viewingDistance = 10;
    //bool playerDetected = true;

    public Transform[] waypoints;
    private int nextWaypoint = 0;

    //animations
    [Header("Animation")]
    public float walkspeed;
    //private float idlespeed = 0;
    Animator anim;

    void Start()
    {
        
        audioData = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Collision");
            gameController.GameOver();
        }
    }
   
    void Patrol()
    {
        anim.SetFloat("Forward", walkspeed);
        agent.isStopped = false;
        if (PlayerInSight())
        {
            state = State.Alert;
        }
        CheckForWaypoint();
    }

    IEnumerator Alert()
    {
        audioData.PlayOneShot(alertAudioFile);
        print("alerted");
        //Wait on detection
        //state = State.Wait;
        //agent.isStopped = true;
        //anim.SetFloat("Forward", idlespeed);
        //yield return new WaitForSeconds(alertTime);
        //print("Waited");
        ////player still in sight?
        //if (PlayerInSight())
        //{
        //    GameOver();
        //}
        //else
        //{
        //    state = State.Patrol;
        //}
        //instant game over on detection
        gameController.GameOver();
        yield return null;
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
      
        float x = -1;
        for (int i = 0; i < 10; i++)
        {
            x += 0.2f;
            Vector3 offset = new Vector3(x, 0, 0);
            Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward + offset).normalized * viewingDistance, Color.yellow);
        }

        x = -1;
        for (int i = 0; i < 10; i++)
        {
            x += 0.2f;
            Vector3 offset = new Vector3(x, 0, 0);
            if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward + offset).normalized, out hit, viewingDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                { 
                    Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward + offset) * viewingDistance, Color.green);
                    return true;
                }
            }
        }
        //Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * viewingDistance, Color.yellow);
        //Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward + new Vector3(1, 0, 0)) * viewingDistance, Color.yellow);
        //Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward + new Vector3(-1, 0, 0)) * viewingDistance, Color.yellow);
        return false;

    }

   
}
