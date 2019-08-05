using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    Vector3 start;
    public Vector3 destination;
    public NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        start = new Vector3(1, .1f, 2);
        destination = new Vector3(6, .1f, 8);
        navMeshAgent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - destination).magnitude <= 0.1f)
        {
            print("Halfway through!");
            navMeshAgent.SetDestination(start);
        } else if ((transform.position - start).magnitude <= 0.1f) {
            print("Another lap!");
            navMeshAgent.SetDestination(destination);
        }
    }
}
