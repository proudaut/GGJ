using UnityEngine;
using System.Collections;

public class inputMove : MonoBehaviour {

	public float speed;
	public float rotation;
	private string player_joy = null;


	public void Setplayer_joy(string _player_joy)
	{
		player_joy = _player_joy;
	}
	public string Getplayer_joy(string _player_joy)
	{
		return player_joy;
	}
	
	void FixedUpdate ()
	{
		if(player_joy != null)
		{
			float moveHorizontal = Input.GetAxis (player_joy + "_DirectionHorizontal");
			float moveVertical = Input.GetAxis (player_joy + "_DirectionVertical");
			
			Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
			rigidbody.velocity = movement * speed;

			transform.up = new Vector3 (moveHorizontal, moveVertical, 0.0f).normalized;
			
			float rHorizontal = Input.GetAxis (player_joy + "_CameraHorizontal");
			float rVertical = Input.GetAxis (player_joy + "_CameraVertical");
			transform.up = new Vector3 (rHorizontal, -rVertical, 0.0f).normalized;


			//rigidbody.MoveRotation(Quaternion.Euler (0.0f, 0.0f, -qrHorizontal * rotation));
			
			if (Input.GetButtonDown(player_joy + "_ButtonX")){
				Debug.Log(player_joy + "button X");
			}
		}
	}
}
