using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameContext.instance!=null)
		{
			foreach(Player lPlayer in GameContext.instance.mListPlayer)
			{
				lPlayer.StartGame();
			}
		}
	}
}
