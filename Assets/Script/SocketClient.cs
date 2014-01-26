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

	byte[] mBytes = new byte[256];

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
				
				tcpclnt.Connect("192.168.13.175",8001);
				//tcpclnt.Connect("192.168.13.155",8001);
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
			mServerStream.Read(mBytes,0,256);
			ParseByte();
		}	
	}
	public void ParseByte()
	{
		var values = new int[mBytes.Length / 4];
		Buffer.BlockCopy(mBytes, 0, values, 0, mBytes.Length);
		List<int> mReceivedData = new List<int> (values);


		if(mReceivedData.Count>0)
		{
			GameMessageType requestType = (GameMessageType)mReceivedData[0];
			switch(requestType)
			{
				case GameMessageType.Start:
					mId = mReceivedData[1];
					break;
				case GameMessageType.GameStart:
					this.gameObject.GetComponent<GameContext>().mGameController.PlayStartGameAnimation();
					break;
				case GameMessageType.ServerSync :
					if(this.gameObject.GetComponent<GameContext>() == null)
					{
						GameContext lGameContext = this.gameObject.AddComponent<GameContext>();
						lGameContext.InitGameValues(mReceivedData);
						Application.LoadLevel("Game");
					}
					else
					{
						this.gameObject.GetComponent<GameContext>().UpdateGameValues(mReceivedData);
					}
					break;
				case GameMessageType.PlayerHit :
					int lSourceId = mReceivedData[1];	
					int lTargetId = mReceivedData[2];
					
					this.gameObject.GetComponent<GameContext>().DidPlayerHit(lSourceId, lTargetId);
				                                                         
					break;
				case GameMessageType.End :
					GameStatus lGameStatus = (GameStatus)mReceivedData[1];	
					this.gameObject.GetComponent<GameContext>().mGameController.PlayEndGameAnimation(lGameStatus);
					break;
			}
		}
	}



	public void SendToServer(String message)		
	{
		byte[] ba= Encoding.UTF8.GetBytes(message);
		mServerStream.Write(ba,0,ba.Length);
	}

	public void SendToServerValues(List<int> intArray)		
	{
		byte[] result = new byte[intArray.ToArray().Length * sizeof(int)];
		Buffer.BlockCopy(intArray.ToArray(), 0, result, 0, result.Length);
		mServerStream.Write(result,0,result.Length);
	}


	void DisconnectToServer()		
	{
		if(mServerStream!=null)
		{
			mServerStream.Close();
		}
	}
}
