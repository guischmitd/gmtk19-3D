using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public Vector3 destination;
    public NavMeshAgent navMeshAgent;
    public int scheduleIndex;
    public float distanceToDestination;

    public List<GameObject> schedule;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        scheduleIndex = 0;

        navMeshAgent.SetDestination(schedule[scheduleIndex].GetComponent<Building>().entrance.transform.position);
    }

    void NextDestination()
    {
        scheduleIndex = (scheduleIndex + 1) % schedule.Count;
        navMeshAgent.SetDestination(schedule[scheduleIndex].GetComponent<Building>().entrance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToDestination = (transform.position - schedule[scheduleIndex].GetComponent<Building>().entrance.transform.position).magnitude;
        if (distanceToDestination <= .1f)
        {
            //print("Got to " + schedule[scheduleIndex].name);
            NextDestination();
        }
    }
}
