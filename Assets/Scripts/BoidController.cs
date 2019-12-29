using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            _boidSwarm[i].velocity += _boids[i].collisionAvoidForce * Time.deltaTime;
            _boidSwarm[i].velocity = _boids[i].Direction * _boids[i].Speed;
            _boids[i].Position = _boidSwarm[i].position;
            _boids[i].Velocity = _boidSwarm[i].velocity;
            _boidSwarm[i].transform.LookAt(_boids[i].Direction);
            
            for (var k = 0; k < NumBoids; k++)
            {
                //If there is an obstacle, cycle through all directions it can view and find one that doesnt crash into it
                //Then set the acceleration to that and add it to the velocity above
                //After all of this is done, in the for loop below, take the average "avoidance" heading that all boids will have obtained and make it its own heading
                if (Boid.CollisionDetected(_boids[k].Position))
                {
                    print("AAAAA");
                    Vector3 collisionAvoidDirection = _boids[k].ClearPath(_boids[k].Position);
                    _boids[i].collisionAvoidForce = _boids[i].Steer(collisionAvoidDirection);
                    _boids[i].Direction = collisionAvoidDirection;
                }
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
                    _boids[i].Status[k] = 1;
                }
                else
                {
                    _boids[i].Direction = _boids[i].Direction;
                    _boids[i].Status[k] = 0;
                }
            }
            _boids[i].ObstacleAvoid(_boids[i].Position, _boids[i].CollisionDirections(), _boidLayer);
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
    public static float CollisionRadius = 15;

    public float Speed = 10;
    public Vector3 Velocity;
    public Vector3 collisionAvoidForce = Vector3.zero;
    private int SteerForce = 10;
    public Vector3 Position = new Vector3(Rand.Next(5, 15), Rand.Next(5, 15), Rand.Next(5, 15));
    public Vector3 Direction = new Vector3(Rand.Next(1, 5), Rand.Next(-5, 10), Rand.Next(1, 10));

    private static readonly float GoldenRatio = (1 + Mathf.Sqrt(5)) / 2;
    private static readonly float AngleIncrement = Mathf.PI * 2 * GoldenRatio;
    private const int NumViewDirections = 300;
    private RaycastHit detectedObject;
    public Vector3[] Directions = new Vector3[NumViewDirections];

    //Function for finding average heading  : FUNCTION 1
    //Cycle through all BOIDS, if one is in the viewRadius, add their heading value to an array
    //Take said array and average all of the values and make it its own heading value
    public static Vector3 AverageHeading(Boid[] boidArray)
    {
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

        for (var i = 0; i < arrLength; i++)
        {
            headings[i] = validDirections[i];
        }

        for (var i = 0; i < arrLength; i++)
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
        for (var i = 0; i < NumViewDirections; i++)
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
        }
    }


    public static bool CollisionDetected(Vector3 position)
    {
        return Physics.Raycast(position, Vector3.forward, CollisionRadius);
    }

    public Vector3 ClearPath(Vector3 position)
    {
        var collisionsDirections = CollisionDirections();
        foreach (var dir in collisionsDirections)
        {
            var ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, CollisionRadius))
            {
                return dir;
            }
        }

        return Direction;
    }

    public Vector3 Steer(Vector3 vector)
    {
        var v = vector.normalized * Speed - Velocity;
        return Vector3.ClampMagnitude (v, SteerForce); 
    }

/*
Vector3 SteerTowards (Vector3 vector) {
    Vector3 v = vector.normalized * settings.maxSpeed - velocity;
    return Vector3.ClampMagnitude (v, settings.maxSteerForce);
}
 */
}
