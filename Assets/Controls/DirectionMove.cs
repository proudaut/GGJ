using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DirectionMove : MonoBehaviour 
{
	public float speed = 5f;

	private Rigidbody rigidbody;
	public Vector3 velocity;
	private Vector3 destination;

	void Awake()
	{
		if(rigidbody == null)
			rigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() 
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			Vector3 pushedPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			destination = new Vector3(pushedPosition.x, pushedPosition.y, transform.position.z);
			velocity = (-(transform.position - destination)).normalized * speed;
		}

		if(transform.position == destination)
			// TODO : approximate destination
			velocity = Vector3.zero;

		rigidbody.velocity = velocity;
	}
}
