using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Prime31;
using System.Threading;

public class GameContext  
{
	public List<Player> mListPlayer = new List<Player>();	
	public static GameContext instance;
	private Thread mThreadSync;
	public GameContext (List<Player> _mListPlayer)		
	{
		GameContext.instance = this;
		mListPlayer = _mListPlayer;
		ThreadStart ts = new ThreadStart(SynchServer );		
		mThreadSync = new Thread(ts);		
		mThreadSync.Start();
	}

	public void SynchServer()
	{
		/*while(true)
		{

			ArrayList lArray = new ArrayList();
			foreach(Player lPlayer in mListPlayer)
			{
				lArray.Add(
			}


			SocketServer.instance.SendToAllClient();
			Thread.Sleep(500);
		}*/
	}
}
