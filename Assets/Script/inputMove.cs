using UnityEngine;
using System.Collections;

public class inputMove : MonoBehaviour {

	public float speed;
	public float rotation;
	public string player_joy;

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis (player_joy + "_DirectionHorizontal");
		float moveVertical = Input.GetAxis (player_joy + "_DirectionVertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		rigidbody.velocity = movement * speed;

		float rHorizontal = Input.GetAxis (player_joy + "_CameraHorizontal");
		rigidbody.MoveRotation(Quaternion.Euler (0.0f, rHorizontal * rotation, 0.0f));

		if (Input.GetButtonDown(player_joy + "_ButtonX")){
			Debug.Log(player_joy + "button X");
		}
	}
}
