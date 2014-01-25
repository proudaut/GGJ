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

	public SlotRoom mSlot1;
	public SlotRoom mSlot2;
	public SlotRoom mSlot3;
	public SlotRoom mSlot4;

	void OnGUI() 
	{
		if(SocketServer.instance.mSynchronizing == true)
		{
			GUI.Label(new Rect(10, 10, 150, 100), "Synchronising....");
		}
		else
		{
			if (GUI.Button(new Rect(600, 370, 80, 80), "Sync"))
			{
				SocketServer.instance.StartSync();
			}
		}
		


		if(SocketServer.instance.mListClient.Count>0  || mListPlayer.Count >0)
		{
			if (GUI.Button(new Rect(250, 350, 250, 60), "Start"))
			{
				StartGame();
			}

			foreach(TcpPlayer lPoint in SocketServer.instance.mListClient)
			{
				if(mSlot1.mText.text ==  "P" + lPoint.mId  || mSlot2.mText.text ==  "P" + lPoint.mId)
				{
					break;
				}
				if(mSlot1.mIconGoblin.enabled == false)
				{
					mSlot1.mIconGoblin.enabled = true;;
					mSlot1.mText.text = "P" + lPoint.mId;
				}
				else
				{
					mSlot2.mIconGoblin.enabled = true;;
					mSlot2.mText.text = "P" + lPoint.mId;
				}
			}
			foreach(Player lPoint in mListPlayer)
			{
				if(mSlot3.mText.text ==  "P" + lPoint.mId  || mSlot4.mText.text ==  "P" + lPoint.mId)
				{
					break;
				}
				if(mSlot3.mIconGoblin.enabled == false)
				{
					mSlot3.mIconGoblin.enabled = true;;
					mSlot3.mText.text = "P" + lPoint.mId;
				}
				else
				{
					mSlot4.mIconGoblin.enabled = true;;
					mSlot4.mText.text = "P" + lPoint.mId;
				}
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
