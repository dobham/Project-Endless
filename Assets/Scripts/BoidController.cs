using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class BoidController : MonoBehaviour
{
    public const int NumBoids = 10;
    private Vector3 _averageHeading;
    private LayerMask _obstacle;
    private readonly Boid[] _boids = new Boid[NumBoids];
    public Rigidbody boidModel;
    private readonly Rigidbody[] _boidSwarm = new Rigidbody[NumBoids];
    private LayerMask _boidLayer;
    public Vector3 averageHeading = new Vector3();
    
    void Start()
    {
        var rand = new Random();
        //Have the boids be on a separate layer for the raycast's to ignore
        _boidLayer = LayerMask.GetMask("Ignore Raycast");
        //Spawn a bunch of BOIDS
        for (var i = 0; i < NumBoids; i++)
        {
            _boids[i] = new Boid();
            _boidSwarm[i] = Instantiate(boidModel, _boids[i].Position, Quaternion.LookRotation(_boids[i].Direction, Vector3.up));
        }
    }
    
    void Update()
    {
        for (var i = 0; i < NumBoids; i++)
        {
            _boidSwarm[i].velocity = _boids[i].Direction * Boid.Speed;
            _boids[i].Position = _boidSwarm[i].position;
            _boidSwarm[i].transform.LookAt(_boids[i].Position +_boids[i].Direction);
            for (var k = 0; k < NumBoids; k++)
            {
                //Cycle through all boids, if x y and z values of the boids are within the index i boid's radius, add them to an array,
                //then get the average heading of all of them, apply that heading to the current boid
                if (k == i) continue;
                if (((_boids[k].Position.x - _boids[i].Position.x) * (_boids[k].Position.x - _boids[i].Position.x)) +
                    ((_boids[k].Position.y - _boids[i].Position.y) * (_boids[k].Position.y - _boids[i].Position.y)) +
                    ((_boids[k].Position.z - _boids[i].Position.z) * (_boids[k].Position.z - _boids[i].Position.z)) <
                    Boid.ViewRadius) //( x-cx ) ^2 + (y-cy) ^2 + (z-cz) ^ 2 < r^2 LIES INSIDE SPHERE
                {
                    _boids[i].ObservedBoids[k] = _boids[k];
                    _boids[i].Direction = Boid.AverageHeading(_boids[i].ObservedBoids);
                }
                else
                {
                    _boids[i].Direction = Vector3.forward;
                    _boids[i].Status[k] = 0;
                }
            }
            // print(_boids[i].Direction);
            _boids[i].ObstacleAvoid(_boids[i].Position, _boids[i].CollisionDirections(), _boidLayer);
            if (_boids[i].DetectObstacle()) {
                // _boids[i].Direction = _boids[i].ClearPath(_boids[i].Position, _boidLayer);
                averageHeading = Vector3.up;
            }
        }
        
        Debug.DrawLine(new Vector3(0,0,0), _boids[0].Position*1.5f, Color.white);
    }
}

public class Boid : Application
{
    private static readonly Random Rand = new Random();

    public int[] Status = new int[BoidController.NumBoids];

    public readonly Boid[] ObservedBoids = new Boid[10];
    public const float ViewRadius = 15;
    public readonly float CollisionRadius = 15;

    public Vector3 Position = new Vector3(Rand.Next(5, 15), Rand.Next(5, 15), Rand.Next(5, 15));
    public const float Speed = 10;
    public Vector3 Direction = new Vector3(Rand.Next(5, 8), Rand.Next(5, 8), Rand.Next(5, 8));


    private static readonly float GoldenRatio = (1 + Mathf.Sqrt(5)) / 2;
    private static readonly float AngleIncrement = Mathf.PI * 2 * GoldenRatio;
    private const int NumViewDirections = 300;
    private RaycastHit detectedObject;
    public Vector3[] Directions = new Vector3[NumViewDirections];

    //Function for finding average heading  : FUNCTION 1
    //Cycle through all BOIDS, if one is in the viewRadius, add their heading value to an array
    //Take said array and average all of the values and make it its own heading value
    public static Vector3 AverageHeading(Boid[] boidArray) {
        var headingSum = new Vector3();
        var arrLength = boidArray.Count(t1 => t1 != null); //Gets the size of the non null array of boids
        var headings = new Vector3[boidArray.Length];
        var validDirections = new Vector3[boidArray.Length];
        for (var t = 0; t < boidArray.Length; t++)
        {
            if (boidArray[t] != null)
            {
                validDirections[t] = boidArray[t].Direction;
            }
        }
        for (var i = 0 ; i < arrLength; i++)
        {
            headings[i] = validDirections[i];
        }
        for (var i = 0 ; i < arrLength; i++)
        {
            headingSum += headings[i];
        }
        var averageHeading = headingSum / arrLength;
        return averageHeading.normalized;
    }

    public Vector3[] CollisionDirections()
         {
             for (var i = 0; i < NumViewDirections; i++)
             {
                 var t = (float) i / NumViewDirections;
                 var inclination = Mathf.Acos(1 - 2 * t);
                 var azimuth = AngleIncrement * i;
     
                 var x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                 var y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
                 var z = Mathf.Cos(inclination);
                 Directions[i] = new Vector3(x, y, z);
             }
     
             return Directions;
         }

    //Function for obstacle avoidance  : FUNCTION 2
    //Cast out rays to locate obstacle, if obstacle is near, change heading and speed
    public void ObstacleAvoid(Vector3 position, Vector3[] directions, LayerMask boidLayer)
    {
        for (int i = 0; i < NumViewDirections; i++)
        {
            //If the detected object is a boid, ignore it and dont save the object
            if (Physics.Raycast(position, directions[i] * 4, CollisionRadius, boidLayer))
            {
                Debug.DrawRay(position, directions[i] * 4, Color.green);
            }
            //If it detects a wall, save the object and get away from it
            else if (Physics.Raycast(position, directions[i] * 4, out detectedObject, CollisionRadius))
            {
                Debug.DrawRay(position, directions[i] * 4, Color.red);
            }
            //Otherwise, there are no obstacles and path is clear
            else
            {
                Debug.DrawRay(position, directions[i] * 4, Color.green);
            }

            // Ray ray = new Ray(position,directions[i]);
            // if (Physics.Raycast(ray, CollisionRadius, boidLayer))
            // {
            //     Debug.DrawRay(position, directions[i]*4, Color.blue);
            // }
        }
    }

    public bool DetectObstacle()
    {
        return Physics.Raycast(Position, Vector3.forward, CollisionRadius);
    }

    public Vector3 ClearPath(Vector3 position, LayerMask boidLayer) {
        for (int i = 0; i < NumViewDirections; i++)
        {
            Ray ray = new Ray(position,Directions[i]);
            if (Physics.Raycast(ray, CollisionRadius, boidLayer))
            {
                Debug.DrawRay(position, Directions[i]*4, Color.blue);
                return Directions[i];
            }
        }
        return Direction;
    }
}
