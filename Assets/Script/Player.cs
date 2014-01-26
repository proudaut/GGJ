using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class Player 
{
	public int mId;
	public PlayerType mType;
	public float mSpeed;

	public bool mAlive = true;
	public Vector3 mCurrentPosition;
	public Quaternion mCurrentRotation;

	
	public GameObject mGamePlayer;

	public Player (int _id, PlayerType _type)		
	{		
		mType = _type;
		mId = _id;
	}
	

	public Player (List<int> _Array)		
	{		
		SetPlayerValues(_Array);
	}
	
	public virtual void StartGame()
	{
		Debug.Log ("create player object");
		//TO DO CREATE MONOBEAVIOR
		if(SocketClient.instance != null && mId == SocketClient.instance.mId)
		{
			mGamePlayer = GameObject.Instantiate (Resources.Load ("Goblin")) as GameObject; 
		}
		else
		{
			if(mType == PlayerType.Troll)
			{
				mGamePlayer = GameObject.Instantiate (Resources.Load ("TrollNoControle")) as GameObject;
			}
			else
			{
				mGamePlayer = GameObject.Instantiate (Resources.Load ("GoblinNoControle")) as GameObject;
			}
			
			mGamePlayer.GetComponent<UpdateNoControle>().mPlayer = this;
		}
		//inform gameobject his reference on Player object
		mGamePlayer.GetComponent<PlayerIdentifier>().Identifier = this;
		Debug.Log ("spawn " + mId);
		mGamePlayer.transform.position = GameObject.Find ("spawn " + mId).transform.position;
	}

	public void StartMove()
	{
		mGamePlayer.GetComponent<PlayerIdentifier>().StartMove();
	}

	public List<int> GetPlayerValues()
	{
		List<int>  lValues = new List<int>();
		lValues.Add(mId);
		lValues.Add((int)mType);
		
		if(mGamePlayer != null)
		{
			int lx = Mathf.RoundToInt( mGamePlayer.transform.position.x * 100);
			int ly = Mathf.RoundToInt( mGamePlayer.transform.position.y * 100);
			int lz = Mathf.RoundToInt( mGamePlayer.transform.position.z * 100);
			
			lValues.Add(lx);
			lValues.Add(ly);
			lValues.Add(lz);

			int rx = Mathf.RoundToInt( mGamePlayer.transform.rotation.x * 100);
			int ry = Mathf.RoundToInt( mGamePlayer.transform.rotation.y * 100);
			int rz = Mathf.RoundToInt( mGamePlayer.transform.rotation.z * 100);
			int rw = Mathf.RoundToInt( mGamePlayer.transform.rotation.w * 100);

			lValues.Add(rx);
			lValues.Add(ry);
			lValues.Add(rz);
			lValues.Add(rw);
		}
		else
		{
			lValues.Add(0);//x
			lValues.Add(0);//y
			lValues.Add(0);//z

			lValues.Add(0);//x
			lValues.Add(0);//y
			lValues.Add(0);//z
			lValues.Add(0);//w
		}

		foreach(int disV in lValues)
		{
			Debug.Log(disV);
		}

		return lValues;
	}

	public void Hit()
	{
	}

	public void isHit()
	{
		mGamePlayer.GetComponent<PlayerIdentifier>().Die();
	}
	
	public void SetPlayerValues(List<int> _Values)
	{
		mId = _Values[0];
		mType =(PlayerType) _Values[1];
		mCurrentPosition = new Vector3( _Values[2]/100.0f ,  _Values[3]/100.0f ,_Values[4]/100.0f);
		mCurrentRotation = new Quaternion(_Values[5]/100.0f , _Values[6]/100.0f , _Values[7]/100.0f ,  _Values[8]/100.0f  );
	}
}
