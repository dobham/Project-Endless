using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{

    public Vector3 averageHeading;
    public LayerMask obstacle;
    


    void Start()
    {
        //Spawn a bunch of BOIDS
    }

    void Update()
    {
        //FUNCTION1()
        //FUNCTION2()
        //FUNCTION3()
    }
    
    //Function for finding average heading  : FUNCTION 1
        //Cycle through all BOIDS, if one is in the viewRadius, add their heading value to an array
        //Take said array and average all of the values and make it its own heading value
    
    //Function for BOID avoidance  : FUNCTION 2
        //Cycle through all BOIDS, if one is in the avoidRadius, change heading and speed accordingly
    
    //Function for obstacle avoidance  : FUNCTION 3
        //Cast out rays to locate obstacle, if obstacle is near, change heading and speed
}

public class Boid : MonoBehaviour
{
    //Initialize the basics for each BOID
    public static Vector3 position;
    private static float viewRadius = 5;
    private static float avoidRadius = 2;
    private static float collisionRadius = 5;
    private static float minSpeed = 3;
    private static float maxSpeed = 5;
    public static Vector3 heading;
    public static float alignmentForce = 1;
    public static float avoidForce = 1;
    public static float collisionForce = 10;
    
    
}
