using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {

	public int speed;
	public string player_joy;

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis (player_joy + "_DirectionHorizontal");
		float moveVertical = Input.GetAxis (player_joy + "_DirectionVertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		float rHorizontal = Input.GetAxis (player_joy + "_CameraHorizontal");
		float rVertical = Input.GetAxis (player_joy + "_CameraVertical");

		rigidbody.MoveRotation(Quaternion.Euler (0.0f, rHorizontal * speed * 10, 0.0f));
	}
}
