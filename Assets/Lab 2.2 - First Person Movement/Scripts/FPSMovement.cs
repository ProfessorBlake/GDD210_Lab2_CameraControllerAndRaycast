using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
	public CharacterController CC;
	public float MoveSpeed;
	public float Gravity = -9.8f;
	public LayerMask GroundLayer;
	public float JumpSpeed;

	public float verticalSpeed;

	private void Update()
	{
		Vector3 movement = Vector3.zero;

		// X/Z movement
		float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
		float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

		if (CC.isGrounded)
		{
			verticalSpeed = 0f;
			if(Input.GetKeyDown(KeyCode.Space))
			{
				verticalSpeed = JumpSpeed;
			}
		}

		movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

		verticalSpeed += (Gravity * Time.deltaTime);
		movement += (transform.up * verticalSpeed * Time.deltaTime);

		CC.Move(movement);
	}
}
