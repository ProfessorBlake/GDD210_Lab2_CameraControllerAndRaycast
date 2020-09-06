using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
	public float MouseSensitivity;
	public Transform CamTransform;
	public CharacterController CC;
	public float Strength;

	private float camRotation = 0f;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
		camRotation -= mouseInputY;
		camRotation = Mathf.Clamp(camRotation, -90f, 90f);
		CamTransform.localRotation = Quaternion.Euler(camRotation, 0f, 0f);

		float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX));	
	}

	private void FixedUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			Push(1f);
		}
		else if (Input.GetMouseButton(1))
		{
			Push(-1f); //Pull
		}
	}

	private void Push(float direction)
	{
		RaycastHit hit;
		if (Physics.Raycast(CamTransform.position, CamTransform.forward, out hit))
		{
			Debug.DrawLine(CamTransform.position + new Vector3(0f, -1f, 0f), hit.point, Color.cyan);
			
			Rigidbody hitRB = hit.collider.GetComponent<Rigidbody>();
			if(hitRB != null)
			{
				hitRB.AddForceAtPosition(CamTransform.forward * Strength * direction, hit.point);
			}
		}

	}
}
