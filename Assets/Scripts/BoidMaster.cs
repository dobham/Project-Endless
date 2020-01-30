using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoidMaster : MonoBehaviour
{
    // Start is called before the first frame update
    private const int NumBoids = 10; 
    private readonly GameObject[] _boids = new GameObject[NumBoids];
    private readonly BoidObject[] _boidScripts = new BoidObject[NumBoids];
    public GameObject boid;
    
    void Start()
    {
        Random Rand = new Random();
        var controllerPosition = transform.position; 
        for (var i = 0; i < NumBoids; i++)
        {
            _boids[i] = GameObject.Instantiate(boid, new Vector3(controllerPosition.x + Rand.Next(-10,10), controllerPosition.y + Rand.Next(-10,10), controllerPosition.z + Rand.Next(-10,10)), Quaternion.identity);
            _boidScripts[i] = _boids[i].GetComponent<BoidObject>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
