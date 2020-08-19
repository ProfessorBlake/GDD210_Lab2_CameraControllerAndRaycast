﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
	public float MouseSensitivity;
	public Transform CamTransform;
	public CharacterController CC;

	public int JumpPadCount;
	public JumpPad JumpPadPrefab;
	public Text JumpPadCountText;

	private float camRotation = 0f;

	public float MoveSpeed;
	public float Gravity = -9.8f;
	public LayerMask GroundLayer;
	public float JumpSpeed;
	public float GroundCheckDist;

	public float verticalSpeed;
	public bool onGround;

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

		if (Input.GetMouseButtonDown(0))
		{
			if (JumpPadCount > 0)
			{
				JumpPadCount--;
				JumpPad newPad = Instantiate(JumpPadPrefab, transform.position, Quaternion.identity);
				newPad.Throw(CamTransform.forward );
				JumpPadCountText.text = "Jump Pads: " + JumpPadCount;
			}
		}

		Vector3 movement = new Vector3();

		// X/Z movement
		float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
		float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

		movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

		// Y movement
		if (onGround && Input.GetKeyDown(KeyCode.Space))
		{
			verticalSpeed = JumpSpeed;
		}

		if (verticalSpeed <= 0 && Physics.CheckSphere(transform.position - (transform.up * GroundCheckDist), 0.5f, GroundLayer.value))
		{
			verticalSpeed = 0;
			transform.position -= Vector3.up * GroundCheckDist * 4f;
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

	public void Launch(float force)
	{
		verticalSpeed = force;
		onGround = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position - (transform.up * GroundCheckDist), 0.5f);
	}
}
