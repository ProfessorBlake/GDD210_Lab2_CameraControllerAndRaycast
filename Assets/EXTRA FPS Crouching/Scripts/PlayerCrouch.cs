using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCrouch: MonoBehaviour
{
	public float MouseSensitivity;
	public Transform CamTransform;

	public CharacterController CC;

	public float camStandY;
	public float camCrouchY;
	private float camOffset;
	private float camRotation = 0f;

	public float MoveSpeedStand;
	public float MoveSpeedCrouch;
	public float Gravity = -9.8f;
	public float JumpSpeed;

	private float verticalSpeed;
	private bool isCrouching;
	private bool wasCrouching;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		wasCrouching = false;
		camOffset = camStandY;
		CC.height = 2;
		CC.center = new Vector3(0, 0f, 0);
	}

	private void Update()
	{
		float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
		camRotation -= mouseInputY;
		camRotation = Mathf.Clamp(camRotation, -90f, 90f);
		CamTransform.localRotation = Quaternion.Euler(camRotation, 0f, 0f);

		float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX));

		isCrouching = Input.GetKey(KeyCode.LeftControl);

		// Start crouch
		if(isCrouching && !wasCrouching)
		{
			wasCrouching = true;
			CC.height = 1;
			CC.center = new Vector3(0, -0.5f, 0);
			camOffset = camCrouchY;
		}
		//End crouch
		else if(wasCrouching && !isCrouching)
		{
			if (!Physics.Raycast(CamTransform.position, Vector3.up, 1f))
			{
				wasCrouching = false;
				camOffset = camStandY;
				CC.height = 2;
				CC.center = new Vector3(0, 0f, 0);
			}
			else
			{
				isCrouching = true;
			}
		}


		float walkSpeed;
		if (isCrouching)
			walkSpeed = MoveSpeedCrouch;
		else
			walkSpeed = MoveSpeedStand;

		Vector3 movement = new Vector3();

		// X/Z movement
		float forwardMovement = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime;
		float sideMovement = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;

		movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

		// Y movement
		if (!isCrouching && CC.isGrounded && Input.GetKeyDown(KeyCode.Space))
		{
			verticalSpeed = JumpSpeed;
		}
		else if(CC.isGrounded)
		{
			verticalSpeed = 0f;
		}

		verticalSpeed += (Gravity * Time.deltaTime);
		movement += (transform.up * verticalSpeed * Time.deltaTime);

		//Camera offset
		CamTransform.localPosition = 
			Vector3.Lerp(CamTransform.localPosition, 
			new Vector3(CamTransform.localPosition.x, camOffset, CamTransform.localPosition.z), 
			Time.deltaTime * 10f);

		CC.Move(movement);
	}
}
