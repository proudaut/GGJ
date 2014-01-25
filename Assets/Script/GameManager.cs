using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameContext.instance!=null)
		{
			foreach(int lKey in GameContext.instance.mDicPlayer.Keys)
			{
				GameContext.instance.mDicPlayer[lKey].StartGame();
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
