using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoidObject : MonoBehaviour
{
    public Vector3 directionCurrent;
    public Vector3 directionTarget;
    public float rotationSpeed = 0.01f;
    public float movementSpeed = 15f;
    
    private Transform objectTransform;
    public  GameObject controller;
    private GameObject[] _boids;
    private BoidMaster _masterScript;
    
    public Vector3[] _observedDirections = new Vector3[10];
    public Vector3[] _observedPositions = new Vector3[10];
    
    private float _viewRadius = 100;
    private float _obstacleRadius = 45;

    private void Start()
    {
        Random rand = new Random();
        objectTransform = transform;
        directionCurrent = objectTransform.rotation * Vector3.forward;
        directionTarget = objectTransform.rotation * Vector3.forward;
        // controller.GetComponent<BoidMaster>().getObject(1);
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
        int numBoidDirections=0,numBoidPositions=0;
        // for (var i = 0; i < _masterScript.NumBoids; i++)
        // {
        // if (Vector3.Distance(_masterScript.BoidObjects[i].objectTransform.position, objectTransform.position) < _viewRadius)
            // {
            //     _observedDirections[i] = _masterScript.BoidObjects[i].directionCurrent;
            //     numBoidDirections++;
            // }
            // if (Vector3.Distance(_masterScript.BoidObjects[i].objectTransform.position, objectTransform.position) < _viewRadius)
            // {
            //     _observedPositions[i] = _masterScript.BoidObjects[i].objectTransform.position;
            //     numBoidPositions++;
            // }
        // }
        directionTarget = AverageHeading(_observedDirections, numBoidDirections) /*+ AveragePosition(_observedPositions, numBoidPositions)*/;
    }

    public Vector3 AverageHeading(Vector3[] boidDirections, int numBoids)
    {
        var average = Vector3.zero;
        for (var i = 0; i < numBoids; i++)
        {
            average += boidDirections[i];
        }

        average /= numBoids;

        return average;
    }
    
    public Vector3 AveragePosition(Vector3[] boidPositions, int numBoids)
    {
        Vector3 average = new Vector3();
        for (var i = 0; i < numBoids; i++)
        {
            average += boidPositions[i];
        }

        average /= numBoids;

        return average;
    }
    
}
