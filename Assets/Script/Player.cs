using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class Player 
{
	public int mId;
	public int mType;
	public float mSpeed;

	public Vector3 mCurrentPosition;
	public Quaternion mCurrentRotation;

	
	public GameObject mGamePlayer;

	public Player (int _id, int _type)		
	{		
		mType = _type;
		mId = _id;
	}

	public Player (Dictionary<string, object> _Dic)		
	{		
		SetPlayerDictionary(_Dic);
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
			if(mType == 1)
			{
				mGamePlayer = GameObject.Instantiate (Resources.Load ("TrollNoControle")) as GameObject;
			}
			else
			{
				mGamePlayer = GameObject.Instantiate (Resources.Load ("GoblinNoControle")) as GameObject;
			}
			
			mGamePlayer.GetComponent<UpdateNoControle>().mPlayer = this;
		}
	}

	public Dictionary<string, object> GetPlayerDictionary()
	{
		Dictionary<string, object> lDic = new Dictionary<string, object>();
		lDic.Add("i", mId.ToString());
		lDic.Add("t", mType.ToString());

		if(mGamePlayer != null)
		{
			int lx = Mathf.RoundToInt( mGamePlayer.transform.position.x * 100);
			int ly = Mathf.RoundToInt( mGamePlayer.transform.position.y * 100);
			int lz = Mathf.RoundToInt( mGamePlayer.transform.position.z * 100);

			lDic.Add("x", lx.ToString());
			lDic.Add("y", ly.ToString());
			lDic.Add("z", lz.ToString());

			int rx = Mathf.RoundToInt( mGamePlayer.transform.rotation.x * 100);
			int ry = Mathf.RoundToInt( mGamePlayer.transform.rotation.y * 100);
			int rz = Mathf.RoundToInt( mGamePlayer.transform.rotation.z * 100);
			int rw = Mathf.RoundToInt( mGamePlayer.transform.rotation.w * 100);


			lDic.Add("a", rx.ToString());
			lDic.Add("b", ry.ToString());
			lDic.Add("c", rz.ToString());
			lDic.Add("d", rw.ToString());
		}
		return lDic;
	}


	public List<int> GetPlayerValues()
	{
		List<int>  lValues = new List<int>();
		lValues.Add(mId);
		lValues.Add(mType);
		
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



		return lValues;
	}



	public void SetPlayerDictionary(Dictionary<string, object> _Dic)
	{
		mId = int.Parse(_Dic["i"].ToString());
		mType = int.Parse(_Dic["t"].ToString());
		
		if(_Dic.ContainsKey("x"))
		{
			mCurrentPosition = new Vector3(int.Parse(_Dic["x"].ToString())/100.0f ,  int.Parse(_Dic["y"].ToString())/100.0f ,int.Parse(_Dic["z"].ToString())/100.0f);
			mCurrentRotation = new Quaternion(int.Parse(_Dic["a"].ToString())/100.0f ,  int.Parse(_Dic["b"].ToString())/100.0f ,int.Parse(_Dic["c"].ToString())/100.0f,int.Parse(_Dic["d"].ToString())/100.0f);
		} 
	}

	
	public void SetPlayerValues(List<int> _Values)
	{
		mId = _Values[0];
		mType = _Values[1];
		mCurrentPosition = new Vector3( _Values[2]/100.0f ,  _Values[3]/100.0f ,_Values[4]/100.0f);
		mCurrentRotation = new Quaternion(_Values[5]/100.0f , _Values[6]/100.0f , _Values[7]/100.0f ,  _Values[8]/100.0f  );
	}
}
