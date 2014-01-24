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

	public Player (Dictionary<string, string> _Dic)		
	{		
		SetPlayerDictionary(_Dic);
	}

	public virtual void StartGame()
	{
		//override in inputPlayer
	}



	public Dictionary<string, string> GetPlayerDictionary()
	{
		Dictionary<string, string> lDic = new Dictionary<string, string>();
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

	public void SetPlayerDictionary(Dictionary<string, string> _Dic)
    {
		mId = int.Parse(_Dic["Id"].ToString());
		mType = int.Parse(_Dic["Type"].ToString());

		if(_Dic.ContainsKey("x"))
		{
			if(mGamePlayer!=null)
			{
				float x = float.Parse(_Dic["x"].ToString());
				float y = float.Parse(_Dic["y"].ToString());
				float z = float.Parse(_Dic["z"].ToString());
			}
		}
	}
}
