using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
	public int VitalityRplenish;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == Tags.PLAYER)
		{
			other.gameObject.GetComponent<Vitality>().Replenish(VitalityRplenish);
		}
	}
}
