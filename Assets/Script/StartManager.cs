using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour 
{
	void Start()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
		Application.LoadLevel("WaitingRoom");
#else
		Application.LoadLevel("Room");
#endif
	}
}
