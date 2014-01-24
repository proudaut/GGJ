using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		foreach(Player lPlayer in GameContext.instance.mListPlayer)
		{
			lPlayer.StartGame();
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
