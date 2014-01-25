using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class Player 
{
	public int mId;
	public int mType;
	public float mSpeed;

	public float mx;
	public float my;
	public float mz;

	
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
			mGamePlayer = GameObject.Instantiate (Resources.Load ("PlayerNoControle")) as GameObject;
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
		}
		return lDic;
	}

	public void Update(Dictionary<string, object> _Dic)
	{
		if((SocketClient.instance!= null && SocketClient.instance.mId !=  mId) || SocketServer.instance != null)
		{	
			int lId = int.Parse(_Dic["i"].ToString());
			if(lId == mId)
			{
				SetPlayerDictionary(_Dic);
			}
		}
	}

	public void UpdateInThread(Dictionary<string, object> _Dic)
	{
		//if((SocketClient.instance!= null && SocketClient.instance.mId !=  mId) || SocketServer.instance != null)
		{	
			int lId = int.Parse(_Dic["i"].ToString());
			if(lId == mId)
			{
				SetPlayerDictionaryInThread(_Dic);
			}
		}
	}

	public void SetPlayerDictionaryInThread(Dictionary<string, object> _Dic)
	{
		mId = int.Parse(_Dic["i"].ToString());
		mType = int.Parse(_Dic["t"].ToString());
		
		if(_Dic.ContainsKey("x"))
		{
			//if(mGamePlayer!=null)
			{

				mx = int.Parse(_Dic["x"].ToString())/100.0f;
				my = int.Parse(_Dic["y"].ToString())/100.0f;
				mz = int.Parse(_Dic["z"].ToString())/100.0f;
			}
		} 
	}

	
	public void SetPlayerDictionary(Dictionary<string, object> _Dic)
    {
		mId = int.Parse(_Dic["i"].ToString());
		mType = int.Parse(_Dic["t"].ToString());

		if(_Dic.ContainsKey("x"))
		{
			if(mGamePlayer!=null)
			{
				mx = int.Parse(_Dic["x"].ToString())/100.0f;
				my = int.Parse(_Dic["y"].ToString())/100.0f;
				mz = int.Parse(_Dic["z"].ToString())/100.0f;
				mGamePlayer.transform.position = new Vector3(mx,my,mz);
			}
		} 
	}
}
