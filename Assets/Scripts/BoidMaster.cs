using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoidMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public int NumBoids = 10;
    private const int boidsNum = 10;
    public readonly GameObject[] Boids = new GameObject[boidsNum];
    public readonly BoidObject[] BoidObjects = new BoidObject[boidsNum];
    public GameObject boid;
    
    void Start()
    {
        Random Rand = new Random();
        var controllerPosition = transform.position; 
        for (var i = 0; i < NumBoids; i++)
        {
            Boids[i] = GameObject.Instantiate(boid, new Vector3(controllerPosition.x + Rand.Next(-10,10), controllerPosition.y + Rand.Next(-10,10), controllerPosition.z + Rand.Next(-10,10)), Quaternion.identity);
            BoidObjects[i] = Boids[i].GetComponent<BoidObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
