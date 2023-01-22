using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    QuadHandler quadHandler = new QuadHandler();
    Vector3 createFrom;

    Quad quadTest;

    // Start is called before the first frame update
    void Start()
    {

        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        createFrom = transform.position;

        quadTest = new Quad(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 0, 1), new Vector3(1, 0, 0));
        quadHandler.Add(quadTest.Copy());

        for(int i = 0; i < 100000; i++)
        {
            Quad quad = new Quad(quadTest.Copy());
            quad.AddOffset(new Vector3(0, 0, 1) * steps);
            steps++;
            quadHandler.Add(quad);
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = quadHandler.vertices.ToArray();
        mesh.triangles = quadHandler.triangles.ToArray();

        meshCollider.sharedMesh = mesh;
        meshFilter.sharedMesh = mesh;
    }

    float timer = 0.1f;
    int steps = 0;
    int maxQuads = 100;

    float deltaTime = 0;
    [SerializeField] float fps;
    [SerializeField] int verticeCount;
    [SerializeField] int triangleCount;
    [SerializeField] int quadCount;
    [SerializeField] int maxQuadCount;


    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;

        verticeCount = quadHandler.vertices.Count;
        triangleCount = quadHandler.triangles.Count;
        quadCount = quadHandler.Count;


        timer -= Time.deltaTime;

        /*
        if (timer <= 0)
        {
            timer = 0f;
            if (quadHandler.Count > maxQuadCount && quadHandler.Count > 0)
            {
                quadHandler.RemoveAt(0);
                return;
            }

            Quad quad = new Quad(quadTest.Copy());
            quad.AddOffset(new Vector3(0, 0, 1) * steps);
            quadHandler.Add(quad);
            steps++;
            UpdateMesh();
        }
        */
    }
}

public class Quad
{
    public Vector3 leftUp;
    public Vector3 leftDown;
    public Vector3 rightUp;
    public Vector3 rightDown;

    public int triangleOffset = 0;

    public Quad(Vector3 leftUp, Vector3 leftDown, Vector3 rightUp, Vector3 rightDown)
    {
        this.leftUp = leftUp;
        this.leftDown = leftDown;
        this.rightUp = rightUp;
        this.rightDown = rightDown;
    }
    public Quad(Quad quad)
    {
        this.leftUp = quad.leftUp;
        this.leftDown = quad.leftDown;
        this.rightUp = quad.rightUp;
        this.rightDown = quad.rightDown;
    }

    public Quad Copy()
    {
        Quad quadCopy = new Quad(leftUp, leftDown, rightUp, rightDown);
        return quadCopy;
    }
    public void AddOffset(Vector3 offset)
    {
        leftUp += offset;
        leftDown += offset;
        rightUp += offset;
        rightDown += offset;
    }
    public Vector3[] GetVertices()
    {
        return new Vector3[] { leftUp, leftDown, rightUp, rightDown };
    }
    public int[] GetTriangles()
    {
        return new int[] {
            0 + triangleOffset, 2 + triangleOffset, 1 + triangleOffset, // indexes to build first triangle
            1 + triangleOffset, 2 + triangleOffset, 3 + triangleOffset }; // indexes to build second triangle
    }
}

public class QuadHandler
{

    List<Quad> quads = new List<Quad>();

    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();

    public int Count { get { return quads.Count; } }

    public void Add(Quad quad)
    {
        quad.triangleOffset = vertices.Count;
        vertices.AddRange(quad.GetVertices());
        triangles.AddRange(quad.GetTriangles());
        quads.Add(quad);
    }
    public void RemoveAt(int index)
    {
        Quad quad = quads[index];
        Remove(quad);
    }
    public void Remove(Quad quad)
    {
        #region Remove Verticies
        Vector3[] deleteVertices = quad.GetVertices();
        foreach (Vector3 deleteVertex in deleteVertices)
        {
            vertices.Remove(deleteVertex);
        }
        #endregion
        #region Remove Triangles
        int[] deleteTriangles = quad.GetTriangles();
        foreach (int deleteTriangle in deleteTriangles)
        {
            triangles.Remove(deleteTriangle);
        }
        #endregion

        quads.Remove(quad);
    }
    public void Clear()
    {
        quads.Clear();
        triangles.Clear();
        vertices.Clear();
    }
}
