using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour 
{
	private SocketServer mSocketServer;
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

			foreach(ServerPlayer lPoint in SocketServer.instance.mListClient)
			{
				string clientname = "Client : " + lPoint.mIndex;
				GUI.Label(new Rect(10 + (100* lPoint.mIndex), 250, 100, 100), clientname);
			}
		}
	}


	void StartGame()
	{
		SocketServer.instance.SendToAllClient("Start");
		Application.LoadLevel("Game");
	}
}
