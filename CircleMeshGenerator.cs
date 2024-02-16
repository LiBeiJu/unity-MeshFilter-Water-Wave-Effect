using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMeshGenerator : MonoBehaviour
{
    [SerializeField]public List<MapPoint> points=new List<MapPoint>();

    [Header("圆形")]
    public int numSegments = 100; // 圆形的线段数量
    public float radius = 1f; // 圆形的半径

    [Header("点波动")]
    public float damping = 1;
    public float k = -50f;

    [Header("Component")]
    public MeshFilter meshFilter = null;
    public PolygonCollider2D cld = null;

    Mesh mesh; 
    Vector3[] vertices; 
    int[] triangles;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateWave();
        }
    }

    public void CreateWave()
    {
        int i = UnityEngine.Random.Range(0, points.Count);
        points[i].velocity += 5;
        points[(i + 1 + points.Count) % points.Count].velocity += 4;
        points[(i - 1 + points.Count) % points.Count].velocity += 4;
        points[(i + 2 + points.Count) % points.Count].velocity += 2;
        points[(i - 2 + points.Count) % points.Count].velocity += 2;

    }
    private void Awake()
    {
        

        for (int i = 0; i < numSegments; i++)
        {
            points.Add(new MapPoint(damping,k));
        }

        foreach (MapPoint point in points)
        {
            point.targetX = radius;
            point.x = radius;
        }
        InitMesh();
        StartCoroutine(GenerateCircleMesh());
    }

    private void InitMesh()
    {
        mesh = new Mesh();
        mesh.name = "TestMeshCircle";
        vertices = new Vector3[numSegments + 1];
        triangles = new int[numSegments * 3];
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        cld=GetComponent<PolygonCollider2D>();  
    }
    IEnumerator GenerateCircleMesh()
    {
        while(Application.isPlaying) 
        {
            foreach (var item in points)
            { item.UpdatePhysics(); }

            // 中心点
            vertices[0] = Vector3.zero;

            // 生成圆形的顶点
            for (int i = 0; i < numSegments; i++)
            {
                float angle = Mathf.PI * 2f * i / numSegments;//弧度
                float x = Mathf.Cos(angle) * points[i].x;
                float y = Mathf.Sin(angle) * points[i].x;
                points[i].posX = x;
                points[i].posY = y;
                vertices[i + 1] = new Vector3(x, y, 0f);

            }


            // 生成圆形的三角形
            for (int i = 0; i < numSegments; i++)
            {
                triangles[i * 3] = 0; // 中心点
                triangles[i * 3 + 2] = i + 1;
                triangles[i * 3 + 1] = i + 2 <= numSegments ? i + 2 : 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            
            yield return null;
        }


    }
}
