using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	int lGameTime = 0;

	public UILabel mText;
	public UILabel mTime;
	public UILabel mScoreGoblin;
	public UILabel mScoreTroll;
	// Use this for initialization
	void Start () 
	{
		if(GameContext.instance!=null)
		{
			GameContext.instance.mGameController = this;
			foreach(int lKey in GameContext.instance.mDicPlayer.Keys)
			{
				GameContext.instance.mDicPlayer[lKey].StartGame();

				//we are server (trolls)
				if(SocketServer.instance != null)
				{
					//hide all gobelins
					if(GameContext.instance.mDicPlayer[lKey].mType == PlayerType.Gobelin)
						GameContext.instance.mDicPlayer[lKey].mGamePlayer.GetComponent<PlayerIdentifier>().Hide();
				}
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
			mTime.text = "" + (90 - lGameTime);
			if(lGameTime == 90)
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


	public void AddPoint(PlayerType lPlayerType)
	{
		if(lPlayerType == PlayerType.Gobelin)
		{
			mScoreGoblin.text = "" + (int.Parse(mScoreGoblin.text)+1);
		}
		else
		{
			mScoreTroll.text = "" + (int.Parse(mScoreGoblin.text)+1);
		}
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
		GameObject GoblinSpoted = ((hitInfo as Object[])[1] as GameObject).transform.parent.gameObject;
		GoblinSpoted.GetComponent<PlayerIdentifier>().Show();
	}

	private void OnHit(object hitInfo)
	{
		GameObject Hiter = ((hitInfo as Object[])[0] as GameObject).transform.parent.gameObject.transform.parent.gameObject;
		GameObject Hited = ((hitInfo as Object[])[1] as GameObject).transform.parent.gameObject.transform.parent.gameObject;

		Debug.Log("Hiter : " + Hiter + " / hited : " + Hited);

		if(Hiter.GetComponent<PlayerIdentifier>().Identifier.mType != Hited.GetComponent<PlayerIdentifier>().Identifier.mType && Hited.GetComponent<PlayerIdentifier>().Identifier.mAlive)
		{
			GameContext.instance.ActionPlayerHit(Hiter.GetComponent<PlayerIdentifier>().Identifier , Hited.GetComponent<PlayerIdentifier>().Identifier);
			GameContext.instance.DidPlayerHit(Hiter.GetComponent<PlayerIdentifier>().Identifier.mId , Hited.GetComponent<PlayerIdentifier>().Identifier.mId);
		}
	}
}
