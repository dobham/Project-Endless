using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = System.Random;

public class AsteriodGenerator : MonoBehaviour
{
    public float width = 1;
    public float height = 1;
    public Material material;
    void Start()
    {
        renderAsteriod();
        /* asteroid.GetComponent<MeshFilter>().mesh = mesh;
         asteroid.GetComponent<MeshRenderer>().material = material;*/
    }

    private void renderAsteriod()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        Color c1 = new Color(0.5f, 0.5f, 0.5f, 1);
        lineRenderer.material = material;
        lineRenderer.SetColors(Color.yellow, Color.green);
        lineRenderer.SetWidth(0.5f, 0.5f);
        lineRenderer.SetVertexCount(128);
        lineRenderer.useWorldSpace = false;

        var rand = new Random();


        float a = 0;
        double t = 0, d = 0;
        //The vertices of the mesh       
        for(int i = 0; i < (int)Mathf.PI*20+3; i++, a+=0.1f) {    
            //float r = 50;
            //float r = rand.Next(65,100);
            t += rand.NextDouble() * 0.40;
            d += rand.NextDouble() * 0.65;
            /*t = rand.NextDouble() * Mathf.Cos(a) + 1;
            d = rand.NextDouble() * Mathf.Sin(a) + 1;*/
            float r = Mathf.Lerp(100, 200, Mathf.InverseLerp(0, 1, Mathf.PerlinNoise((float)t,(float)d)));
            float x = r * Mathf.Cos(a);
            float y = r * Mathf.Sin(a);
            Vector3 pos = new Vector3(x,y,0);
            lineRenderer.SetPosition(i, pos);
            
      
        }
     
        /*float deltaTheta = (float) (2 * Mathf.PI) / 128;
        float theta = 0f;

        for (int i = 0; i < 360 + 1; i++)
        {
            float radius = 50;
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0, z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }*/
    }
}
