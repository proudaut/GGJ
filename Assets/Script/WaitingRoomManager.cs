using UnityEngine;
using System.Collections;

public class WaitingRoomManager : MonoBehaviour
{
	void OnGUI() 
	{
		if (SocketClient.instance.mSynchronizing == false)
		{
			if (GUI.Button(new Rect(10, 10, 150, 100), "Synchronisation"))
			{
				SocketClient.instance.StartSync();
			}
		}
		if(SocketClient.instance.mSynchronizing  == true)
		{
			GUI.Label(new Rect(10, 10, 150, 100), "Synchronising....");
		}
		else
		{
			if (GUI.Button(new Rect(10, 10, 150, 100), "Synchronisation"))
			{
				SocketClient.instance.StartSync();
			}
		}

		if( SocketClient.instance.mConnected) 
		{
			GUI.Label(new Rect(10 , 150, 100, 100), "Connected");
		}
	}
}
