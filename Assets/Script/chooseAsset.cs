using UnityEngine;
using System.Collections;

public class chooseAsset : MonoBehaviour {

	public GameObject badge;
	public GameObject badge_big;


	// Use this for initialization
	void Start () {
		//i am server 
		if (SocketServer.instance != null) 
		{
			badge.SetActive(false);
		} else {
			badge_big.SetActive(false);
		}
	}
}
