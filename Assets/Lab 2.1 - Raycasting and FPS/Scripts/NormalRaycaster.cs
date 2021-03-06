﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRaycaster : MonoBehaviour
{
	public float Speed;
	public float Dist;
	public float Power;
	
	private void Update()
	{
		transform.position = new Vector3(transform.position.x, 0f, Mathf.Sin(Time.time * Speed) * Dist);
		transform.rotation = Quaternion.Euler(new Vector3(0f, 90f + Mathf.Sin(Time.time * 2f) * 2f,0f));

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			Spinner hitSpinner = hit.collider.GetComponent<Spinner>();
			if (hitSpinner != null)
			{
				Debug.DrawLine(transform.position, hit.point, Color.magenta);
				Debug.DrawLine(hit.point, hit.point + hit.normal, Color.magenta);
				hitSpinner.AddSpin(Power);
			}
			else
			{
				Debug.DrawLine(transform.position, hit.point, Color.green);
			}
		}
		else
			Debug.DrawLine(transform.position,transform.position +  transform.forward * 100f, Color.red);
	}
}

/* 
	The Raycast function returns a bool, so in order to tell what we hit we use the "out" keyword to update our RaycastHit variable with the thing we hit.
	This lets us use the Raycast function inside the if statement (true if we hit something) and then inside we can use our hit info.
*/