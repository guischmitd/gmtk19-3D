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
    public List<Vector2> occupied;

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

        for (int i = 0; i < nCols; i++)
        {
            for (int j = 0; j < nRows; j++)
            {
                if (buildingPool[instantiatedBuildings].name != "empty")
                {
                    GameObject building = Instantiate(buildingPool[instantiatedBuildings]);
                    Vector2 pos = new Vector2(i * 3, j * 3);
                    building.GetComponent<Building>().Build(pos, roadSize);

                    for (int x = 0; x < building.GetComponent<Building>().sizeX; x++)
                    {
                        for (int y = 0; y < building.GetComponent<Building>().sizeZ; y++)
                        {
                            occupied.Add(pos + new Vector2(x, y));
                        }
                    }
                    
                    building.name = buildingPool[instantiatedBuildings].name + " x" + i.ToString() + " y" + j.ToString();
                    building.GetComponent<Building>().Build(new Vector2 (i * 3, j * 3), roadSize);

                    building.transform.parent = gameObject.transform;
                    buildings.Add(building);
                }
                instantiatedBuildings++;
            }
        }

        // Filling up remaining spaces
        for (int i = 0; i < blocksX; i++)
        {
            for (int j = 0; j < blocksY; j++)
            {
                Vector2 pos = new Vector2(i, j);
                if (!occupied.Contains(pos))
                {
                    GameObject building = Instantiate(fillerBuildings[Random.Range(0, 2)]);
                    building.GetComponent<Building>().Build(pos, roadSize);

                    building.transform.parent = gameObject.transform;
                    buildings.Add(building);
                }       
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
