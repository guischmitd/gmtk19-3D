using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public GameObject cityPrefab;
    GameObject city;
    public GameObject navMeshPrefab;
    GameObject navMesh;
    public GameObject agentPrefab;
    public List<GameObject> agents;
    public int nAgents;

    // Start is called before the first frame update
    void Start()
    {
        city = Instantiate(cityPrefab);
        City cityController = city.GetComponent<City>();
        cityController.nRows = 3;
        cityController.nCols = 3;
        cityController.GenerateBuildings(2, 3, 4);

        navMesh = Instantiate(navMeshPrefab);
        navMesh.transform.parent = city.transform;

        navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();

        int nDestinations = 5;

        for (int i = 0; i < nAgents; i++)
        {
            // Instantiate an agent
            GameObject agent = Instantiate(agentPrefab);

            agent.transform.position = new Vector3(Random.Range(0, Mathf.RoundToInt(city.transform.localScale.x)), agent.transform.position.y, Random.Range(0, Mathf.RoundToInt(city.transform.localScale.z)));

            // Add buildings to the agent's schedule
            for (int j = 0; j < nDestinations; j++)
            {
                int index = Random.Range(0, cityController.buildings.Count);
                agent.GetComponent<AgentController>().schedule.Add(cityController.buildings[index]);
            }
            // Add the agent to the list of created agents
            agents.Add(agent);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    Destroy(city.gameObject);
        //    city = Instantiate(cityPrefab);
        //    city.GetComponent<City>().rows = 3;
        //    city.GetComponent<City>().cols = 3;
        //    city.GetComponent<City>().GenerateBuildings(3, 3, 3);

        //    navMesh = Instantiate(navMeshPrefab);
        //    navMesh.transform.parent = city.transform;
        //    navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
        //}
    }
}
