﻿using UnityEngine;
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

	void Update()
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
