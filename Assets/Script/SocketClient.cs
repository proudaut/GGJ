using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;


public class SocketClient : MonoBehaviour {

	private Thread mThreadSync;	
	private Thread mThreadListen;	
	public Stream mServerStream = null;
	public bool mConnected = false;
	public bool mSynchronizing = false;
	public int mId = -1;

	byte[] mBytes = new byte[1024];

	// Use this for initialization
	public static SocketClient instance;
	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this);
	}
	void OnDisable()
	{
		DisconnectToServer();
	}


	public void StartSync()
	{
		mSynchronizing = true;
		ThreadStart ts = new ThreadStart(LookingForServer );		
		mThreadSync = new Thread(ts);		
		mThreadSync.Start();
		StartCoroutine("StopSync");
	}

	IEnumerator StopSync()
	{
		yield return new WaitForSeconds(5f);
		mThreadSync.Abort();
		mThreadSync = null;
		mSynchronizing = false;
	}



	void LookingForServer()		
	{
		while(mConnected == false)
		{
			Thread.Sleep(500);
			try {
				TcpClient tcpclnt = new TcpClient();
				Debug.Log("Connecting.....");
				
				tcpclnt.Connect("192.168.13.155",8001);
				// use the ipaddress as in the server program
				mConnected =true;
				
				mServerStream = tcpclnt.GetStream();
				
				ThreadStart ts = new ThreadStart(ListenServer);		
				mThreadListen = new Thread(ts);		
				mThreadListen.Start();
			}
			
			catch (Exception e) {
				Debug.Log("Error..... " + e.StackTrace);
			}
		}
		mSynchronizing = false;
	}


	void ListenServer()		
	{
		while(mConnected)			
		{			
			mServerStream.Read(mBytes,0,1024);
			string text = Encoding.UTF8.GetString(mBytes);
			if(string.IsNullOrEmpty(text) == false)
			{
				object jsonvalue = Prime31.Json.jsonDecode(text);
				if(jsonvalue is List<System.Object>)
				{
					List<System.Object> JsonObject = (List<System.Object>)jsonvalue;
					if(this.gameObject.GetComponent<GameContext>() == null)
					{
						GameContext lGameContext = this.gameObject.AddComponent<GameContext>();
						lGameContext.InitGame(JsonObject);
						Application.LoadLevel("Game");
					}
					else
					{
						this.gameObject.GetComponent<GameContext>().UpdateGame(JsonObject);
					}
				}
				else if(jsonvalue is Dictionary<string, object>)
				{
					Dictionary<string, object> JsonObject = (Dictionary<string, object>)jsonvalue;
					mId = int.Parse(JsonObject["i"].ToString());
				}
			}
		}	
	}

	public void SendToServer(String message)		
	{
		byte[] ba= Encoding.UTF8.GetBytes(message);
		mServerStream.Write(ba,0,ba.Length);
	}

	void DisconnectToServer()		
	{
		if(mServerStream!=null)
		{
			mServerStream.Close();
		}
	}
}
