using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	int lGameTime = 0;

	public UILabel mText;
	public UILabel mTime;
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
			mTime.text = "" + (15 - lGameTime);
			if(lGameTime == 15)
			{
				mTime.text = "";
				PlayEndGameAnimation(GameStatus.Lose);
				GameContext.instance.ActionGameEnd(GameStatus.Win);
				yield break;
			}
			lGameTime++;
		}
	}
	

	public void PlayStartGameAnimation()
	{
		StartCoroutine(StartAnimation());
	}

	IEnumerator StartAnimation()
	{
		mText.text = "3";
		yield return new WaitForSeconds(1);
		mText.text = "2";
		yield return new WaitForSeconds(1);
		mText.text = "1";
		yield return new WaitForSeconds(1);
		mText.text = "Fight";
		yield return new WaitForSeconds(1);
		mText.text = "";
		StartCoroutine(EndGame());
	}

	public void PlayEndGameAnimation(GameStatus lGameStatus)
	{
		if(lGameStatus == GameStatus.Lose)
		{
			mText.text = "YOU LOSE";
		}
		else if(lGameStatus == GameStatus.Win)
		{
			mText.text = "YOU WIN";
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
