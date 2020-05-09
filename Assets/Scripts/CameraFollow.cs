using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject Target;
	public float FollowDistance = 20f;
	public float Speed = 75f;
	public float RunSpeed = 2f;

	private Transform m_tTargetTransform;
	private Transform m_tTargetModelTransform;
	private PlayerControler m_pcTargetControler;

	private float m_fHorizontalAngle = 180f;
	private float m_fVerticalAngle = 45f;

	void Awake()
	{
		m_tTargetTransform = Target.transform;
		m_tTargetModelTransform = Target.transform.Find("Model");
		m_pcTargetControler = Target.GetComponent<PlayerControler>();
	}

	void FixedUpdate()
	{
		Vector2 v2Input = GetRightStickInput();

		UpdateCameraAngle(v2Input);

		Vector3 v3CameraOffset = GetCameraOffset();

		transform.position = m_tTargetTransform.position + v3CameraOffset;
		transform.LookAt(m_tTargetTransform); //Must look after moving to prevent jitter
	}

	private Vector2 GetRightStickInput()
	{
		float fHorizontalAxisInput = Input.GetAxisRaw("Horizontal Right");
		float fVerticalAxisInput = Input.GetAxisRaw("Vertical Right");
		Vector2 v2Input = new Vector2(fHorizontalAxisInput, fVerticalAxisInput);

		return Vector2.ClampMagnitude(v2Input, 1);
	}

	private void UpdateCameraAngle(Vector2 v2Input)
	{
		//Base angles updated by input
		m_fHorizontalAngle += v2Input.x * (Speed * Time.deltaTime);
		m_fVerticalAngle += -v2Input.y * (Speed * Time.deltaTime);

		if (m_pcTargetControler.Run)
		{
			//Shunt the horizontal angle behind target while running
			float fAngleBehindTarget = Mathf.Repeat(m_tTargetModelTransform.rotation.eulerAngles.y + 180, 360);
			m_fHorizontalAngle = Mathf.LerpAngle(m_fHorizontalAngle, fAngleBehindTarget, RunSpeed * Time.deltaTime);
		}
	}

	private Vector3 GetCameraOffset()
	{
		Quaternion qHorizontal = Quaternion.AngleAxis(m_fHorizontalAngle, Vector3.up);
		Quaternion qVertical = Quaternion.AngleAxis(m_fVerticalAngle, Vector3.left);
		Vector3 v3Horizontal = qHorizontal * m_tTargetTransform.forward;
		Vector3 v3Vertical = qHorizontal * qVertical * m_tTargetTransform.forward;
		Vector3 v3CombinedAngle = v3Horizontal + v3Vertical;

		return v3CombinedAngle.normalized * FollowDistance;
	}
}
