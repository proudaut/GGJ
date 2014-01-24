using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;


public class SocketPlayer	
{	
	private Thread tread;	
	private Socket socket;
	private bool running = true;	
	public int mIndex = 0;
	public SocketPlayer (Socket s , int index)		
	{			
		mIndex = index;
		socket = s;
		ThreadStart ts = new ThreadStart(StartTread);
		tread = new Thread(ts);		
		tread.Start();
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
	
	public void Close()
	{
		socket.Close();
	}
}
