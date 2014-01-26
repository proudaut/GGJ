using UnityEngine;
using System.Collections;

public class DirectionMoveAcc : MonoBehaviour {

	public float speed = 3f;
	void Update () {
		Vector3 movement = new Vector3 (Input.acceleration.x, -Input.acceleration.y, 0.0f);
		movement *= Time.deltaTime;

		// clamp acceleration vector to unit sphere
		if (movement.sqrMagnitude > 1)
			movement.Normalize();

		transform.Translate (movement * speed);	
	}
}
