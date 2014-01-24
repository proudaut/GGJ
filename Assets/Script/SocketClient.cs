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
	Stream mServerStream = null;
	bool running = false;
	bool mIsSynchronazing = false;
	// Use this for initialization


	void OnDisable()
	{
		DisconnectToServer();
	}

	void OnGUI() 
	{
		if(mServerStream != null)
		{
			if (GUI.Button(new Rect(10, 10, 150, 100), "Connected"))
			{
				SendToServer("Plop Server");
			}
		}
		else if (mIsSynchronazing == false)
		{
			if (GUI.Button(new Rect(10, 10, 150, 100), "Synchronisation"))
			{
				mIsSynchronazing = true;
				ThreadStart ts = new ThreadStart(LookingForServer );		
				mThreadSync = new Thread(ts);		
				mThreadSync.Start();
				StartCoroutine("StopSync");
			}
		}
	}

	IEnumerator StopSync()
	{
		yield return new WaitForSeconds(5f);
		mThreadSync.Abort();
		mThreadSync = null;
		mIsSynchronazing = false;
	}



	void LookingForServer()		
	{
		while(running == false)
		{
			Thread.Sleep(500);
			try {
				TcpClient tcpclnt = new TcpClient();
				Debug.Log("Connecting.....");
				
				tcpclnt.Connect("192.168.13.155",8001);
				// use the ipaddress as in the server program
				running =true;
				
				mServerStream = tcpclnt.GetStream();
				
				ThreadStart ts = new ThreadStart(ListenServer);		
				mThreadListen = new Thread(ts);		
				mThreadListen.Start();
				
				SendToServer("Coucou je suis le client");
			}
			
			catch (Exception e) {
				Debug.Log("Error..... " + e.StackTrace);
			}
		}
		mIsSynchronazing = false;
	}


	void ListenServer()		
	{
		while(running)			
		{			
			byte[] b=new byte[100];
			mServerStream.Read(b,0,100);
			string text = Encoding.UTF8.GetString(b);
			Debug.Log("receive " + text);
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
