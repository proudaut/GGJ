using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomManager : MonoBehaviour 
{
	private SocketServer mSocketServer;
	public List<Player> mListPlayer = new List<Player>();	
	bool player1 = false;
	bool player2 = false;


	void OnGUI() 
	{
		if(SocketServer.instance.mSynchronizing == true)
		{
			GUI.Label(new Rect(10, 10, 150, 100), "Synchronising....");
		}
		else
		{
			if (GUI.Button(new Rect(10, 10, 100, 100), "Synchronisation"))
			{
				SocketServer.instance.StartSync();
			}
		}
		


		if(SocketServer.instance.mListClient.Count>0  || mListPlayer.Count >0)
		{
			if (GUI.Button(new Rect(10, 400, 100, 100), "Start"))
			{
				StartGame();
			}

			foreach(TcpPlayer lPoint in SocketServer.instance.mListClient)
			{
				string clientname = "Client : " + lPoint.mId + " " +lPoint.mType;
				GUI.Label(new Rect(10 + (100* lPoint.mId), 250, 100, 100), clientname);
			}
			foreach(Player lPoint in mListPlayer)
			{
				string clientname = "Client : " + lPoint.mId + " "  + lPoint.mType;
				GUI.Label(new Rect(10 + (100* lPoint.mId), 250, 100, 100), clientname);
			}
		}
	}

	
	void FixedUpdate ()
	{
		if (Input.GetButtonDown("Player1_ButtonX") && player1 == false)
		{
			player1 = true;
			mListPlayer.Add(new InputPlayer(SocketServer.instance.getId(),PlayerType.Troll,"Player1"));
		}
		if (Input.GetButtonDown("Player2_ButtonX") && player2 == false)
		{
			player2 = true;
			mListPlayer.Add(new InputPlayer(SocketServer.instance.getId(),PlayerType.Troll,"Player2"));
		}
	}


	void StartGame()
	{
		foreach(TcpPlayer ltcpPlayer in SocketServer.instance.mListClient)
		{
			mListPlayer.Add(ltcpPlayer);
		}

		GameContext lGameContext = SocketServer.instance.gameObject.AddComponent<GameContext>();
		lGameContext.InitGame(mListPlayer);
		Application.LoadLevel("Game");
	}
}
