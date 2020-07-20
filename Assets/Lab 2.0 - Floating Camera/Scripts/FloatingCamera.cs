using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCamera : MonoBehaviour
{
	public float Speed;
	public float Sensitivity;

	private void Update()
	{
		float sprintMultiplier = 1f;
		if(Input.GetKey(KeyCode.LeftShift))
		{
			sprintMultiplier = 4f;
		}

		//Movement
		float forwardSpeed = Input.GetAxis("Vertical") * Speed * sprintMultiplier * Time.deltaTime;
		float sidewaysSpeed = Input.GetAxis("Horizontal") * Speed * sprintMultiplier * Time.deltaTime;

		transform.position += (transform.forward * forwardSpeed) + (transform.right * sidewaysSpeed);

		//Rotation
		float xSpeed = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
		float ySpeed = -Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

		transform.Rotate(new Vector3(ySpeed, xSpeed, 0f));
	}

	private void OnDrawGizmos()
	{
		//Navigation system

		Vector3 drawCenter = transform.position + (transform.right * -0.0f) + (transform.up * -0.15f) + (transform.forward * 0.2f); //Find a point at bottom left of view.

		Gizmos.color = Color.red;
		Gizmos.DrawLine(drawCenter, drawCenter + Vector3.right * 0.03f);
		Gizmos.DrawSphere(drawCenter + Vector3.right * 0.03f, 0.002f);
		Gizmos.DrawSphere(Vector3.right * 1000f, 10f);

		Gizmos.color = Color.green;
		Gizmos.DrawLine(drawCenter, drawCenter + Vector3.up * 0.03f);
		Gizmos.DrawSphere(drawCenter + Vector3.up * 0.03f, 0.002f);
		Gizmos.DrawSphere(Vector3.up * 1000f, 10f);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(drawCenter, drawCenter + Vector3.forward * 0.03f);
		Gizmos.DrawSphere(drawCenter + Vector3.forward * 0.03f, 0.002f);
		Gizmos.DrawSphere(Vector3.forward * 1000f, 10f);
	}

}
