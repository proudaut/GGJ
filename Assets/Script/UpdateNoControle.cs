using UnityEngine;
using System.Collections;

public class UpdateNoControle : MonoBehaviour 
{
	public Player mPlayer = null;
	// Use this for initialization
	Vector3 mNextPosition;
	Quaternion mNextRotation;


	private float startTime;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(mPlayer!= null)
		{
			if( mNextPosition != mPlayer.mCurrentPosition )
			{
				mNextPosition = mPlayer.mCurrentPosition;
				mNextRotation = mPlayer.mCurrentRotation;
				startTime = Time.time;
			}
			float time = (Time.time - startTime) / 0.18f;
			if(this.gameObject.transform.position != mNextPosition && time <= 1)
			{
				this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position,mNextPosition, time) ;
				this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation,mNextRotation, time) ;
			}
		}
	}
}
