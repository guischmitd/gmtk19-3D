using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    Vector3 start;
    public Vector3 destination;
    public NavMeshAgent navMeshAgent;
    private int scheduleIndex;

    public List<GameObject> schedule;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        scheduleIndex = 0;

        //start = new Vector3(1, .1f, 2);
        //destination = new Vector3(6, .1f, 8);

        navMeshAgent.SetDestination(schedule[scheduleIndex].transform.position);

        
    }

    void NextDestination()
    {
        scheduleIndex = scheduleIndex % schedule.Count;
        navMeshAgent.SetDestination(schedule[scheduleIndex].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - schedule[scheduleIndex].transform.position).magnitude <= 1f)
        {
            print("Got to " + schedule[scheduleIndex].name);
            NextDestination();
        }
    }
}
