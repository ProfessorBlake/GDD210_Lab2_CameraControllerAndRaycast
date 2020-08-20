using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayerExample : MonoBehaviour
{
	public Rigidbody RB;
	public Vector3 MoveDir;

	private void FixedUpdate()
	{
		RB.AddForce((transform.forward * MoveDir.x) + (transform.right * MoveDir.z));
		RB.AddForce(-RB.position);

		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.position, out hit))
		{
			transform.up = hit.normal;
		}
	}
}
