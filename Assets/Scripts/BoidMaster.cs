using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoidMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public int NumBoids = 10;
    public GameObject[] Boids = new GameObject[10];
    public BoidObject[] BoidObjects = new BoidObject[10];
    public GameObject boid;
    
    void Start()
    {
        Random Rand = new Random();
        var controllerPosition = transform.position; 
        for (var i = 0; i < NumBoids; i++)
        {
            Boids[i] = GameObject.Instantiate(boid, new Vector3(controllerPosition.x + Rand.Next(-10,10), controllerPosition.y + Rand.Next(-10,10), controllerPosition.z + Rand.Next(-10,10)), Quaternion.LookRotation(new Vector3(Rand.Next(-10, 10), Rand.Next(-10, 10), Rand.Next(-10, 10))));
            BoidObjects[i] = Boids[i].GetComponent<BoidObject>();
            // print(Boids[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        getObject(1);
    }

    public GameObject getObject(int i)
    {
        print(Boids[i]);
        return Boids[i];
    }
}
