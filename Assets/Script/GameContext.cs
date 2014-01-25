using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Prime31;
using System.Threading;

public enum GameMessageType
{
	Start = 4001,
	ServerSync,
	ClientSync,
	PlayerHit,
	End
}

public enum PlayerType
{
	Troll,
	Gobelin
}

public enum SoloStageMode
{
	DialogueOnly,
	OwnDeck,
	CustomDeck
}


public class GameContext : MonoBehaviour
{
	public Dictionary<int,Player> mDicPlayer = new Dictionary<int,Player>();
	public static GameContext instance;
	private Thread mThreadSync;

	List<int> mValuesListSynch = new List<int>();


	void Awake()
	{
		GameContext.instance = this;
	}
	public void InitGame (List<Player> _ListPlayer)		
	{
		GameContext.instance = this;
		foreach(Player lPlayer in _ListPlayer)
		{
			mDicPlayer.Add(lPlayer.mId, lPlayer);
		}
		//StartCoroutine(SynchServer());
		StartCoroutine(SynchServerValues());
	}


	public void InitGame (List<System.Object> lArray)
	{
		Debug.Log ("InitGame lGameContext");
		foreach(Dictionary<string, object> lDicPlayer in lArray)
		{
			Player lPlayer =  new Player(lDicPlayer);
			mDicPlayer.Add(lPlayer.mId, lPlayer);
		}
		StartCoroutine(SynchClient());
	}

	public void InitGameValues (List<int> lArray)
	{
		int count = lArray[1];
		for(int i=0; i< count ; i++)
		{
			List<int> values = lArray.GetRange(2+(i*9),9);
			Player lPlayer =  new Player(values);
			mDicPlayer.Add(lPlayer.mId, lPlayer);
		}
		//StartCoroutine(SynchClient());
		StartCoroutine(SynchClientValues());
	}
	
	public void UpdateGameValues(List<int> lArray)
	{
		int count = lArray[1];
		for(int i=0; i< count ; i++)
		{
			List<int> values = lArray.GetRange(2+(i*9),9);
			int id = values[0];
			mDicPlayer[id].SetPlayerValues(values);
		}
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

	public IEnumerator SynchServerValues()
	{
		while(true)
		{
			mValuesListSynch.Clear();
			mValuesListSynch.Add((int)GameMessageType.ServerSync);
			mValuesListSynch.Add(mDicPlayer.Keys.Count);

			foreach(int key in mDicPlayer.Keys)
			{
				mValuesListSynch.AddRange(mDicPlayer[key].GetPlayerValues());
			}

			
			SocketServer.instance.SendToAllClientValues(mValuesListSynch);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public IEnumerator SynchClientValues()
	{
		while(true)
		{
			mValuesListSynch.Clear();
			mValuesListSynch.Add((int)GameMessageType.ClientSync);
			mValuesListSynch.AddRange(mDicPlayer[SocketClient.instance.mId].GetPlayerValues());
			SocketClient.instance.SendToServerValues(mValuesListSynch);
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	public IEnumerator SynchClient()
	{
		while(true)
		{
			SocketClient.instance.SendToServer(Prime31.Json.jsonEncode(mDicPlayer[SocketClient.instance.mId].GetPlayerDictionary()));
			yield return new WaitForSeconds(0.1f);
		}
	}
}
