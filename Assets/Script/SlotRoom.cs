using UnityEngine;
using System.Collections;

public class SlotRoom : MonoBehaviour 
{
	public UITexture mIconGoblin;
	public UITexture mFondGoblin;
	public UILabel mText;
	// Use this for initialization


	void Start ()
	{
		mText.text = "";
		mIconGoblin.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
