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
	private Thread mThread;	
	private List<Client> arrReader = new List<Client>();		
	private bool synchronizing = false;
	
	void Start()		
	{			
	}


	void OnGUI() 
	{
		if(synchronizing == true)
		{
			GUI.Label(new Rect(10, 10, 150, 100), "Synchronising....");
		}
		else
		{
			if (GUI.Button(new Rect(10, 10, 150, 100), "Synchronisation"))
			{
				synchronizing = true;
				ThreadStart ts = new ThreadStart(SyncSearch );		
				mThread = new Thread(ts);		
				mThread.Start();
			}
		}

		
		
		foreach(Client lPoint in arrReader)
		{
			int id = arrReader.IndexOf(lPoint);
			string clientname = "Client : " + id;
			if (GUI.Button(new Rect(10 + (100* id), 150, 100, 100), clientname))
				lPoint.Send("Server Say Hello");
		}
	}

	
	void SyncSearch()		
	{
		try {
			IPAddress ipAd = IPAddress.Parse(LocalIP());

			Debug.Log("Looking For New Client");
			TcpListener myList=new TcpListener(ipAd,8001);     
			myList.Start();

			Socket s=myList.AcceptSocket();

			Debug.Log("Find Client");
			arrReader.Add(new Client(s , arrReader.Count));
			synchronizing = false;
			myList.Stop();
		}
		catch (Exception e) {
			synchronizing = false;
			Debug.Log("Error..... " + e.StackTrace);
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


public class Client	
{	
	private Thread tread;	
	private Socket socket;
	private bool running = true;	
	private int mIndex = 0;
	public Client (Socket s , int index)		
	{			
		mIndex = index;
		socket = s;
		ThreadStart ts = new ThreadStart(StartTread);
		tread = new Thread(ts);		
		tread.Start();

		Send("Welcome Pipo");
	}	
	public void Destroy ()
	{
		running = false;
		socket.Close();
	}
	
	private void StartTread()		
	{		
		while(running)			
		{			
			byte[] b=new byte[100];
			socket.Receive(b);
			Debug.Log("Recieved...");
			string text = Encoding.UTF8.GetString(b);
			Debug.Log("Recieved from " + mIndex + ":" + text);
		}		
	}

	public void Send(String message)
	{
		byte[] ba= Encoding.UTF8.GetBytes(message);
		socket.Send(ba);
		Debug.Log("\nSent :" + message);
	}
}

