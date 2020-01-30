using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class BoidMaster : MonoBehaviour {
    // Start is called before the first frame update
    public int NumBoids = 100;
    public GameObject[] Boids = new GameObject[100];
    public BoidObject[] BoidObjects = new BoidObject[100];
    public GameObject boid;
    public Transform controllerTransform;
    
    void Start() {
        var rand = new Random();
        controllerTransform = transform;
        var controllerPosition = controllerTransform.position; 
        for (var i = 0; i < NumBoids; i++)
        {
            Boids[i] = GameObject.Instantiate(boid, new Vector3(controllerPosition.x + rand.Next(-10,10), controllerPosition.y + rand.Next(-10,10), controllerPosition.z + rand.Next(-10,10)), Quaternion.LookRotation(new Vector3(rand.Next(-10, 10), rand.Next(-10, 10), rand.Next(-10, 10))));
            BoidObjects[i] = Boids[i].GetComponent<BoidObject>();
        }
    }
}
