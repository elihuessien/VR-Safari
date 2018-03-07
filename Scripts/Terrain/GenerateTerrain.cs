using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Notes taken from: https://www.youtube.com/watch?v=dycHQFEz8VI*/
public class GenerateTerrain : MonoBehaviour {
    //No sense making it public and editing in Unity
    //Because it would be deleted and recreated constantly
    //an the single edit would be lost
    int heightScale;
    float detailScale;


	// Use this for initialization
	void Start () {
        heightScale = 3;
        detailScale = 4.0f;

        //get and edit mesh
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        //calculate heights for each vertex
        for(int i=0; i<vertices.Length; i++)
        {
            float x = ((vertices[i].x + this.transform.position.x) / detailScale);
            float z = ((vertices[i].z + this.transform.position.z) / detailScale);
            vertices[i].y = Mathf.PerlinNoise(x, z) * heightScale;
        }

        //aply changes
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }
}
