using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class Player 
{
	public int mId;
	public int mType;
	public float mSpeed;

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
		//override in inputPlayer
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
		int lId = int.Parse(_Dic["Id"].ToString());
		if(lId == mId)
		{
			SetPlayerDictionary(_Dic);
		}
	}

	public void SetPlayerDictionary(Dictionary<string, object> _Dic)
    {
		Debug.Log("SetPlayerDictionary");
		mId = int.Parse(_Dic["Id"].ToString());
		mType = int.Parse(_Dic["Type"].ToString());

		if(_Dic.ContainsKey("x"))
		{
			if(mGamePlayer!=null)
			{
				float x = float.Parse(_Dic["x"].ToString());
				float y = float.Parse(_Dic["y"].ToString());
				float z = float.Parse(_Dic["z"].ToString());

				mGamePlayer.transform.position = new Vector3(x,y,z);
			}
		}
		Debug.Log("SetPlayerDictionary" + mId); 
	}
}
