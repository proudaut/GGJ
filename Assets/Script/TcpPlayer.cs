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
			Debug.Log("Start ListenPlayer");

			byte[] b=new byte[256];
			socket.Receive(b);
			string text = Encoding.UTF8.GetString(b);

			Debug.Log("StartTread : " + text);

			object jsonvalue = Prime31.Json.jsonDecode(text);
			if(jsonvalue is Dictionary<string, object>)
			{
				Dictionary<string, object> JsonObject = (Dictionary<string, object>)jsonvalue;
				base.UpdateInThread(JsonObject);
			}

		}	

		Debug.Log("STOP ListenPlayer");
	}
	
	public void Send(String message)
	{
		byte[] ba= Encoding.UTF8.GetBytes(message);
		socket.Send(ba);
	}

	
	public void SendWelcome()
	{
		Dictionary<string, object> lDic = new Dictionary<string, object>();
		lDic.Add("Id", mId.ToString());
		Send(Prime31.Json.jsonEncode(lDic));
	}

	public void Close()
	{
		socket.Close();
	}
}
