using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public float nRows;
    public float nCols;

    public GameObject schoolPrefab;
    public GameObject hospitalPrefab;
    public GameObject mallPrefab;

    public GameObject parkPrefab;
    public GameObject housePrefab;
    public GameObject emptyPrefab;

    public List<GameObject> buildings;

    public float roadSize;

    static void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void GenerateBuildings(int nHospitals, int nSchools, int nMalls)
    {
        float blocksY = nRows * 3;
        float blocksX = nCols * 3;

        gameObject.transform.localScale = new Vector3(blocksX + roadSize, 0.25f, blocksY + roadSize);
        gameObject.transform.localPosition = new Vector3(blocksX / 2, -0.25f / 2, blocksY / 2);

        List<GameObject> buildingPool = new List<GameObject>();
        List<GameObject> fillerBuildings = new List<GameObject>();
        fillerBuildings.Add(housePrefab);
        fillerBuildings.Add(parkPrefab);

        int totalBuildings = nHospitals + nSchools + nMalls;
        int emptyBlocks = (int)(nRows * nCols) - totalBuildings;

        for (int i = 0; i < (int)(nRows * nCols); i++)
        {
            if (i < nHospitals)
            {
                buildingPool.Add(hospitalPrefab);
            }
            else if (i < nHospitals + nSchools)
            {
                buildingPool.Add(schoolPrefab);
            }
            else if (i < totalBuildings)
            {
                buildingPool.Add(mallPrefab);
            }
            else
            {
                buildingPool.Add(emptyPrefab);
            }
        }

        ShuffleList(buildingPool);

        // Instantiate Major buildings (1 per block)
        int instantiatedBuildings = 0;

        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < nCols; j++)
            {
                if (buildingPool[instantiatedBuildings].name != "empty")
                {
                    GameObject building = Instantiate(buildingPool[instantiatedBuildings]);
                    building.name = buildingPool[instantiatedBuildings].name + i.ToString() + j.ToString();
                    building.GetComponent<Building>().Build(new Vector2 (j * 3, i * 3), roadSize);

                    building.transform.parent = gameObject.transform;
                    buildings.Add(building);
                }
                instantiatedBuildings++;
            }
        }

        // Filling up remaining spaces
        for (int i = 0; i < nRows * 3; i++)
        {
            for (int j = 0; j < nCols * 3; j++)
            {
                GameObject building = Instantiate(fillerBuildings[Random.Range(0, 2)]);
                building.GetComponent<Building>().Build(new Vector2(j, i), roadSize);

                building.transform.parent = gameObject.transform;
                buildings.Add(building);
            }
        }
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        foreach (var building in buildings)
        {
            Destroy(building);
        }
    }
}
