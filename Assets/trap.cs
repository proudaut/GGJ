using UnityEngine;
using System.Collections;

public class trap : MonoBehaviour 
{
	public GameObject Target;

	/*void OnCollisionEnter(Collision coll)
	{
		Debug.Log("Collision trap");
		if(coll.gameObject.layer == LayerMask.NameToLayer("Goblin") || coll.gameObject.layer == LayerMask.NameToLayer("Troll"))
		{
			Debug.Log("Collision trap with goblin");
			Target.GetComponentInChildren<Animation>().Play("WaterBounce");
		}
	}*/

	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.layer == LayerMask.NameToLayer("Goblin") || coll.gameObject.layer == LayerMask.NameToLayer("Troll"))
		{
			Target.GetComponentInChildren<Animation>().Play();
		}
	}
}
