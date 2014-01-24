using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomManager : MonoBehaviour 
{
	private SocketServer mSocketServer;
	public List<Player> mListPlayer = new List<Player>();	

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
		


		if(SocketServer.instance.mListClient.Count>0)
		{
			if (GUI.Button(new Rect(10, 400, 100, 100), "Start"))
			{
				StartGame();
			}

			foreach(TcpPlayer lPoint in SocketServer.instance.mListClient)
			{
				string clientname = "Client : " + lPoint.mId;
				GUI.Label(new Rect(10 + (100* lPoint.mId), 250, 100, 100), clientname);
			}
		}
	}

	
	void FixedUpdate ()
	{
		if (Input.GetButtonDown("Player1_ButtonX"))
		{
			mListPlayer.Add(new InputPlayer(SocketServer.instance.getId(),1,"Player1"));
		}
		if (Input.GetButtonDown("Player2_ButtonX"))
		{
			mListPlayer.Add(new InputPlayer(SocketServer.instance.getId(),1,"Player2"));
		}
	}


	void StartGame()
	{
		SocketServer.instance.SendToAllClient("Start");
		foreach(TcpPlayer ltcpPlayer in SocketServer.instance.mListClient)
		{
			mListPlayer.Add(ltcpPlayer);
		}
		new GameContext(mListPlayer);
		Application.LoadLevel("Game");
	}
}
