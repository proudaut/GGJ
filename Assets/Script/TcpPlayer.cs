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
	private byte[] mBytes =new byte[256];
	private Thread tread;	
	private Socket socket;
	private bool running = true;
	public TcpPlayer (Socket s , int _id , int _type)	 : base(_id, _type)	
	{			
		socket = s;
		ThreadStart ts = new ThreadStart(ListenPlayer);
		tread = new Thread(ts);		
		tread.Start();

		SendWelcome();
	}	
	public void Destroy ()
	{
		running = false;
		socket.Close();
	}
	
	private void ListenPlayer()		
	{		
		while(running)			
		{			
			socket.Receive(mBytes);
			string text = Encoding.UTF8.GetString(mBytes);
			object jsonvalue = Prime31.Json.jsonDecode(text);
			if(jsonvalue is Dictionary<string, object>)
			{
				Dictionary<string, object> JsonObject = (Dictionary<string, object>)jsonvalue;
				base.SetPlayerDictionary(JsonObject);
			}
		}
	}
	
	public void Send(String message)
	{
		byte[] ba= Encoding.UTF8.GetBytes(message);
		socket.Send(ba);
	}

	
	public void SendWelcome()
	{
		Dictionary<string, object> lDic = new Dictionary<string, object>();
		lDic.Add("i", mId.ToString());
		Send(Prime31.Json.jsonEncode(lDic));
	}

	public void Close()
	{
		socket.Close();
	}
}
