using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoidObject : MonoBehaviour
{
    public Vector3 directionCurrent;
    public Vector3 directionTarget;
    public Vector3 position;
    public float rotationSpeed = 0.01f;
    public float movementSpeed = 0.01f; 
    private Transform objectTransform;
    public static GameObject Controller;
    private GameObject[] _boids;
    private readonly BoidMaster _materScript = Controller.GetComponent<BoidMaster>();

    private Vector3[] _observedPositions;
    
    private float _viewRadius = 30;
    private float _obstacleRadius = 45;

    private void Start()
    {
        Random rand = new Random();
        objectTransform = transform;
        directionCurrent = new Vector3(rand.Next(-10,10), rand.Next(-10,10), rand.Next(-10,10));
        directionTarget = new Vector3(rand.Next(-10,10), rand.Next(-10,10), rand.Next(-10,10));
        _boids = _materScript.Boids;
    }

    private void Update()
    {
        if (directionTarget.x > directionCurrent.x)
        {
            directionCurrent.x += rotationSpeed;
        }
        else directionCurrent.x -= rotationSpeed;
        
        if (directionTarget.y > directionCurrent.y)
        {
            directionCurrent.y += rotationSpeed;
        }
        else directionCurrent.y -= rotationSpeed;
        
        if (directionTarget.z > directionCurrent.z)
        {
            directionCurrent.z += rotationSpeed;
        }
        else directionCurrent.z -= rotationSpeed;
        
        objectTransform.position += directionCurrent * movementSpeed;
        objectTransform.rotation = Quaternion.LookRotation(directionCurrent);

        for (var i = 0; i < _materScript.NumBoids; i++)
        {
            if (Vector3.Distance(_materScript.BoidObjects[i].position, position) < _viewRadius)
            {
                _observedPositions[i] = _materScript.BoidObjects[i].position;
            }
        }
        AverageHeading(_observedPositions, _materScript.NumBoids);
    }

    public void AverageHeading(Vector3[] boidPositions, int numBoids)
    {
        Vector3 average = new Vector3();
        for (var i = 0; i < numBoids; i++)
        {
            average += boidPositions[i];
        }

        average /= numBoids;
        
        directionTarget = average;
    }
    
}
