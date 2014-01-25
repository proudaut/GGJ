using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DirectionMove : MonoBehaviour 
{
	public float speed = 5f;

	private Rigidbody rigidbody;
	public Vector3 velocity;
	private Vector3 destination;
	private float percentageDifferenceAllowed = 0.05f;

	void Awake()
	{
		if(rigidbody == null)
			rigidbody = GetComponentInChildren<Rigidbody>();
	}

	void FixedUpdate() 
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			Vector3 pushedPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			destination = new Vector3(pushedPosition.x, pushedPosition.y, transform.position.z);
			velocity = (-(transform.position - destination)).normalized * speed;
		}
		if(Input.GetMouseButtonUp(0))
		{
			//Vector3 pushedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				Vector3 pushedPosition =  hit.point;
				
				destination = new Vector3(pushedPosition.x, pushedPosition.y, transform.position.z);
				velocity = (-(transform.position - destination)).normalized * speed;
			}



/*
			Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);    
			Vector3 pushedPosition = ray.origin + (ray.direction);  


			Debug.Log("click "+ pushedPosition.x + " " + pushedPosition.y + " " + pushedPosition.z);
			Debug.Log("self "+ transform.position.x + " " + transform.position.y + " " + transform.position.z);

			destination = new Vector3(pushedPosition.x, pushedPosition.y, transform.position.z);
			velocity = (-(transform.position - destination)).normalized * speed;*/

		}

		if((transform.position - destination).sqrMagnitude <= (transform.position * percentageDifferenceAllowed).sqrMagnitude)
			velocity = Vector3.zero;

		rigidbody.velocity = velocity;
	}

	private void Collide(object collision)
	{
		velocity = Vector3.zero;
	}
}
