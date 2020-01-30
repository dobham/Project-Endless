using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidObject : MonoBehaviour
{
    private static readonly Random Rand = new Random();
    
    public Vector3 directionCurrent;
    public Vector3 directionTarget;
    public Vector3 position;
    public float rotationSpeed = 0.01f;
    private Transform objectTransform;
    
    private float _viewRadius = 30;
    private float _obstacleRadius = 45;

    private void Start()
    {
        objectTransform = transform;
        directionCurrent = Vector3.up;
        directionTarget = Vector3.left;
        
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

        objectTransform.position += directionCurrent;
        objectTransform.rotation = Quaternion.LookRotation(directionCurrent);
    }
}
