﻿using UnityEngine;
using System.Collections;

public class inputMove : MonoBehaviour {

	public float speed;
	public float rotation;
	public string player_joy = null;
	public bool mActive = false;

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
		if( mActive )
		{
			if(player_joy != null)
			{
				
				float moveHorizontal = Input.GetAxis (player_joy + "_DirectionHorizontal");
				float moveVertical = Input.GetAxis (player_joy + "_DirectionVertical");
				Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
				
				if (movement == Vector3.zero && player_joy == "Player1") {
					float moveHorizontalJoy = Input.GetAxis (player_joy + "_DirectionHorizontal_joy");
					float moveVerticalJoy = Input.GetAxis (player_joy + "_DirectionVertical_joy");
					movement = new Vector3 (moveHorizontalJoy, moveVerticalJoy, 0.0f);
				}
				
				
				rigidbody.velocity = movement * speed;
				
				if (Input.GetButton(player_joy + "_ButtonL1")) {
					transform.Rotate (0, 0, rotation);
				}
				
				if (Input.GetButton(player_joy + "_ButtonL2")) {
					transform.Rotate (0, 0, -rotation);
				} 
			}
		}
		else
		{
			rigidbody.velocity  = new Vector3();
		}
	}
}
