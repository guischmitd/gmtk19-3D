using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float sizeX;
    public float sizeY;
    public float sizeZ;
    public string type;
    public bool collided;
    public GameObject entrance;

    public GameObject[] agentsInside;

    public void Build(Vector2 pos, float roadSize)
    {
        gameObject.transform.position = new Vector3(pos.x + sizeX / 2, sizeY / 2, pos.y + sizeZ / 2);
        gameObject.transform.localScale = new Vector3(sizeX - roadSize, sizeY, sizeZ - roadSize);
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

    //    void OnCollisionEnter(Collision collision)
    //    {
    //        if (type != "park" && type != "house")
    //        {
    //            string otherType = collision.gameObject.GetComponent<Building>().type;
    //            if (otherType == "park" || otherType == "house")
    //            {
    //                print("Destroyed" + collision.gameObject.name);
    //                GameObject.FindGameObjectWithTag("City").GetComponent<City>().buildings.Remove(collision.gameObject);
    //                Destroy(collision.gameObject);
    //            }
    //        }

    //    }
}
