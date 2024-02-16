using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint 
{
    public float posX;
    public float posY;
    public Vector2 Pos
    {
        set { posX = value.x; posY = value.y; }
        get
        {
            return new Vector2(posX, posY);
        }
    }
    [Header("Init")]
    public float angle;
    public float targetX=1;
    public float damping = 1f;
    public float k = -50f;
    [Header("changed")]
    public float x=0;
    public float velocity=0;
    public float acceleration=0;
    public float loss=0;



    public MapPoint() { }
    public MapPoint(float damping,float k)
    {
        this.damping = damping;
        this.k = k;
    }

    //private void Update()
    //{
    //    UpdatePhysics();
    //    transform.position = new Vector2(posX, x);
    //}
    public void UpdatePhysics()
    {
        loss =- damping * velocity;
        acceleration = (x - targetX) * k+loss;
        velocity += acceleration*Time.deltaTime;
        x += velocity*Time.deltaTime;
    }
}
