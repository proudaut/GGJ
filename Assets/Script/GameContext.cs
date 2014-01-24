using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class GameContext  
{
	public List<Player> mListPlayer = new List<Player>();	
	public static GameContext instance;

	public GameContext (List<Player> _mListPlayer)		
	{
		GameContext.instance = this;
		mListPlayer = _mListPlayer;
	}
}
