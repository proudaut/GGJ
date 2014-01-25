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
			Debug.Log("==========");
			Debug.Log("moveHorizontal : " + moveHorizontal);
			Debug.Log("moveVertical : " + moveVertical);

			Debug.Log("movement : " + movement.ToString());
			rigidbody.velocity = movement * speed;

			//transform.up = new Vector3 (moveHorizontal, moveVertical, 0.0f).normalized;
			
			//float rHorizontal = Input.GetAxis (player_joy + "_CameraHorizontal");
			//float rVertical = Input.GetAxis (player_joy + "_CameraVertical");
			//transform.up = new Vector3 (rHorizontal, -rVertical, 0.0f).normalized;

			if (Input.GetButtonUp(player_joy + "_ButtonL1")) {
				Debug.Log(player_joy + "button L1");
				rigidbody.angularVelocity = Vector3.zero;
			}
			
			if (Input.GetButtonUp(player_joy + "_ButtonL2")) {
				Debug.Log(player_joy + "button L2");
				rigidbody.angularVelocity = Vector3.zero;
			}

			if (Input.GetButtonDown(player_joy + "_ButtonL1")) {
				Debug.Log(player_joy + "button L1");
				rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, rotation);
			}

			if (Input.GetButtonDown(player_joy + "_ButtonL2")) {
				Debug.Log(player_joy + "button L2");
				rigidbody.angularVelocity = new Vector3( 0.0f, 0.0f, -rotation);
			} 

			if (Input.GetButtonDown(player_joy + "_ButtonL2") == null && Input.GetButtonDown(player_joy + "_ButtonL1") == nul) {
				rigidbody.angularVelocity = Vector3.zero;
			}
			
			if (Input.GetButtonDown(player_joy + "_ButtonX")){
				Debug.Log(player_joy + "button X");
			}
		}
	}
}
