﻿using UnityEngine;
using System.Collections;

public class trap : MonoBehaviour 
{
	public GameObject Target;

	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.layer == LayerMask.NameToLayer("Goblin") || coll.gameObject.layer == LayerMask.NameToLayer("Troll"))
		{
			Target.GetComponentInChildren<Animation>().Play();
		}
	}
}
