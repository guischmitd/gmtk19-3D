﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        city = Instantiate(cityPrefab);
        city.GetComponent<City>().rows = 5;
        city.GetComponent<City>().cols = 3;
        city.GetComponent<City>().GenerateBuildings(5, 3, 7);

        navMesh = Instantiate(navMeshPrefab);
        navMesh.transform.parent = city.transform;

        navMesh.GetComponent<NavMeshSurface>().BuildNavMesh();

        GameObject agent = Instantiate(agentPrefab);
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