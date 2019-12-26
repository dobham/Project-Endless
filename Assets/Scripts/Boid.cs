// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Serialization;
// using Random = System.Random;
//
// public class Boid : MonoBehaviour
// {
//     private static readonly Random Rand = new Random();
//     
//     
//     public float AlignmentForce = 1;
//     public float AvoidForce = 1;
//     public float CollisionForce = 10;
//     
//     public readonly Boid[] ObservedBoids = new Boid[10];
//     public const float ViewRadius = 15;
//     public readonly float AvoidRadius = 2;
//     public readonly float CollisionRadius = 5;
//     
//     public Vector3 Position = new Vector3(Rand.Next(1,5),  Rand.Next(1,5), Rand.Next(1,5));
//     public const float Speed = 3;
//     public Vector3 Direction = new Vector3(Rand.Next(1, 5), Rand.Next(-5, 10), Rand.Next(1, 10));
//
//     
//     private static readonly float GoldenRatio = (1 + Mathf.Sqrt (5)) / 2;
//     private static readonly float AngleIncrement = Mathf.PI * 2 * GoldenRatio;
//     private const int NumViewDirections = 300;
//     
//     //Function for finding average heading  : FUNCTION 1
//
//     public static Vector3 AverageHeading(Boid[] boidArray) {
//         var headingSum = new Vector3();
//         var arrLength = boidArray.Count(t1 => t1 != null); //Gets the size of the non null array of boids
//         var headings = new Vector3[boidArray.Length];
//         var validDirections = new Vector3[boidArray.Length];
//         for (var t = 0; t < boidArray.Length; t++)
//         {
//             if (boidArray[t] != null)
//             {
//                 validDirections[t] = boidArray[t].Direction;
//             }
//         }
//         for (var i = 0 ; i < arrLength; i++)
//         {
//             headings[i] = validDirections[i];
//         }
//         for (var i = 0 ; i < arrLength; i++)
//         {
//             headingSum += headings[i];
//         }
//         var averageHeading = headingSum / arrLength;
//         return averageHeading.normalized;
//     }
//     //Cycle through all BOIDS, if one is in the viewRadius, add their heading value to an array
//     //Take said array and average all of the values and make it its own heading value
//     public void CollisionDirections () {
//         Vector3[] directions = new Vector3[NumViewDirections];
//         for (var i = 0; i < NumViewDirections; i++) {
//             var t = (float) i / NumViewDirections;
//             var inclination = Mathf.Acos (1 - 2 * t);
//             var azimuth = AngleIncrement * i;
//
//             var x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
//             var y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
//             var z = Mathf.Cos (inclination);
//             directions[i] = new Vector3 (x, y, z);
//         }
//     }
//     //Function for BOID avoidance  : FUNCTION 2
//     public void BoidAvoid(Boid[] boidArray) {
//         
//     }
//     //Cycle through all BOIDS, if one is in the avoidRadius, change heading and speed accordingly
//
//     //Function for obstacle avoidance  : FUNCTION 3
//     public void ObstacleAvoid() {
//         
//     }
//     //Cast out rays to locate obstacle, if obstacle is near, change heading and speed
//     
//     
// }