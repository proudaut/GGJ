using UnityEngine;
using System.Collections;

public class PlayerIdentifier : MonoBehaviour 
{
	public Player Identifier;
	public GameObject Graphical;
	public inputMove m_Controller1;
	public DirectionMove m_Controller2;
	public bool IsVisible = true;

	public Animation m_DieAnimation;
	public Animation m_RespawnAnimation;

	private float visibleStartTime;
	private float visibleDuration = 1f;

	public void Show()
	{
		if(Identifier.mAlive)
		{
			visibleStartTime = Time.time;
		}
	}

	public void Hide()
	{
		Graphical.SetActive(false);
		IsVisible = false;
	}

	public void StartMove()
	{
		if(m_Controller1 != null)
		{
			m_Controller1.mActive=true;
		}
		if(m_Controller2 != null)
		{
			m_Controller2.mActive=true;
		}
	}

	public void Die()
	{
		if(Identifier.mAlive  == true)
		{
			Identifier.mAlive = false;
			m_DieAnimation["DieAnim"].time = 0;
			m_DieAnimation["DieAnim"].speed = 1;
			m_DieAnimation.Play();

			if(m_Controller1 != null)
			{
				m_Controller1.mActive=false;
			}
			if(m_Controller2 != null)
			{
				m_Controller2.mActive=false;
			}
			StartCoroutine(DieAnimation());
		}
	}

	IEnumerator DieAnimation()
	{
		yield return new WaitForSeconds(1.0f);
		Graphical.SetActive(false);
		this.gameObject.transform.position = GameObject.Find ("spawn " + Random.Range(1,8)).transform.position;
		yield return new WaitForSeconds(1.0f);

		m_DieAnimation["DieAnim"].time = m_DieAnimation["DieAnim"].length;
		m_DieAnimation["DieAnim"].speed = -1;
		m_DieAnimation.Play();

		visibleStartTime = 0;

		if(m_Controller1 != null)
		{
			m_Controller1.mActive=true;
		}
		if(m_Controller2 != null)
		{
			m_Controller2.mActive=true;
		}


		if(Identifier.mType == PlayerType.Gobelin && SocketServer.instance != null)
		{
			yield return new WaitForSeconds(3.0f);
			Identifier.mAlive = true;
		}
		else
		{
			for(int i=0; i<4; i++)
			{
				yield return new WaitForSeconds(0.2f);
				Graphical.SetActive( !Graphical.activeSelf);
				yield return new WaitForSeconds(0.2f);
			}
			Graphical.SetActive(true);
			Identifier.mAlive = true;
		}
	}


	void Update()
	{
		//For trolls, hide goblins
		if(SocketServer.instance != null && Identifier != null && Identifier.mType == PlayerType.Gobelin)
		{
			if(Time.time <= visibleStartTime + visibleDuration)
			{
				Graphical.SetActive(true);
				IsVisible = true;
			}
			else
			{
				Hide();
			}
		}
	}
}
