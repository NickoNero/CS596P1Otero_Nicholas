using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class Grided : MonoBehaviour {

    public int xSize, ySize;
    public float divbyzed = 10f;
    private float speed, scale, yVal;
    private Vector3[] vertices,newverts;
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider myMeshCollider;
    private Rigidbody myRigidbody;
    private MeshColliderCookingOptions myMeshCollCookOpts;

    private void Awake()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Resources.Load<Material>("MeshMaterial");
        //add a collider
        myMeshCollider = gameObject.AddComponent<MeshCollider>();
        myRigidbody = gameObject.AddComponent<Rigidbody>();
        Generate();

        newverts = new Vector3[(xSize + 1) * (ySize + 1)];
    }
    /*private void Start()
    {
        Generate();
    }*/
    private void Generate()
    {
        Rigidbody cubeRB;
        //WaitForSeconds wait = new WaitForSeconds(0.05f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        //add the meshCollider
        myMeshCollider = GetComponent<MeshCollider>();
        //add the rigidbody
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
        myRigidbody.isKinematic = true;
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        //Vector4[] tangents = new Vector4[vertices.Length];
        //Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y=0; y<= ySize; y++){
            for(int x = 0; x<= xSize; x++,i++)
            {
                if (x == y)
                {
                    yVal = Random.RandomRange(-5.0f,5.0f);
                }
                else
                    yVal = 0f;
                    vertices[i] = new Vector3(x, yVal, y);
                    uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                    //tangents[i] = tangent;
            }
        }
        //Add veritces to the mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        //mesh.tangents = tangents;

        int[] triangles = new int[xSize*ySize*6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
            
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                    triangles[ti + 5] = vi + xSize + 2;
                    //this is n^2 complexity

            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateNormals();
        myMeshCollider.sharedMesh = mesh;
        myMeshCollider.convex = false;
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(19, 19, 19);
        cubeRB = cube.AddComponent<Rigidbody>();
        cubeRB.useGravity = true;
    }
    void MeshWave()
    {
        mesh = this.gameObject.GetComponent<MeshFilter>().mesh;

        for (int i=0, y=0; y <= ySize; y++)
        {
            for(int x = 0; x <= xSize; x++, i++)
            {
                yVal = -scale * Mathf.Cos((i * Time.time / speed) + ((float)(vertices[i].z / speed) + ((float)vertices[i].x / speed)) / Mathf.PI);
                newverts[i] = new Vector3(x, yVal, y);
            }
        }
        vertices = newverts;
        mesh.vertices = newverts;
        //alter normals for lighting effects
        mesh.RecalculateNormals();
        myMeshCollider.sharedMesh=mesh;

    }
        private void OnDrawGizmos()
    {
        if(vertices == null)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        for(int i = 0; i<vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    /*private void Update()
    {
        MeshWave();
    }*/
    // apply an ethan to the world via import
}
