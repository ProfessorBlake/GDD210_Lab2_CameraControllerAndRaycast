using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
	private float speed;

	private void Start()
	{
		speed = Random.Range(0.2f, 0.9f);
	}

	private void Update()
	{
		transform.position = new Vector3(transform.position.x, 0f, Mathf.Sin(Time.time * speed) * 5f);

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			Debug.DrawLine(transform.position, hit.point, Color.green);
		}
		else
		{
			Debug.DrawLine(transform.position, transform.position + transform.forward * 100f, Color.red);
		}
	}
}

/* 
	The Raycast function returns a bool, so in order to tell what we hit we use the "out" keyword to update our RaycastHit variable with the thing we hit.
	This lets us use the Raycast function inside the if statement (true if we hit something) and then inside we can use our hit info.
*/