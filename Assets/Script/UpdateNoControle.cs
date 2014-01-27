using UnityEngine;
using System.Collections;

public class UpdateNoControle : MonoBehaviour 
{
	public Player mPlayer = null;
	// Use this for initialization
	Vector3 mNextPosition;

	

	private float startTimeP;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(mPlayer!= null)
		{

			if( mNextPosition != mPlayer.mCurrentPosition)
			{
				mNextPosition = mPlayer.mCurrentPosition;
				startTimeP = Time.time;
			}
			float timeP = (Time.time - startTimeP) / 0.1f;
			if( (this.gameObject.transform.position != mNextPosition) && timeP <= 1 && mPlayer.mAlive)
			{
				this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position,mNextPosition, timeP) ;
			}
			else
			{
				this.gameObject.transform.position = mNextPosition;
			}
			this.gameObject.transform.rotation = mPlayer.mCurrentRotation;
		}
	}
}
