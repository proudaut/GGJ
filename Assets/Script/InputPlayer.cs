 using UnityEngine;
using System.Collections;

public class InputPlayer : Player
{
	public float mRotation;
	string mPlayer_joy;

	public InputPlayer (int _id, PlayerType _type , string _inputname) : base(_id, _type)		
	{		
		mPlayer_joy = _inputname;
	}

	public override void StartGame()
	{
		if(mGamePlayer == null)
		{
			Debug.Log ("create player object");
			//TO DO CREATE MONOBEAVIOR
			mGamePlayer = GameObject.Instantiate (Resources.Load ("Troll")) as GameObject;
			mGamePlayer.GetComponent<inputMove> ().Setplayer_joy(mPlayer_joy);
			mGamePlayer.GetComponent<PlayerIdentifier>().Identifier = this;
		}

		Debug.Log ("spawn " + mId);
		mGamePlayer.transform.position = GameObject.Find ("spawn " + mId).transform.position;
	}
}
