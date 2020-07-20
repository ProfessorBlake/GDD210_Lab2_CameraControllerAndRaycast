using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
	public float HealingValue;

	public void OnUse()
	{
		Destroy(gameObject);
	}
}
