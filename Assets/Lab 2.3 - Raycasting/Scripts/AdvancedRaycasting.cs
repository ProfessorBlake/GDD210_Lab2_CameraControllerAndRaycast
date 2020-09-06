using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRaycasting : MonoBehaviour
{
	public float MouseSensitivity;
	public Transform CamTransform;

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

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SimpleRaycast();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			RaycastAll();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			PickupHealth();
		}
	}

	private void SimpleRaycast()
	{
		RaycastHit hit;
		if (Physics.Raycast(CamTransform.position, CamTransform.forward, out hit))
		{
			Debug.DrawLine(CamTransform.position + new Vector3(0f, -1f, 0f), hit.point, Color.green, 5f);
			Debug.Log("Simple Raycast: " + hit.collider.gameObject.name);
		}
		
	}

	private void RaycastAll()
	{
		RaycastHit[] hits = Physics.RaycastAll(CamTransform.position, CamTransform.forward);

		if (hits.Length > 0)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				Debug.DrawLine(CamTransform.position + new Vector3(0f, -1f, 0f), hits[i].point, Color.green, 5f);
				Debug.Log("Raycast All hit " + i + ": " + hits[i].collider.gameObject.name);
			}
		}
	}

	private void PickupHealth()
	{
		RaycastHit hit;
		if (Physics.Raycast(CamTransform.position, CamTransform.forward, out hit))
		{
			Debug.DrawLine(CamTransform.position + new Vector3(0f, -1f, 0f), hit.point, Color.green, 5f);

			HealthOrb hitOrb = hit.collider.gameObject.GetComponent<HealthOrb>();
			if(hitOrb != null)
			{
				Debug.Log("Hit a health orb! Has " + hitOrb.HealingValue + " healing.");
				hitOrb.OnUse();
			}
			else
			{
				Debug.Log("Hit a non-health orb.");
			}
		}
	}
}
