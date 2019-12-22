using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    private Vector3 averageHeading;
    private LayerMask obstacle;
    public Boid[] boids = new Boid[10];
    public Rigidbody boidModel;
    public Rigidbody[] boidSwarm = new Rigidbody[10];
    void Start()
    {

        //Spawn a bunch of BOIDS
        for (int i = 0; i < 10; i++)
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
        for (int i = 0, j = 10; i < 10; i++, j++)
        {
            boidSwarm[i].velocity = Quaternion.Euler(boids[i].vectorX, boids[i].vectorY, 0)  * Vector3.forward * 1;
            boidSwarm[i].rotation = Quaternion.Euler(boids[i].angleX,boids[i].angleY, 0);
        }
    }
}
public class Boid : Application {
    //Initialize the basics for each BOID
    public float alignmentForce = 1;
    public float avoidForce = 1;
    public float collisionForce = 10;
    public Vector3 position;
    public float viewRadius = 5;
    public float avoidRadius = 2;
    public float collisionRadius = 5;
    public float minSpeed = 3;
    public float maxSpeed = 5;
    public static float goldenRatio = (1 + Mathf.Sqrt (5)) / 2;
    public static float angleIncrement = Mathf.PI * 2 * goldenRatio;
    public Vector3[] directions;
    public const int numViewDirections = 300;
    Vector3 boidDirection;
    public float angleX = -90;
    public float angleY = -90;
    public float vectorX = 0;
    public float vectorY = 0;


    public void setDirection()
    {
        Vector3 heading1 = new Vector3(0,0,0);
        Vector3 heading2 = new Vector3(1,1,0);
        boidDirection = (heading1 - heading2)/(heading1 - heading2).magnitude;
    }

    //Function for finding average heading  : FUNCTION 1
    public static Vector3 averageHeading(Boid[] boidArray)
    {
        Vector3[] headings = new Vector3[boidArray.Length];
        Vector3 headingSum = new Vector3();
        for (int i = 0 ; i < boidArray.Length; i++)
        {
            headings[i] = boidArray[i].boidDirection;
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
    public static void boidDirections () {
        Vector3[] directions = new Vector3[numViewDirections];
        for (int i = 0; i < numViewDirections; i++) {
            float t = (float) i / numViewDirections;
            float inclination = Mathf.Acos (1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
            float y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
            float z = Mathf.Cos (inclination);
            directions[i] = new Vector3 (x, y, z);
        }
    }
    //Function for BOID avoidance  : FUNCTION 2
    public static void boidAvoid(Boid[] boidArray)
    {
        
    }
    //Cycle through all BOIDS, if one is in the avoidRadius, change heading and speed accordingly

    //Function for obstacle avoidance  : FUNCTION 3
    public static void obstacleAvoid()
    {
        
    }
    //Cast out rays to locate obstacle, if obstacle is near, change heading and speed
    
    
}
