using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundCheck
{
    public bool isGrounded;
    public float offset = 0.1f;
    public Vector3 surfacePosition;
    //ContactFilter filter;
    Collider[] results = new Collider[1];
    Transform transform;

    private void IsGrounded()
    {
        Vector3 point = transform.position + Vector3.down * offset;
        Vector3 size = new Vector3(transform.localScale.x, transform.localScale.y);
        //if (Physics2D.OverlapBox(point, size, 0, filter.NoFilter(), results) > 0)
        //{
        //    isGrounded = true;
        //    surfacePosition = Physics.ClosestPoint(transform.position, results[0]);
        //}
        //else
        //{
        //    isGrounded = false;
        //}
    }
}
