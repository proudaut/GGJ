using UnityEngine;
using System.Collections;



public class Player 
{
	public int mId;
	public Vector3 mPosition;
	public Quaternion mRotation;
	public int mType;

	public GameObject mGamePlayer;

	public Player (int _id, int _type)		
	{		
		mType = _type;
		mId = _id;
	}


	public void StartGame()
	{
		//TO DO CREATE MONOBEAVIOR
	}
}
