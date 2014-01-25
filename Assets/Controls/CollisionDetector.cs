using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
	void OnCollisionEnter(Collision coll)
	{
		this.gameObject.SendMessage("Collide", coll, SendMessageOptions.DontRequireReceiver);
	}
}

