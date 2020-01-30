using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public const int NumBoids = 10; 
    private readonly GameObject[] _boids = new GameObject[NumBoids];
    public GameObject boid = new GameObject();
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            _boids[i] = GameObject.Instantiate(boid);
        }

        _boids[1].GetComponent<BoidObject>().direction = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
