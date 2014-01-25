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
		StartCoroutine(DieAnimation());
	}

	IEnumerator DieAnimation()
	{
		Identifier.mAlive = false;
		Graphical.SetActive( false );
		yield return new WaitForEndOfFrame();
		this.gameObject.transform.position = GameObject.Find ("spawn " + Identifier.mId).transform.position;

		yield return new WaitForSeconds(2.0f);
		for(int i=0; i<4; i++)
		{
			Graphical.SetActive( !Graphical.activeSelf );
			yield return new WaitForSeconds(0.8f);
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
