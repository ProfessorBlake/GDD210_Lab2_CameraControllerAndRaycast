using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
	public float MouseSensitivity;
	public Transform CamTransform;

	private float camRotation = 0f;

	private void Update()
	{
		float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
		camRotation -= mouseInputY;
		camRotation = Mathf.Clamp(camRotation, -90f, 90f);
		CamTransform.localRotation = Quaternion.Euler(camRotation, 0f, 0f);

		float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX));

		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if(Physics.Raycast(CamTransform.position, CamTransform.forward, out hit))
			{
				Debug.Log(hit.collider.gameObject.name);
			}
		}
	}
}
