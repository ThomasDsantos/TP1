using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RigidBodyPerso : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 10, 0);
    float gravity = 9.8f;
    //private bool isOnFloor = false;
 
    public float MovingForce;
    Vector3 StartPoint;
    Vector3 Origin;
    public int NoOfRays = 10;
    RaycastHit HitInfo;
    float LengthOfRay, DistanceBetweenRays, DirectionFactor;
    float margin = 0.015f;
    Ray ray;
    private Collider _collider;
    private Collider _collider1;

    // Start is called before the first frame update
    void Start()
    {
        _collider1 = GetComponent<Collider>();
        _collider = GetComponent<Collider>();
        LengthOfRay = GetComponent<Collider>().bounds.extents.y;
        //Initialize DirectionFactor for upward direction
        DirectionFactor = Mathf.Sign (Vector3.down.y);

    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //Detect colision from bottom and top
        StartPoint = new Vector3 (_collider.bounds.min.x + margin, transform.position.y, transform.position.z);
        if (!IsCollidingVertically())
        {
            //Newton physics
            velocity.y -= gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            //reset ball
            transform.position = new Vector3(10, 4, 10);
            velocity = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(2, 8), UnityEngine.Random.Range(-5, 5));
        }
    }
    
    bool IsCollidingVertically ()
    {
        Origin = StartPoint;
        DistanceBetweenRays = (_collider.bounds.size.x - 2 * margin) / (NoOfRays - 1);

        for (int i = 0; i < NoOfRays; i++) {

            // Ray to be casted.
            ray = new Ray (Origin, Vector3.up * DirectionFactor);
            if (Physics.Raycast (ray, out HitInfo, LengthOfRay))
            {
                //get name of collider to set if do that or that
                return true;
            }
            Origin += new Vector3 (DistanceBetweenRays, 0, 0);
        }
        return false;
    } 
}
