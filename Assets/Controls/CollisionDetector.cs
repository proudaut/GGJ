using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
	private Rigidbody rigidbody;

	void Awake()
	{
		if(rigidbody == null)
			rigidbody = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision coll)
	{
		this.gameObject.SendMessage("Collide", coll, SendMessageOptions.DontRequireReceiver);
	}
}

