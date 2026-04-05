using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class grappling : MonoBehaviour
{
    Rigidbody2D rb;
    LineRenderer lr;
    DistanceJoint2D dj;
    public LayerMask grappleLayer;
    public bool isGrappling;
    Vector3 point;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        lr.enabled = false;
        dj.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isGrappling = false;

            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0f;

            Collider2D hit = Physics2D.OverlapCircle(point, 0.5f, grappleLayer);

            if (hit)
            {
                isGrappling = true;
                lr.enabled = true;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, point);
                dj.enabled = true;
                dj.connectedAnchor = point;
            }
        }

        
        if (Input.GetMouseButtonUp(1))
        {
            isGrappling = false;
            lr.enabled = false;
            dj.enabled = false;
        }
        if (isGrappling)
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, point);
        }  
    }
}
