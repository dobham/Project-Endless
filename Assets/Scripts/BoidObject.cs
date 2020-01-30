using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class BoidObject : MonoBehaviour {
    public Vector3 directionCurrent;
    public Vector3 directionTarget;
    public Transform objectTransform;
    
    private BoidMaster _masterScript;
    
    private const float RotationSpeed = 0.01f;
    private const float MovementSpeed = 0.45f;

    private const int NumDirections = 300;
    private const float ViewRadius = 25;
    private const float ObstacleRadius = 20;
    private const float boidRadius = 25;

    private Vector3[] _observedDirections; //Might be subject to becoming a public variable, might add a feature where other boids will see what boids are in their arrays
    private Vector3[] _observedPositions;
    private Vector3[] _observedAvoidDirections;
    private static Vector3[] _viewDirections = new Vector3[NumDirections];

    private RaycastHit _detectedObject;
    private LayerMask boidLayer;
    private LayerMask obstacleLayer;

    private void Start() {
        _masterScript = (BoidMaster)GameObject.FindObjectOfType(typeof(BoidMaster));
        objectTransform = transform;
        
        var startingDirection = objectTransform.rotation * Vector3.forward;
        directionCurrent = startingDirection;
        directionTarget = startingDirection;
        
        _observedDirections = new Vector3[_masterScript.NumBoids];
        _observedPositions = new Vector3[_masterScript.NumBoids];
        _observedAvoidDirections = new Vector3[_masterScript.NumBoids];
        
        obstacleLayer = LayerMask.GetMask("Default");
        boidLayer = LayerMask.GetMask("Boid");
        print(obstacleLayer);
        print(boidLayer);
    }

    private void Update() {
        if (directionTarget.x > directionCurrent.x)
        {
            directionCurrent.x += RotationSpeed;
        }
        else directionCurrent.x -= RotationSpeed;
        
        if (directionTarget.y > directionCurrent.y)
        {
            directionCurrent.y += RotationSpeed;
        }
        else directionCurrent.y -= RotationSpeed;
        
        if (directionTarget.z > directionCurrent.z)
        {
            directionCurrent.z += RotationSpeed;
        }
        else directionCurrent.z -= RotationSpeed;

        objectTransform.position += directionCurrent * MovementSpeed;
        objectTransform.rotation = Quaternion.LookRotation(directionCurrent);
        int numBoidDirections = 0, numBoidPositions = 0;
        for (var i = 0; i < _masterScript.NumBoids; i++)
        { 
            if (Vector3.Distance(_masterScript.BoidObjects[i].objectTransform.position, objectTransform.position) < ViewRadius) //DIRECTIONS
            {
                _observedDirections[i] = _masterScript.BoidObjects[i].directionCurrent;
                _observedPositions[i] = _masterScript.BoidObjects[i].objectTransform.position;
                numBoidPositions++;
                numBoidDirections++;
            }
        }
        directionTarget = (AverageHeading(_observedDirections, numBoidDirections) + (AveragePosition(_observedPositions, numBoidPositions)-objectTransform.position)*0.005f + ObstacleAvoid()*0.5f).normalized*MovementSpeed;
        if (directionTarget == Vector3.zero)
        {
            directionTarget = _masterScript.controllerTransform.position - objectTransform.position;
        }
    }

    private static Vector3 AverageHeading(Vector3[] boidDirections, int numBoids) {
        var average = Vector3.zero;
        for (var i = 0; i < numBoids; i++)
        {
            average += boidDirections[i];
        }

        average /= numBoids;

        return average;
    }

    private static Vector3 AveragePosition(Vector3[] boidPositions, int numBoids) {
        Vector3 average = new Vector3();
        for (var i = 0; i < numBoids; i++)
        {
            average += boidPositions[i];
        }

        average /= numBoids;
        //Debug.DrawLine(Vector3.zero, average);
        return average;
    }

    private static Vector3[] GetDirections() {
        var goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        var angleIncrement = Mathf.PI * 2 * goldenRatio;
        
        for (var i = 0; i < NumDirections; i++)
        {
            var t = (float) i / NumDirections;
            var inclination = Mathf.Acos(1 - 2 * t);
            var azimuth = angleIncrement * i;
     
            var x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            var y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            var z = Mathf.Cos(inclination);
            _viewDirections[i] = new Vector3(x, y, z);
        }
        
        return _viewDirections;
    }
    
    public Vector3 ObstacleAvoid()
    {
        var directions = GetDirections();

        Vector3 average = Vector3.zero;
        int numObstacles=0;

        for (var i = 0; i < NumDirections; i++)
        {
            //If the detected object is a boid, ignore it and dont save the object
            
            //If it detects a wall, save the object and get away from it
            if (Physics.Raycast(objectTransform.position, directions[i] * 2, out _detectedObject, ObstacleRadius, obstacleLayer))
            {
                Debug.DrawRay(objectTransform.position, directions[i] * 4, Color.red);
                average+=directions[i].normalized*(ObstacleRadius-Vector3.Distance(objectTransform.position, _detectedObject.transform.position));
                numObstacles++;
            }
            else if (Physics.Raycast(objectTransform.position, directions[i] * 2, boidRadius, boidLayer))
            {
                Debug.DrawRay(objectTransform.position, directions[i] * 4, Color.blue);
                average += directions[i].normalized * 1f;
                numObstacles++;
            }
            //Otherwise, there are no obstacles and path is clear
            else
            {
                //Debug.DrawRay(objectTransform.position, directions[i] * 4, Color.green);
            }

            // Ray ray = new Ray(position,directions[i]);
            // if (Physics.Raycast(ray, CollisionRadius, boidLayer))
            // {
            //     Debug.DrawRay(position, directions[i]*4, Color.blue);
            // }
        }
        average /= -numObstacles;
        return average;
    }
}
