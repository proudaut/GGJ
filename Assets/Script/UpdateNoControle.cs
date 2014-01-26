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
			float time = (Time.time - startTime) / 0.3f;
			if(this.gameObject.transform.position != mNextPosition && time <= 1 && mPlayer.mAlive)
			{
				this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position,mNextPosition, time) ;
				this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation,mNextRotation, time) ;
			}
			else
			{
				this.gameObject.transform.position = mNextPosition;
				this.gameObject.transform.rotation = mNextRotation;
			}
		}
	}
}
