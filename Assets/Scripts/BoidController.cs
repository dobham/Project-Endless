using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class BoidController : MonoBehaviour
{
    private const int NumBoids = 10;
    private Vector3 _averageHeading;
    private LayerMask _obstacle;
    public Boid[] boids = new Boid[NumBoids];
    public Rigidbody boidModel;
    public Rigidbody[] boidSwarm = new Rigidbody[NumBoids];
    void Start()
    {
        var rand = new Random();
        //Spawn a bunch of BOIDS
        for (var i = 0; i < NumBoids; i++)
        {
            boids[i] = new Boid();
            boidSwarm[i] = Instantiate(boidModel, transform.forward, Quaternion.Euler(boids[i].angleX, boids[i].angleY, boids[i].angleZ));
            boidSwarm[i].useGravity = false;
        }
    }
    
    //TO CHANGE ANGLE X, ADD TO DIRECTION ANGLE Z
    //TO CHANGE ANGLE Y, ADD TO DIRECTION ANGLE Y
    void Update()
    {
        for (var i = 0; i < NumBoids; i++)
        {
            //                              Angle of the object                                             //Vector multiplied by the speed
            boidSwarm[i].velocity = Quaternion.Euler(boids[i].vectorAngleX, boids[i].vectorAngleY, boids[i].vectorAngleZ)  * boids[i].Direction * boids[i].speed;
            // boids[i].Direction = boidSwarm[i].velocity;
            boids[i].Position = boidSwarm[i].position;
            boidSwarm[i].rotation = Quaternion.Euler(boids[i].angleX, boids[i].angleY, boids[i].angleZ);
            print(boids[i].Direction);
            
            for (var k = 0; k < NumBoids; k++)
            {
                //Cycle through all boids, if x y and z values of the boids are within the index i boid's radius, add them to an array,
                //then get the average heading of all of them, apply that heading to the current boid
                if (k == i) continue;
                if (((boids[k].Position.x - boids[i].Position.x) * (boids[k].Position.x - boids[i].Position.x)) +
                    ((boids[k].Position.y - boids[i].Position.y) * (boids[k].Position.y - boids[i].Position.y)) +
                    ((boids[k].Position.z - boids[i].Position.z) * (boids[k].Position.z - boids[i].Position.z)) <
                    boids[i].ViewRadius) //( x-cx ) ^2 + (y-cy) ^2 + (z-cz) ^ 2 < r^2 LIES INSIDE SPHERE
                {
                    boids[i].ObservedBoids[k] = boids[k];
                }
            }
            boids[i].Direction = Boid.AverageHeading(boids[i].ObservedBoids);
        }
    }
}
public class Boid : Application {
    //Initialize the basics for each BOID
    public float AlignmentForce = 1;
    public float AvoidForce = 1;
    public float CollisionForce = 10;
    public readonly Boid[] ObservedBoids = new Boid[10];
    public Vector3 Position = new Vector3(0,0,0);
    public float ViewRadius = 5;
    public const float AvoidRadius = 2;
    public const float CollisionRadius = 5;
    private static readonly float GoldenRatio = (1 + Mathf.Sqrt (5)) / 2;
    private static readonly float AngleIncrement = Mathf.PI * 2 * GoldenRatio;
    public Vector3[] Directions;
    private const int NumViewDirections = 300;
    public Vector3 Direction = Vector3.forward;
    public readonly float angleX = -90;
    public readonly float angleY = -0;
    public readonly float angleZ = -90;
    public readonly float vectorAngleX = 0;
    public readonly float vectorAngleY = 0;
    public readonly float vectorAngleZ = 0;
    public readonly float speed = 5;
    
    //Function for finding average heading  : FUNCTION 1
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
    //Cycle through all BOIDS, if one is in the viewRadius, add their heading value to an array
    //Take said array and average all of the values and make it its own heading value
    public void CollisionDirections () {
        Vector3[] directions = new Vector3[NumViewDirections];
        for (var i = 0; i < NumViewDirections; i++) {
            var t = (float) i / NumViewDirections;
            var inclination = Mathf.Acos (1 - 2 * t);
            var azimuth = AngleIncrement * i;

            var x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
            var y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
            var z = Mathf.Cos (inclination);
            directions[i] = new Vector3 (x, y, z);
        }
    }
    //Function for BOID avoidance  : FUNCTION 2
    public void BoidAvoid(Boid[] boidArray) {
        
    }
    //Cycle through all BOIDS, if one is in the avoidRadius, change heading and speed accordingly

    //Function for obstacle avoidance  : FUNCTION 3
    public void ObstacleAvoid() {
        
    }
    //Cast out rays to locate obstacle, if obstacle is near, change heading and speed
    
    
}
