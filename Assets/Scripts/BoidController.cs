using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public static int NumBoids = 1;
    private Vector3 _averageHeading;
    private LayerMask _obstacle;
    public Boid[] boids = new Boid[NumBoids];
    public Rigidbody boidModel;
    public Rigidbody[] boidSwarm = new Rigidbody[NumBoids];
    void Start()
    {

        //Spawn a bunch of BOIDS
        for (int i = 0; i < NumBoids; i++)
        {
            boids[i] = new Boid();
            boidSwarm[i] = Instantiate(boidModel, transform.forward, Quaternion.Euler(boids[i].angleX,boids[i].angleY,0));
            boidSwarm[i].useGravity = false;
        }
    }
    
    //TO CHANGE ANGLE X, ADD TO DIRECTION ANGLE Z
    //TO CHANGE ANGLE Y, ADD TO DIRECTION ANGLE Y
    void Update()
    {
        for (int i = 0; i < NumBoids; i++)
        {
            boidSwarm[i].velocity = Quaternion.Euler(boids[i].vectorAngleX, boids[i].vectorAngleY, 0)  * Vector3.forward * 5;
            boids[i].BoidDirection = boidSwarm[i].velocity;
            boids[i].Position = boidSwarm[i].position;
            boidSwarm[i].rotation = Quaternion.Euler(boids[i].angleX,boids[i].angleY, 0);
            
            
            for (int k = 0; k < NumBoids; k++) {
                //Cycle through all boids, if x y and z values of the boids are within the index i boid's radius, add them to an array,
                //then get the average heading of all of them, apply that heading to the current boid    
            }
        }
    }
}
public class Boid : Application {
    //Initialize the basics for each BOID
    public float AlignmentForce = 1;
    public float AvoidForce = 1;
    public float CollisionForce = 10;
    public Vector3 Position = new Vector3(0,0,0);
    public const float ViewRadius = 5;
    public const float AvoidRadius = 2;
    public const float CollisionRadius = 5;
    private static float goldenRatio = (1 + Mathf.Sqrt (5)) / 2;
    private static float angleIncrement = Mathf.PI * 2 * goldenRatio;
    public Vector3[] Directions;
    private const int NumViewDirections = 300;
    public Vector3 BoidDirection;
    public float angleX = -90;
    public float angleY = -90;
    public float vectorAngleX = 0;
    public float vectorAngleY = 0;
    
    //Function for finding average heading  : FUNCTION 1
    public static Vector3 AverageHeading(Boid[] boidArray)
    {
        Vector3[] headings = new Vector3[boidArray.Length];
        Vector3 headingSum = new Vector3();
        for (int i = 0 ; i < boidArray.Length; i++)
        {
            headings[i] = boidArray[i].BoidDirection;
        }
        for (int i = 0 ; i < boidArray.Length; i++)
        {
            headingSum += headings[i];
        }
        Vector3 averageHeading = headingSum / boidArray.Length;
        return averageHeading;
    }
    //Cycle through all BOIDS, if one is in the viewRadius, add their heading value to an array
    //Take said array and average all of the values and make it its own heading value
    public static void BoidDirections () {
        Vector3[] directions = new Vector3[NumViewDirections];
        for (int i = 0; i < NumViewDirections; i++) {
            float t = (float) i / NumViewDirections;
            float inclination = Mathf.Acos (1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
            float y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
            float z = Mathf.Cos (inclination);
            directions[i] = new Vector3 (x, y, z);
        }
    }
    //Function for BOID avoidance  : FUNCTION 2
    public static void BoidAvoid(Boid[] boidArray)
    {
        
    }
    //Cycle through all BOIDS, if one is in the avoidRadius, change heading and speed accordingly

    //Function for obstacle avoidance  : FUNCTION 3
    public static void ObstacleAvoid()
    {
        
    }
    //Cast out rays to locate obstacle, if obstacle is near, change heading and speed
    
    
}
