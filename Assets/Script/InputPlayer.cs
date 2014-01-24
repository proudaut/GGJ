using UnityEngine;
using System.Collections;

public class InputPlayer : Player
{
	string mPlayer_joy;
	public InputPlayer (int _id, int _type , string _inputname) : base(_id, _type)		
	{		
		mPlayer_joy = _inputname;
	}

	void FixedUpdate ()
	{
		if(mGamePlayer != null)
		{
			//TODO Move GameObject
			/*float moveHorizontal = Input.GetAxis (player_joy + "_DirectionHorizontal");
			float moveVertical = Input.GetAxis (player_joy + "_DirectionVertical");
			
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rigidbody.velocity = movement * speed;
			
			float rHorizontal = Input.GetAxis (player_joy + "_CameraHorizontal");
			rigidbody.MoveRotation(Quaternion.Euler (0.0f, rHorizontal * rotation, 0.0f));
			
			if (Input.GetButtonDown(player_joy + "_ButtonX"))
			{
				Debug.Log(player_joy + "button X");
			}*/
		}
	}
}
