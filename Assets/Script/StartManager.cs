using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour 
{
	void Start()
	{
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
		Application.LoadLevel("WaitingRoom");
#else
		Application.LoadLevel("Room");
#endif
	}
}
