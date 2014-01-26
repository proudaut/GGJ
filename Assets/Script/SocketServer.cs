using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;


//public delegate void OnReceiveMessage(string message);

public class SocketServer : MonoBehaviour	
{	
	public Thread mThreadSync;	
	public List<TcpPlayer> mListClient = new List<TcpPlayer>();		
	public bool mSynchronizing = false;
	public static SocketServer instance;
	private int mId = 0;

	public int getId()
	{
		mId++;
		return mId;
	}

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this);
	}

	public void StopSync()
	{
		mThreadSync.Abort();
		mThreadSync = null;
		mSynchronizing = false;
	}

	public void StartSync()
	{
		mSynchronizing = true;
		ThreadStart ts = new ThreadStart(SyncSearch );		
		mThreadSync = new Thread(ts);		
		mThreadSync.Start();
	}

	
	void SyncSearch()		
	{
		try {
			//IPAddress ipAd = IPAddress.Parse(LocalIP());
			IPAddress ipAd = IPAddress.Parse("192.168.13.175");

			Debug.Log("Looking For New Client, Server register on : " + ipAd);
			TcpListener myList=new TcpListener(ipAd,8001);     
			myList.Start();

			Socket s=myList.AcceptSocket();

			Debug.Log("Find Client");
			mListClient.Add(new TcpPlayer(s , getId() ,  PlayerType.Gobelin));
			mSynchronizing = false;
			myList.Stop();
		}
		catch (Exception e) {
			mSynchronizing = false;
			Debug.Log("Error..... " + e.StackTrace);
		}  

	}

	void OnDisable()
	{
		foreach(TcpPlayer lPoint in mListClient)
		{
			lPoint.Close();
		}
	}


	public void SendToAllClientValues(List<int> _values)
	{
		foreach(TcpPlayer lPoint in mListClient)
		{
			lPoint.SendValues(_values);
		}
	}
	

	public string LocalIP()
	{
		IPHostEntry host;
		string localIP = "?";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
			}
		}
		return localIP;
	}
}



