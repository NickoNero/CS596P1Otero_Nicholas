using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridClass : MonoBehaviour {
    public Vector3[] newVertices;
    public Vector2[] newUV;
    public int[] newTriangles;
	// Use this for initialization
	void Start () {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
