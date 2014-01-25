using UnityEngine;
using System.Collections;

public class UpdateNoControle : MonoBehaviour 
{
	public Player mPlayer = null;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(mPlayer!= null)
		{
			this.gameObject.transform.position = new Vector3(mPlayer.mx , mPlayer.my , mPlayer.mz);
		}
	}
}
