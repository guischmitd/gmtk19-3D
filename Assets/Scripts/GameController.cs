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

    public int citySizeX;
    public int citySizeZ;
    public int nHospitals;
    public int nSchools;
    public int nMalls;

    // Start is called before the first frame update
    void Start()
    {
        city = Instantiate(cityPrefab);
        City cityController = city.GetComponent<City>();
        cityController.nCols = citySizeX;
        cityController.nRows = citySizeZ;
        cityController.GenerateBuildings(nHospitals, nSchools, nMalls);

        navMesh = Instantiate(navMeshPrefab, city.transform);

        navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();

        int nDestinations = 5;

        for (int i = 0; i < nAgents; i++)
        {
            // Instantiate an agent
            GameObject agent = Instantiate(agentPrefab);
            Vector3 pos = new Vector3(Random.Range(0, Mathf.RoundToInt(city.transform.localScale.x)), agent.transform.position.y, Random.Range(0, Mathf.RoundToInt(city.transform.localScale.z)));
            agent.transform.position = pos;
            //print(agent.transform.position);

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
        
    }
}
