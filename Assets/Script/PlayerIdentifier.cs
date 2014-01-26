using UnityEngine;
using System.Collections;

public class PlayerIdentifier : MonoBehaviour 
{
	public Player Identifier;
	public GameObject Graphical;

	private float visibleStartTime;
	private float visibleDuration = 1f;

	public void Show()
	{
		visibleStartTime = Time.time;
	}

	public void Hide()
	{
		Graphical.SetActive(false);
	}

	public void Die()
	{
		if(Identifier.mAlive  == true)
		{
			Identifier.mAlive = false;
			Graphical.SetActive(false);
			this.gameObject.transform.position = GameObject.Find ("spawn " + Random.Range(1,8)).transform.position;
			StartCoroutine(DieAnimation());
		}
	}

	IEnumerator DieAnimation()
	{
		yield return new WaitForSeconds(2.0f);
		for(int i=0; i<4; i++)
		{
			yield return new WaitForSeconds(0.5f);
			Graphical.SetActive( !Graphical.activeSelf);
			yield return new WaitForSeconds(0.5f);
		}
		Graphical.SetActive(true);
		Identifier.mAlive = true;
	}


	void Update()
	{
		//For trolls, hide goblins
		if(SocketServer.instance != null && Identifier != null && Identifier.mType == PlayerType.Gobelin)
		{
			if(Time.time <= visibleStartTime + visibleDuration)
			{
				Graphical.SetActive(true);
			}
			else
			{
				Hide();
			}
		}
	}
}
