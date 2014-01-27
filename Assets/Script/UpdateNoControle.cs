using UnityEngine;
using System.Collections;

public class UpdateNoControle : MonoBehaviour 
{
	public Player mPlayer = null;
	public float speed = 8f;
	public float speed2 = 6f;

	private float startTimeP;
	private float startTimeR;
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(mPlayer!= null)
		{
			if(mPlayer.mType == PlayerType.Gobelin)
			{
				if(mPlayer.mAlive)
				{
					transform.position = Vector3.Lerp(transform.position,  mPlayer.mCurrentPosition, speed*Time.deltaTime);
					transform.rotation = Quaternion.Slerp(transform.rotation,  mPlayer.mCurrentRotation, speed*Time.deltaTime);
				}
				else
				{
					this.gameObject.transform.position = mPlayer.mCurrentPosition;
					this.gameObject.transform.rotation = mPlayer.mCurrentRotation;
				}
			}
			else
			{
				if(mPlayer.mAlive)
				{
					transform.position = Vector3.Lerp(transform.position,  mPlayer.mCurrentPosition, speed2*Time.deltaTime);
					transform.rotation = Quaternion.Slerp(transform.rotation,  mPlayer.mCurrentRotation, speed2*Time.deltaTime);
				}
				else
				{
					this.gameObject.transform.position = mPlayer.mCurrentPosition;
					this.gameObject.transform.rotation = mPlayer.mCurrentRotation;
				}
			}

		}
	}
}
