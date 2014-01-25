using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Prime31;
using System.Threading;

public class GameContext : MonoBehaviour
{
	public List<Player> mListPlayer = new List<Player>();	
	public static GameContext instance;
	private Thread mThreadSync;

	void Awake()
	{
		GameContext.instance = this;
	}
	public void InitGame (List<Player> _mListPlayer)		
	{
		GameContext.instance = this;
		mListPlayer = _mListPlayer;
		StartCoroutine(SynchServer());
		
	}

	public void InitGame (List<System.Object> lArray)
	{
		Debug.Log ("InitGame lGameContext");
		foreach(Dictionary<string, object> lDicPlayer in lArray)
		{
			Player lPlayer =  new Player(lDicPlayer);
			mListPlayer.Add(lPlayer);
		}
	}

	public void UpdateGame(List<System.Object> lArray)
	{
		foreach(Dictionary<string, object> lDicPlayer in lArray)
		{
			foreach(Player lPlayer in mListPlayer)
			{
				lPlayer.Update(lDicPlayer);
			}
		}
	}


	public IEnumerator SynchServer()
	{
		while(true)
		{
			List<System.Object> lArray = new List<System.Object>();
			foreach(Player lPlayer in mListPlayer)
			{
				lArray.Add(lPlayer.GetPlayerDictionary());
			}
		
			SocketServer.instance.SendToAllClient(Prime31.Json.jsonEncode(lArray));
			yield return new WaitForSeconds(0.5f);
		}
	}
}
