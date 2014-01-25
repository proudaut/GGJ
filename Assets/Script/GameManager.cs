using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	int lGameTime = 0;
	// Use this for initialization
	void Start () 
	{
		if(GameContext.instance!=null)
		{
			GameContext.instance.mGameController = this;
			foreach(int lKey in GameContext.instance.mDicPlayer.Keys)
			{
				GameContext.instance.mDicPlayer[lKey].StartGame();
			}
		}

		if(SocketServer.instance!= null)
		{
			StartCoroutine(StartGame());
			StartCoroutine(EndGame());
		}
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(3);
		PlayStartGameAnimation();
		GameContext.instance.ActionGameStart();
	}

	IEnumerator EndGame()
	{
		while(true)
		{
			yield return new WaitForSeconds(1);
			lGameTime++;
			if(lGameTime == 15)
			{
				PlayEndGameAnimation(GameStatus.Lose);
				GameContext.instance.ActionGameEnd(GameStatus.Win);
			}
			yield break;
		}
	}
	

	public void PlayStartGameAnimation()
	{

	}

	public void PlayEndGameAnimation(GameStatus lGameStatus)
	{
		if(lGameStatus == GameStatus.Lose)
		{
		}
		else if(lGameStatus == GameStatus.Win)
		{
		}
	}

	private void VisionHit(object hitInfo)
	{
		GameObject GoblinSpoted = (hitInfo as Object[])[1] as GameObject;

	}

	void Update()
	{
		//we are server (trolls)
		if(SocketServer.instance != null)
		{
			foreach(Player player in GameContext.instance.mDicPlayer.Values)
			{
				//if(player.mType == PlayerType.Gobelin)
					//player.mGamePlayer.get
			}
		}
	}
}
