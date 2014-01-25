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
	GameStart,
	End
}

public enum PlayerType
{
	Troll,
	Gobelin
}

public enum GameStatus
{
	Playing,
	Win,
	Lose
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
	public GameManager mGameController;
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
		StartCoroutine(SynchServerValues());
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

	public void ActionPlayerHit(Player _Source, Player _Target)
	{
		if(SocketServer.instance!= null)
		{
			List<int> lList = new List<int>();
			lList.Add((int)GameMessageType.PlayerHit);
			lList.Add(_Source.mId);
			lList.Add(_Target.mId);
			SocketServer.instance.SendToAllClientValues(lList);
		}
	}

	public void ActionGameStart()
	{
		if(SocketServer.instance!= null)
		{
			List<int> lList = new List<int>();
			lList.Add((int)GameMessageType.GameStart);
			SocketServer.instance.SendToAllClientValues(lList);
		}
	}

	public void ActionGameEnd(GameStatus lGameStatus)
	{
		if(SocketServer.instance!= null)
		{
			List<int> lList = new List<int>();
			lList.Add((int)GameMessageType.End);
			lList.Add((int)lGameStatus);
			SocketServer.instance.SendToAllClientValues(lList);
		}
	}

	public void DidPlayerHit(int _SourceId, int _TargetId)
	{
		mDicPlayer[_SourceId].Hit();
		mDicPlayer[_TargetId].isHit();
		mGameController.AddPoint(mDicPlayer[_SourceId].mType);
	}

}
