using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
	private float speed;

	private void Update()
	{
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
		speed = speed * (1f - Time.deltaTime);

		Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue, 0.1f);
		Debug.DrawLine(transform.position, transform.position + -transform.forward, Color.blue, 0.1f);
		Debug.DrawLine(transform.position, transform.position + transform.right, Color.blue, 0.1f);
		Debug.DrawLine(transform.position, transform.position + -transform.right, Color.blue, 0.1f);
	}

	public void AddSpin(float power)
	{
		speed += power * Time.deltaTime;
	}
}
