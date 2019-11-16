using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour {
    public Material material;
    public static int subdivisions = 5;
    public Vector3[] verts = new Vector3[subdivisions * subdivisions * 6];
    public int[] triangleVerts = new int[(subdivisions-1)*(subdivisions-1) * 36];
    public Vector3[] normals = new Vector3[subdivisions * subdivisions * 6];
    void Start() {
        
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        Mesh cubeSphere = new Mesh();
        
        
        int index = 0;
        for (var i = 0; i < 6; i++) {
            switch (i) {
                //top square
                case 0:
                    for (int x = -Mathf.FloorToInt(subdivisions/2f); x < Mathf.CeilToInt(subdivisions/2f); x++) {
                        for (var y = -Mathf.FloorToInt(subdivisions/2f); y < Mathf.CeilToInt(subdivisions/2f); y++, index++) {
                            normals[index] = verts[index] = new Vector3(x, y, Mathf.FloorToInt(subdivisions/2f)).normalized;
                        }
                    } break;
                //right square (when looking from above)
                case 1:
                    for (var y = -Mathf.FloorToInt(subdivisions/2f); y < Mathf.CeilToInt(subdivisions/2f); y++) {
                        for (var z = -Mathf.FloorToInt(subdivisions/2f); z < Mathf.CeilToInt(subdivisions/2f); z++, index++) {
                            normals[index] = verts[index] = new Vector3(Mathf.FloorToInt(subdivisions/2f), y, z).normalized;
                        }
                    } break;
                //left sqaure
                case 2:
                    for (var y = Mathf.FloorToInt(subdivisions/2f); y > -Mathf.CeilToInt(subdivisions/2f); y--) {
                        for (var z = -Mathf.FloorToInt(subdivisions/2f); z < Mathf.CeilToInt(subdivisions/2f); z++, index++) {
                            normals[index] = verts[index] = new Vector3(-Mathf.FloorToInt(subdivisions/2f), y, z).normalized;
                        }
                    } break;
                //front sqaure
                case 3:
                    for (var x = -Mathf.FloorToInt(subdivisions/2f); x < Mathf.CeilToInt(subdivisions/2f); x++) {
                        for (var z = -Mathf.FloorToInt(subdivisions/2f); z < Mathf.CeilToInt(subdivisions/2f); z++, index++) {
                            normals[index] = verts[index] = new Vector3(x, -Mathf.FloorToInt(subdivisions/2f), z).normalized;
                        }
                    } break;
                //back square
                case 4:
                    for (var x = Mathf.FloorToInt(subdivisions/2f); x > -Mathf.CeilToInt(subdivisions/2f); x--) {
                        for (var z = -Mathf.FloorToInt(subdivisions/2f); z < Mathf.CeilToInt(subdivisions/2f); z++, index++) {
                            normals[index] = verts[index] = new Vector3(x, Mathf.FloorToInt(subdivisions/2f), z).normalized;
                        }
                    } break;
                //bottom square
                default:
                    for (var x = Mathf.FloorToInt(subdivisions/2f); x > -Mathf.CeilToInt(subdivisions/2f); x--) {
                        for (var y = -Mathf.FloorToInt(subdivisions/2f); y < Mathf.CeilToInt(subdivisions/2f); y++, index++) {
                            normals[index] = verts[index] = new Vector3(x, y, -Mathf.FloorToInt(subdivisions/2f)).normalized;
                        }
                    } break;
            }
        }

        verts[11] *= 2;
        verts[12] *= 3.2f;
        verts[17] *= 1.5f;
        cubeSphere.vertices = verts;
        cubeSphere.normals = normals;
        index = 0;
        for (int face = 0; face < 6; face++) {
            for (int xIndex = 0; xIndex < subdivisions-1; xIndex++) {
                for (int yIndex = 0; yIndex < subdivisions-1; yIndex++) {
                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + yIndex;
                    index++;
                    
                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + (yIndex + 6);
                    index++;
                    
                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + (yIndex + 1);
                    index++;
                    
                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + yIndex;
                    index++;
                    
                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + (yIndex + 5);
                    index++;
                    
                    triangleVerts[index] = (face * subdivisions * subdivisions) + (xIndex * subdivisions) + (yIndex + 6);
                    index++;
                }
            } 
        }
        cubeSphere.triangles = triangleVerts;
        
        
        cubeSphere.RecalculateNormals();
        meshFilter.mesh = cubeSphere;
        meshRenderer.material = material;


        for (int i = 0; i < 576; i++)
        {
//            print(i);
            print(triangleVerts[i]);
        }

        for (int i = 0; i < 150; i++) {
            GameObject dube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dube.transform.localScale = Vector3.one * 0.2f;

            dube.name = i.ToString();
            dube.transform.position = verts[i].normalized;
        }
    }
}
