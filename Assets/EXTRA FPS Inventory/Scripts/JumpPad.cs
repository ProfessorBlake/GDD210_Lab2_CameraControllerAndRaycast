using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
	public Rigidbody RB;
	public float Force;

	private float activeDelay = 1f;

	private void Update()
	{
		activeDelay -= Time.deltaTime;
	}

	public void Throw(Vector3 throwVect)
	{
		RB.AddForce(throwVect * 10f,ForceMode.VelocityChange);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (activeDelay > 0f) //Wait until on the ground to launch.
			return;

		activeDelay = 1f;
		PlayerInventory player = other.GetComponent<PlayerInventory>();
		if(player != null)
		{
			player.Launch(Force);
		}
	}
}
