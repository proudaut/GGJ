using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Prime31;
using System.Threading;

public class GameContext : MonoBehaviour
{
	public List<Player> mListPlayer = new List<Player>();
	public Dictionary<int,Player> mDicPlayer = new Dictionary<int,Player>();
	public static GameContext instance;
	private Thread mThreadSync;

	List<System.Object> mValuesListSynch = new List<System.Object>();


	void Awake()
	{
		GameContext.instance = this;
	}
	public void InitGame (List<Player> _mListPlayer)		
	{
		GameContext.instance = this;
		mListPlayer = _mListPlayer;
		InitDicPlayer();
		StartCoroutine(SynchServer());
	}

	public void InitDicPlayer()
	{
		foreach(Player lPlayer in mListPlayer)
		{
			mDicPlayer.Add(lPlayer.mId, lPlayer);
		}
	}


	public void InitGame (List<System.Object> lArray)
	{
		Debug.Log ("InitGame lGameContext");
		foreach(Dictionary<string, object> lDicPlayer in lArray)
		{
			Player lPlayer =  new Player(lDicPlayer);
			mListPlayer.Add(lPlayer);
		}
		InitDicPlayer();
		StartCoroutine(SynchClient());
	}

	public void UpdateGame(List<System.Object> lArray)
	{
		foreach(Dictionary<string, object> lDicPlayer in lArray)
		{
			int id = int.Parse(lDicPlayer["i"].ToString());
			mDicPlayer[id].SetPlayerDictionary(lDicPlayer);
		}
	}


	public IEnumerator SynchServer()
	{
		while(true)
		{
			List<System.Object> lArray = new List<System.Object>();
			foreach(int key in mDicPlayer.Keys)
			{
				lArray.Add(mDicPlayer[key].GetPlayerDictionary());
			}
		
			SocketServer.instance.SendToAllClient(Prime31.Json.jsonEncode(lArray));
			yield return new WaitForSeconds(0.1f);
		}
	}

	/*public IEnumerator SynchServerValues()
	{
		while(true)
		{

			foreach(Player lPlayer in mListPlayer)
			{
				lArray.Add(lPlayer.GetPlayerDictionary());
			}
			
			SocketServer.instance.SendToAllClient(Prime31.Json.jsonEncode(lArray));
			yield return new WaitForSeconds(0.1f);
		}
	}*/



	public IEnumerator SynchClient()
	{
		while(true)
		{
			SocketClient.instance.SendToServer(Prime31.Json.jsonEncode(mDicPlayer[SocketClient.instance.mId].GetPlayerDictionary()));
			yield return new WaitForSeconds(0.1f);
		}
	}
}
