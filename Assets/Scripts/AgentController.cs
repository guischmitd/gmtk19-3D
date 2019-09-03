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
    public bool isInside;

    public List<GameObject> schedule;
    public List<float> scheduleTime;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        scheduleIndex = 0;

        navMeshAgent.SetDestination(schedule[scheduleIndex].GetComponent<Building>().entrance.transform.position);
    }

    IEnumerator Enter(GameObject building)
    {
        navMeshAgent.enabled = false;
        transform.position = building.transform.position;
        print("Started countdown at " + Time.time.ToString());
        yield return new WaitForSeconds(scheduleTime[scheduleIndex]);
        print("Finished countdown at " + Time.time.ToString());
        transform.position = building.GetComponent<Building>().entrance.transform.position;
        navMeshAgent.enabled = true;
        NextDestination();
        isInside = false;
    }

    public void Enter(Building building)
    {

    }

    public void NextDestination()
    {
        scheduleIndex = (scheduleIndex + 1) % schedule.Count;
        navMeshAgent.SetDestination(schedule[scheduleIndex].GetComponent<Building>().entrance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToDestination = (transform.position - schedule[scheduleIndex].GetComponent<Building>().entrance.transform.position).magnitude;
        if (distanceToDestination <= .15f && !isInside)
        {
            isInside = true;
            StartCoroutine(Enter(schedule[scheduleIndex]));
        }
    }
}
