﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    private Vector3 averageHeading;
    private LayerMask obstacle;
    public Boid[] boids = new Boid[10];
    public int[] numbers = new int[10];
    public GameObject boidModel;
    void Start()
    {

        //Spawn a bunch of BOIDS
        for (int i = 0; i < 10; i++)
        {
            boids[i] = new Boid();
            Instantiate(boidModel, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
    

    void Update()
    {

    }
}
public class Boid : Application {
    //Initialize the basics for each BOID
    public static float alignmentForce = 1;
    public static float avoidForce = 1;
    public static float collisionForce = 10;
    private static Vector3 position;
    private static float viewRadius = 5;
    private static float avoidRadius = 2;
    private static float collisionRadius = 5;
    private static float minSpeed = 3;
    private static float maxSpeed = 5;
    private static float goldenRatio = (1 + Mathf.Sqrt (5)) / 2;
    private static float angleIncrement = Mathf.PI * 2 * goldenRatio;
    private Vector3[] directions;
    private const int numViewDirections = 300;
    Vector3 boidDirection;

    void setDirection()
    {
        Vector3 heading1 = new Vector3(0,0,0);
        Vector3 heading2 = new Vector3(1,1,0);
        boidDirection = (heading1 - heading2)/(heading1 - heading2).magnitude;
    }

    //Function for finding average heading  : FUNCTION 1
    static Vector3 averageHeading(Boid[] boidArray)
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
    static void boidDirections () {
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
    static void boidAvoid(Boid[] boidArray)
    {
        
    }
    //Cycle through all BOIDS, if one is in the avoidRadius, change heading and speed accordingly

    //Function for obstacle avoidance  : FUNCTION 3
    static void obstacleAvoid()
    {
        
    }
    //Cast out rays to locate obstacle, if obstacle is near, change heading and speed
    
    
}
