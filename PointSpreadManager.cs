using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpreadManager : MonoBehaviour
{
    public float spread = 0.2f;
    public List<MapPoint> points;

    private void Start()
    {
        points = GetComponent<CircleMeshGenerator>().points;
    }
    private void Update()
    {
        for(int i = 0; i < points.Count; i++)
        {
            if(i!=0)
                points[i-1].velocity += (points[i].x- points[i - 1].x )* spread;
            else
                points[points.Count-1].velocity += (points[i].x - points[points.Count - 1].x) * spread;
            if (i!=points.Count-1)
                points[i + 1].velocity += (points[i].x - points[i + 1].x) * spread;
            else
                points[0].velocity += (points[i].x - points[0].x) * spread;
        }
    }
}
