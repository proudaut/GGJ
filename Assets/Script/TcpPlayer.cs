using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text;


public class TcpPlayer : Player	
{	
	private Thread tread;	
	private Socket socket;
	private bool running = true;	
	public TcpPlayer (Socket s , int _id , int _type)	 : base(_id, _type)	
	{			
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
			string text = Encoding.UTF8.GetString(b);
		}		
	}
	
	public void Send(String message)
	{
		byte[] ba= Encoding.UTF8.GetBytes(message);
		socket.Send(ba);
	}
	
	public void Close()
	{
		socket.Close();
	}
}
