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
			byte[] b=new byte[100];
			mServerStream.Read(b,0,100);
			string text = Encoding.UTF8.GetString(b);
			Debug.Log ("Receive " + text);

			List<System.Object> JsonObject = (List<System.Object>)Prime31.Json.jsonDecode(text);

			Debug.Log ("Parsed " + Prime31.Json.jsonEncode(JsonObject));


			if(this.gameObject.GetComponent<GameContext>() == null)
			{
				Debug.Log ("create lGameContext");
				GameContext lGameContext = this.gameObject.AddComponent<GameContext>();
				lGameContext.InitGame(JsonObject);
				Application.LoadLevel("Game");
			}
			else
			{
				this.gameObject.GetComponent<GameContext>().UpdateGame(JsonObject);
			}
		}	
	}

	void SendToServer(String message)		
	{
		Debug.Log("Transmitting.....");
		byte[] ba= Encoding.UTF8.GetBytes(message);
		mServerStream.Write(ba,0,ba.Length);
		Debug.Log("Transmitting....." + message);
	}

	void DisconnectToServer()		
	{
		if(mServerStream!=null)
		{
			mServerStream.Close();
		}
	}
}
