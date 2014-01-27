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
	private byte[] mBytes =new byte[24];
	private Thread tread;	
	private Socket socket;
	private bool running = true;
	public TcpPlayer (Socket s , int _id , PlayerType _type)	 : base(_id, _type)	
	{			
		socket = s;
		ThreadStart ts = new ThreadStart(ListenPlayer);
		tread = new Thread(ts);		
		tread.Start();

		SendWelcomeValues();
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
			ParseByte();
		}
	}

	public void ParseByte()
	{
		var valuesarr = new int[mBytes.Length / 4];
		Buffer.BlockCopy(mBytes, 0, valuesarr, 0, mBytes.Length);
		List<int> mReceivedData = new List<int> (valuesarr);


		if(mReceivedData.Count>0)
		{
			GameMessageType requestType = (GameMessageType)mReceivedData[0];
			switch(requestType)
			{
				case GameMessageType.ClientSync : 
				List<int> values = mReceivedData.GetRange(1,5);
				base.SetPlayerValues(values);
				break;
			}
		}
	}
	


	public void SendValues(List<int> intArray)
	{
		byte[] result = new byte[intArray.ToArray().Length * sizeof(int)];
		Buffer.BlockCopy(intArray.ToArray(), 0, result, 0, result.Length);
		socket.Send(result);
	}
	

	public void SendWelcomeValues()
	{
		List<int> lWelc = new List<int>();
		lWelc.Add((int)GameMessageType.Start);
		lWelc.Add(mId);
		SendValues(lWelc);
	}
	

	public void Close()
	{
		socket.Close();
	}
}
