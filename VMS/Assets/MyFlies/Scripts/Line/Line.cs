using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
    public Material material;
	// Use this for initialization
	void Start () {
        //DrawTriangle();
    }
	
	// Update is called once per frame
	void Update () {
        Graphics.DrawMesh(gameObject.GetComponent<Mesh>(), Vector3.zero, Quaternion.identity, material, 0);
    }

    void DrawTriangle()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = material;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(5, 5, 5), new Vector3(5, 5, -5), new Vector3(5, -5, -5), new Vector3(5, -5, 5) };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 1 };
    }
}
