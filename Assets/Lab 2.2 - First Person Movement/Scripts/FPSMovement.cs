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
	public bool onGround;

	private void Update()
	{
		Vector3 movement = new Vector3();

		// X/Z movement
		float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
		float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

		movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

		// Y movement
		if(onGround && Input.GetKeyDown(KeyCode.Space))
		{
			verticalSpeed = JumpSpeed;
		}

		if(verticalSpeed <= 0 && Physics.CheckSphere(transform.position - (transform.up * 0.6f), 0.5f, GroundLayer.value))
		{
			verticalSpeed = 0f;
			onGround = true;
		}
		else
		{
			onGround = false;
		}

		verticalSpeed += (Gravity * Time.deltaTime);
		movement += (transform.up * verticalSpeed * Time.deltaTime);

		CC.Move(movement);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position - (transform.up * 0.6f), 0.5f);
	}
}
