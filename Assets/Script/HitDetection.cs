using UnityEngine;
using System.Collections;

public class HitDetection : MonoBehaviour 
{
	public Collider hitCollider;

	private GameObject Target;
	private Object[] parameters;

	void Awake()
	{
		parameters = new Object[2];
		Target = GameObject.Find("GameManager");
	}

	void OnCollisionEnter(Collision coll)
	{
		if(coll.collider.gameObject.layer == LayerMask.NameToLayer("Hited"))
		{
			Debug.Log("Hit touch hited");
			parameters[0] = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
			parameters[1] = coll.collider.transform.parent.gameObject.transform.parent.gameObject;
			Target.gameObject.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
		}
	}
}
