using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public const int NumBoids = 10; 
    private readonly GameObject[] _boids = new GameObject[NumBoids];
    private readonly BoidObject[] _boidScripts = new BoidObject[NumBoids];
    public GameObject boid;
    
    void Start()
    {
        for (int i = 0; i < NumBoids; i++)
        {
            _boids[i] = GameObject.Instantiate(boid);
            _boidScripts[i] = _boids[i].GetComponent<BoidObject>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
