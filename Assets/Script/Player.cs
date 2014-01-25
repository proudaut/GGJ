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
		lDic.Add("Id", mId.ToString());
		lDic.Add("Type", mType.ToString());

		if(mGamePlayer != null)
		{
			lDic.Add("x", mGamePlayer.transform.position.x.ToString());
			lDic.Add("y", mGamePlayer.transform.position.y.ToString());
			lDic.Add("z", mGamePlayer.transform.position.z.ToString());
		}
		return lDic;
	}

	public void Update(Dictionary<string, object> _Dic)
	{
		if((SocketClient.instance!= null && SocketClient.instance.mId !=  mId) || SocketServer.instance != null)
		{	
			int lId = int.Parse(_Dic["Id"].ToString());
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
			int lId = int.Parse(_Dic["Id"].ToString());
			if(lId == mId)
			{
				SetPlayerDictionaryInThread(_Dic);
			}
		}
	}

	public void SetPlayerDictionaryInThread(Dictionary<string, object> _Dic)
	{
		mId = int.Parse(_Dic["Id"].ToString());
		mType = int.Parse(_Dic["Type"].ToString());
		
		if(_Dic.ContainsKey("x"))
		{
			//if(mGamePlayer!=null)
			{
				mx = float.Parse(_Dic["x"].ToString());
				my = float.Parse(_Dic["y"].ToString());
				mz = float.Parse(_Dic["z"].ToString());
			}
		} 
	}

	
	public void SetPlayerDictionary(Dictionary<string, object> _Dic)
    {
		mId = int.Parse(_Dic["Id"].ToString());
		mType = int.Parse(_Dic["Type"].ToString());

		if(_Dic.ContainsKey("x"))
		{
			if(mGamePlayer!=null)
			{
				mx = float.Parse(_Dic["x"].ToString());
				my = float.Parse(_Dic["y"].ToString());
				mz = float.Parse(_Dic["z"].ToString());
				mGamePlayer.transform.position = new Vector3(mx,my,mz);
			}
		} 
	}
}
