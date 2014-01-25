 using UnityEngine;
using System.Collections;

public class InputPlayer : Player
{
	public float mRotation;
	string mPlayer_joy;

	public InputPlayer (int _id, int _type , string _inputname) : base(_id, _type)		
	{		
		mPlayer_joy = _inputname;
	}

	public override void StartGame()
	{
		Debug.Log ("create player object");
		//TO DO CREATE MONOBEAVIOR
		mGamePlayer = GameObject.Instantiate (Resources.Load ("Player")) as GameObject;
		mGamePlayer.GetComponent<inputMove> ().Setplayer_joy(mPlayer_joy);
	}
}
