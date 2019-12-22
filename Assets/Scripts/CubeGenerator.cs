using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public Material material;
    public static int subdivisions = 9;
    Vector3[] verts = new Vector3[subdivisions * subdivisions * 6];
    int[] triangleVerts = new int[(subdivisions - 1) * (subdivisions - 1) * 36];
    Vector3[] normals = new Vector3[subdivisions * subdivisions * 6];

    void Start()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        Mesh cubeSphere = new Mesh();

        int index = 0;
        for (var i = 0; i < 6; i++)
        {
            switch (i)
            {
                //top square
                case 0:
                    for (int x = -Mathf.FloorToInt(subdivisions / 2f); x < Mathf.CeilToInt(subdivisions / 2f); x++)
                    {
                        for (var y = -Mathf.FloorToInt(subdivisions / 2f);
                            y < Mathf.CeilToInt(subdivisions / 2f);
                            y++, index++)
                        {
                            normals[index] = verts[index] =
                                new Vector3(x, y, Mathf.FloorToInt(subdivisions / 2f)).normalized;
//                            float xVal = v.x * v.x;
//                            float yVal = v.y * v.y;
//                            float zVal = v.z * v.z;
//                            Vector3 s;
//                            s.x = v.x * Mathf.Sqrt(6f - 3*yVal - 3*zVal + 2*(yVal * zVal));
//                            s.y = v.y * Mathf.Sqrt(1f - xVal / 2f - zVal / 2f + xVal * zVal / 3f);
//                            s.z = v.z * Mathf.Sqrt(1f - xVal / 2f - yVal / 2f + xVal * yVal / 3f);
//                            normals[index] = s;
//                            verts[index] = normals[index];
//                            print(verts[index]);
                        }
                    }

                    break;
                //right square (when looking from above)
                case 1:
                    for (var y = -Mathf.FloorToInt(subdivisions / 2f); y < Mathf.CeilToInt(subdivisions / 2f); y++)
                    {
                        for (var z = -Mathf.FloorToInt(subdivisions / 2f);
                            z < Mathf.CeilToInt(subdivisions / 2f);
                            z++, index++)
                        {
                            normals[index] = verts[index] =
                                new Vector3(Mathf.FloorToInt(subdivisions / 2f), y, z).normalized;
//                            float xVal = verts[index].x * verts[index].x;
//                            float yVal = verts[index].y * verts[index].y;
//                            float zVal = verts[index].z * verts[index].z;
//                            Vector3 s;
//                            s.x = verts[index].x * Mathf.Sqrt(1f - yVal / 2f - zVal / 2f + yVal * zVal / 3f);
//                            s.y = verts[index].y * Mathf.Sqrt(1f - xVal / 2f - zVal / 2f + xVal * zVal / 3f);
//                            s.z = verts[index].z * Mathf.Sqrt(1f - xVal / 2f - yVal / 2f + xVal * yVal / 3f);
//                            normals[index] = s;
                        }
                    }

                    break;
                //left sqaure
                case 2:
                    for (var y = Mathf.FloorToInt(subdivisions / 2f); y > -Mathf.CeilToInt(subdivisions / 2f); y--)
                    {
                        for (var z = -Mathf.FloorToInt(subdivisions / 2f);
                            z < Mathf.CeilToInt(subdivisions / 2f);
                            z++, index++)
                        {
                            normals[index] = verts[index] =
                                new Vector3(-Mathf.FloorToInt(subdivisions / 2f), y, z).normalized;
//                            float xVal = verts[index].x * verts[index].x;
//                            float yVal = verts[index].y * verts[index].y;
//                            float zVal = verts[index].z * verts[index].z;
//                            Vector3 s;
//                            s.x = verts[index].x * Mathf.Sqrt(1f - yVal / 2f - zVal / 2f + yVal * zVal / 3f);
//                            s.y = verts[index].y * Mathf.Sqrt(1f - xVal / 2f - zVal / 2f + xVal * zVal / 3f);
//                            s.z = verts[index].z * Mathf.Sqrt(1f - xVal / 2f - yVal / 2f + xVal * yVal / 3f);
//                            normals[index] = s;
                        }
                    }

                    break;
                //front sqaure
                case 3:
                    for (var x = -Mathf.FloorToInt(subdivisions / 2f); x < Mathf.CeilToInt(subdivisions / 2f); x++)
                    {
                        for (var z = -Mathf.FloorToInt(subdivisions / 2f);
                            z < Mathf.CeilToInt(subdivisions / 2f);
                            z++, index++)
                        {
                            normals[index] = verts[index] =
                                new Vector3(x, -Mathf.FloorToInt(subdivisions / 2f), z).normalized;
//                            float xVal = verts[index].x * verts[index].x;
//                            float yVal = verts[index].y * verts[index].y;
//                            float zVal = verts[index].z * verts[index].z;
//                            Vector3 s;
//                            s.x = verts[index].x * Mathf.Sqrt(1f - yVal / 2f - zVal / 2f + yVal * zVal / 3f);
//                            s.y = verts[index].y * Mathf.Sqrt(1f - xVal / 2f - zVal / 2f + xVal * zVal / 3f);
//                            s.z = verts[index].z * Mathf.Sqrt(1f - xVal / 2f - yVal / 2f + xVal * yVal / 3f);
//                            normals[index] = s;
                        }
                    }

                    break;
                //back square
                case 4:
                    for (var x = Mathf.FloorToInt(subdivisions / 2f); x > -Mathf.CeilToInt(subdivisions / 2f); x--)
                    {
                        for (var z = -Mathf.FloorToInt(subdivisions / 2f);
                            z < Mathf.CeilToInt(subdivisions / 2f);
                            z++, index++)
                        {
                            normals[index] = verts[index] =
                                new Vector3(x, Mathf.FloorToInt(subdivisions / 2f), z).normalized;
//                            float xVal = verts[index].x * verts[index].x;
//                            float yVal = verts[index].y * verts[index].y;
//                            float zVal = verts[index].z * verts[index].z;
//                            Vector3 s;
//                            s.x = verts[index].x * Mathf.Sqrt(1f - yVal / 2f - zVal / 2f + yVal * zVal / 3f);
//                            s.y = verts[index].y * Mathf.Sqrt(1f - xVal / 2f - zVal / 2f + xVal * zVal / 3f);
//                            s.z = verts[index].z * Mathf.Sqrt(1f - xVal / 2f - yVal / 2f + xVal * yVal / 3f);
//                            normals[index] = s;
                        }
                    }

                    break;
                //bottom square
                default:
                    for (var x = Mathf.FloorToInt(subdivisions / 2f); x > -Mathf.CeilToInt(subdivisions / 2f); x--)
                    {
                        for (var y = -Mathf.FloorToInt(subdivisions / 2f);
                            y < Mathf.CeilToInt(subdivisions / 2f);
                            y++, index++)
                        {
                            normals[index] = verts[index] =
                                new Vector3(x, y, -Mathf.FloorToInt(subdivisions / 2f)).normalized;
//                            float xVal = verts[index].x * verts[index].x;
//                            float yVal = verts[index].y * verts[index].y;
//                            float zVal = verts[index].z * verts[index].z;
//                            Vector3 s;
//                            s.x = verts[index].x * Mathf.Sqrt(1f - yVal / 2f - zVal / 2f + yVal * zVal / 3f);
//                            s.y = verts[index].y * Mathf.Sqrt(1f - xVal / 2f - zVal / 2f + xVal * zVal / 3f);
//                            s.z = verts[index].z * Mathf.Sqrt(1f - xVal / 2f - yVal / 2f + xVal * yVal / 3f);
//                            normals[index] = s;
                        }
                    }

                    break;
            }
        }

//        verts[11] *= 2;
//        verts[12] *= 3.2f;
//        verts[17] *= 1.5f;

        cubeSphere.vertices = verts;
        cubeSphere.normals = normals;
        index = 0;
        for (int face = 0; face < 6; face++)
        {
            for (int xIndex = 0; xIndex < subdivisions - 1; xIndex++)
            {
                for (int yIndex = 0; yIndex < subdivisions - 1; yIndex++)
                {
                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + yIndex;
                    index++;

                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) +
                                           (yIndex + subdivisions + 1);
                    index++;

                    triangleVerts[index] =
                        (face * subdivisions * subdivisions) + (xIndex * subdivisions) + (yIndex + 1);
                    index++;

                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + yIndex;
                    index++;

                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) +
                                           (yIndex + subdivisions);
                    index++;

                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) +
                                           (yIndex + subdivisions + 1);
                    index++;
                }
            }
        }

        cubeSphere.triangles = triangleVerts;

        cubeSphere.RecalculateNormals();
        meshFilter.mesh = cubeSphere;
        meshRenderer.material = material;


        for (int i = 0; i < (subdivisions - 1) * (subdivisions - 1) * 36; i++)
        {
            //print(i);
            // print(triangleVerts[i]);
        }

        for (int i = 0; i < subdivisions * subdivisions * 6; i++)
        {
            GameObject dube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dube.transform.localScale = Vector3.one * 0.2f;

            dube.name = i.ToString();
            dube.transform.position = verts[i].normalized;
        }
    }
}