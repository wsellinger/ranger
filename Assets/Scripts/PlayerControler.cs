using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float Friction = 0.6f;
    public float Deceleration = 0.2f;

    public float SneakAcceleration = 2f;
    public float WalkAcceleration = 2f;
    public float RunAcceleration = 2f;

    public float SneakMaxVelocity = 2f;
    public float WalkMaxVelocity = 5f;
    public float RunMaxVelocity = 10f;

    private Vector3 v3PlayerVelocity;
    private float fPlayerMaxVelocity;
    private bool bSneak;
    private bool bRun;

    void Start ()
    {
		
	}

    void FixedUpdate()
    {
        Vector2 v2Input = GetLeftStickInput();
        
        bSneak = Input.GetButton("Sneak");
        bRun = Input.GetButton("Run");

        UpdateMaxVelocity(v2Input);
        Move(v2Input);
    }

    private Vector2 GetLeftStickInput()
    {
        float fHorizontalAxisInput = Input.GetAxisRaw("Horizontal Left");
        float fVerticalAxisInput = Input.GetAxisRaw("Vertical Left");
        Vector2 v2Input = new Vector2(fHorizontalAxisInput, fVerticalAxisInput);

        return Vector2.ClampMagnitude(v2Input, 1);
    }

    private void UpdateMaxVelocity(Vector2 v2Input)
    {
        float fMaxVelocity = GetMaxVelocity();

        fMaxVelocity = fMaxVelocity * v2Input.magnitude;

        if (fPlayerMaxVelocity > fMaxVelocity)
        {
            fPlayerMaxVelocity = Mathf.Max(fPlayerMaxVelocity - Deceleration, fMaxVelocity);
        }
        else if (fPlayerMaxVelocity < fMaxVelocity)
        {
            fPlayerMaxVelocity = fMaxVelocity;
        }
    }

    void Move(Vector2 v2Input)
    {
        Vector3 v3Acceleration;

        if (v2Input.sqrMagnitude == 0)
        {
            Vector3 v3Friction = Vector3.ClampMagnitude(v3PlayerVelocity, Friction);
            v3PlayerVelocity = v3PlayerVelocity - v3Friction;
        }
        else
        {
            float fAcceleration = GetAcceleration();

            v3Acceleration = CalculateCameraRelativeAcceleration(v2Input, fAcceleration);
            v3PlayerVelocity = Vector3.ClampMagnitude(v3PlayerVelocity + v3Acceleration, fPlayerMaxVelocity);
        }

        transform.position = transform.position + (v3PlayerVelocity * Time.deltaTime);
    }

    private float GetMaxVelocity()
    {
        if (bSneak)
        {
            return SneakMaxVelocity;
        }
        else if (bRun)
        {
            return RunMaxVelocity;
        }

        return WalkMaxVelocity;
    }

    private float GetAcceleration()
    {
        if (bSneak)
        {
            return SneakAcceleration;
        }
        else if (bRun)
        {
            return RunAcceleration;
        }

        return WalkAcceleration;
    }

    Vector3 CalculateCameraRelativeAcceleration(Vector2 v2Input, float fAcceleration)
    {
        Vector3 v3RelativeForward, v3RelativeRight, v3PlayerAcceleration;
        Transform tCameraTransform = Camera.main.transform;

        v3RelativeForward = CalculateXYProjectedNormal(tCameraTransform.forward) * v2Input.y;
        v3RelativeRight = CalculateXYProjectedNormal(tCameraTransform.right) * v2Input.x;
        v3PlayerAcceleration = v3RelativeForward + v3RelativeRight;

        return v3PlayerAcceleration * fAcceleration;
    }

    Vector3 CalculateXYProjectedNormal(Vector3 v3Input)
    {
        v3Input.y = 0;
        return v3Input.normalized;
    }
}
