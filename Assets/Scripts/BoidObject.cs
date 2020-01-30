using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidObject : MonoBehaviour
{
    private static readonly Random Rand = new Random();
    
    public Vector3 direction;
    public Vector3 position;
    public float speed = 10;
    
    private float _viewRadius = 30;
    private float _obstacleRadius = 45;
}
