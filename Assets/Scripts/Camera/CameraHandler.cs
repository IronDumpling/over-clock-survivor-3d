using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoSingleton<CameraHandler>
{
    public Vector3 GetCameraForward()
    {
        Vector3 forward = -transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public Vector3 GetCameraRight()
    {
        Vector3 right = -transform.right;
        right.y = 0;
        return right.normalized;
    }

    public void LookAt()
    {

    }
}
