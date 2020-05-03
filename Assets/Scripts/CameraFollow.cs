using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float FollowDistance;
    public float Speed;

    float fHorizontalAngle = 180;
    float fVerticalAngle = 45;

    void Start ()
    {
        
    }

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal Right");
        float verticalAxis = -Input.GetAxisRaw("Vertical Right");

        fHorizontalAngle += horizontalAxis * Speed;
        fVerticalAngle += verticalAxis * Speed;
        
        Vector3 v3Horizontal, v3Vertical, v3CombinedAngle, v3CameraOffset;
        Quaternion qHorizontal, qVertical;

        qHorizontal = Quaternion.AngleAxis(fHorizontalAngle, Vector3.up);
        qVertical = Quaternion.AngleAxis(fVerticalAngle, Vector3.left);
        v3Horizontal = qHorizontal * Target.forward;
        v3Vertical = qHorizontal * qVertical * Target.forward;
        v3CombinedAngle = v3Horizontal + v3Vertical;
        v3CameraOffset = v3CombinedAngle.normalized * FollowDistance;

        transform.position = Target.position + v3CameraOffset;
        transform.LookAt(Target);
    }
}
