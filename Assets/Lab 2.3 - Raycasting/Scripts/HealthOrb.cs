using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
	public float HealingValue;

	private string healMessage = "Enjoy!";

	public void OnUse()
	{
		Debug.Log("Health Orb: " + healMessage);
		Destroy(gameObject);
	}
}
