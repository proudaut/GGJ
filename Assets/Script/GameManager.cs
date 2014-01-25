using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if(GameContext.instance!=null)
		{
			foreach(int lKey in GameContext.instance.mDicPlayer.Keys)
			{
				GameContext.instance.mDicPlayer[lKey].StartGame();
			}
		}
	}

	private void VisionHit(object hitInfo)
	{
		Debug.Log("Troll hit goblin : " + hitInfo);
		//if(player
	}
}
