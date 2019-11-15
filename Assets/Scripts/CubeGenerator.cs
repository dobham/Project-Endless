using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour {
    public Material material;
    public const int n = 150;
    public Vector3[] verts = new Vector3[n]; 
    void Start() {
        int index = 0;
        Mesh cube = new Mesh();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        
        for (var i = 0; i < 6; i++) {
            switch (i) {
                //top square
                case 0:
                    for (int x = -2; x < 3; x++) {
                        for (var y = -2; y < 3; y++, index++) {
                            verts[index] = new Vector3(x, y, 2);
                            print(verts[index]);
                            print(index);
                        }
                    } break;
                //right square (when looking from above)
                case 1:
                    for (var y = -2; y < 3; y++) {
                        for (var z = -2; z < 3; z++, index++) {
                            verts[index] = new Vector3(2, y, z);
                            print(verts[index]);
                            print(index);
                        }
                    } break;
                //left sqaure
                case 2:
                    for (var y = -2; y < 3; y++) {
                        for (var z = -2; z < 3; z++, index++) {
                            verts[index] = new Vector3(-2, y, z);
                            print(verts[index]);
                            print(index);
                        }
                    } break;
                //front sqaure
                case 3:
                    for (var x = -2; x < 3; x++) {
                        for (var z = -2; z < 3; z++, index++) {
                            verts[index] = new Vector3(x, -2, z);
                            print(verts[index]);
                            print(index);
                        }
                    } break;
                //back square
                case 4:
                    for (var x = 2; x > -3; x--) {
                        for (var z = -2; z < 3; z++, index++) {
                            verts[index] = new Vector3(x, 2, z);
                            print(verts[index]);
                            print(index);
                        }
                    } break;
                //bottom square
                default:
                    for (var x = -2; x < 3; x++) {
                        for (var y = -2; y < 3; y++, index++) {
                            verts[index] = new Vector3(x, y, -2);
                            print(verts[index]);
                            print(index);
                        }
                    } break;
            }
        }
        for (int i = 0; i < 150; i++) {
            GameObject dube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dube.name = i.ToString();
            dube.transform.position = verts[i];
        }
    }
}
