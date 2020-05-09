using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float Friction = 0.4f;
    public float Deceleration = 0.2f;

    public float StrafeAccelerationPercent = .25f;
    public int StrafeAngle = 22;
    public int SkidAngle = 90;

    public float SneakAcceleration = 2f;
    public float WalkAcceleration = 2f;
    public float RunAcceleration = 2f;

    public float SneakMaxVelocity = 2f;
    public float WalkMaxVelocity = 5f;
    public float RunMaxVelocity = 10f;

	public float TurnSpeed = 10f;

    public bool Run
    {
        get { return m_bRun; }
    }

    private Vector3 m_v3PlayerVelocity;
    private float m_fPlayerMaxVelocity;
    private bool m_bSneak;
    private bool m_bRun;

	private Transform m_tModelTransform;

	void Awake()
	{
		m_tModelTransform = transform.Find("Model");
	}

	void FixedUpdate()
    {
        Vector2 v2Input = GetLeftStickInput();
        
        m_bSneak = Input.GetButton("Sneak");
        m_bRun = Input.GetButton("Run");

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

        if (m_fPlayerMaxVelocity > fMaxVelocity)
        {
            m_fPlayerMaxVelocity = Mathf.Max(m_fPlayerMaxVelocity - Deceleration, fMaxVelocity);
        }
        else if (m_fPlayerMaxVelocity < fMaxVelocity)
		{
            m_fPlayerMaxVelocity = fMaxVelocity;
        }
    }

    private void Move(Vector2 v2Input)
    {
        Vector3 v3Acceleration;

        if (v2Input.sqrMagnitude == 0)
        {
            ApplyFriction();
        }
        else
        {
            float fAcceleration = GetAcceleration();

            v3Acceleration = CalculateCameraRelativeAcceleration(v2Input, fAcceleration);
            
            if (m_bRun)
            {
                v3Acceleration = ApplyTurnDampening(v3Acceleration);
            }

            m_v3PlayerVelocity = Vector3.ClampMagnitude(m_v3PlayerVelocity + v3Acceleration, m_fPlayerMaxVelocity);
        }

        transform.position = transform.position + (m_v3PlayerVelocity * Time.deltaTime);
		
		//Rotation
		Vector3 v3Zero = new Vector3(0, 0, 0);
        if (m_v3PlayerVelocity != v3Zero)
		{
			Quaternion qLookTarget = Quaternion.LookRotation(m_v3PlayerVelocity);
			m_tModelTransform.rotation = Quaternion.Lerp(m_tModelTransform.rotation, qLookTarget, TurnSpeed * Time.deltaTime);
		}
    }

    private Vector3 CalculateCameraRelativeAcceleration(Vector2 v2Input, float fAcceleration)
    {
        Vector3 v3RelativeForward, v3RelativeRight, v3PlayerAcceleration;
        Transform tCameraTransform = Camera.main.transform;

        v3RelativeForward = CalculateXYProjectedNormal(tCameraTransform.forward) * v2Input.y;
        v3RelativeRight = CalculateXYProjectedNormal(tCameraTransform.right) * v2Input.x;
        v3PlayerAcceleration = v3RelativeForward + v3RelativeRight;

        return v3PlayerAcceleration * fAcceleration;
    }

    private void ApplyFriction()
    {
        Vector3 v3Friction = Vector3.ClampMagnitude(m_v3PlayerVelocity, Friction);
        m_v3PlayerVelocity = m_v3PlayerVelocity - v3Friction;
    }

    private Vector3 ApplyTurnDampening(Vector3 v3Acceleration)
    {
        float fAngle = Vector3.Angle(m_v3PlayerVelocity, v3Acceleration);
        
        if (fAngle < StrafeAngle)
        {
            return v3Acceleration;
        }
        else
        {
            v3Acceleration = v3Acceleration * StrafeAccelerationPercent;
        }

        if (fAngle > SkidAngle)
        {
            ApplyFriction();
        }

        return v3Acceleration;
    }

    private Vector3 CalculateXYProjectedNormal(Vector3 v3Input)
    {
        v3Input.y = 0;
        return v3Input.normalized;
    }

    private float GetMaxVelocity()
    {
        if (m_bSneak)
        {
            return SneakMaxVelocity;
        }
        else if (m_bRun)
        {
            return RunMaxVelocity;
        }

        return WalkMaxVelocity;
    }

    private float GetAcceleration()
    {
        if (m_bSneak)
        {
            return SneakAcceleration;
        }
        else if (m_bRun)
        {
            return RunAcceleration;
        }

        return WalkAcceleration;
    }
}
