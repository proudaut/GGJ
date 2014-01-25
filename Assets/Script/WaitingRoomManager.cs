using UnityEngine;
using System.Collections;

public class WaitingRoomManager : MonoBehaviour
{
	public SlotRoom mSlot;
	public UILabel mText;
	void OnGUI() 
	{
		if( SocketClient.instance.mConnected == false) 
		{	
			if(SocketClient.instance.mSynchronizing  == true)
			{
				mText.text = "Connecting...";
				mSlot.mFondGoblin.enabled = true;
			}
			else
			{
				if (GUI.Button(new Rect(250, 150, 250, 100), "JOIN"))
				{
					SocketClient.instance.StartSync();
				}
			}
		}
		else 
		{
			mText.text = "WAIT SERVER START";
			mSlot.mIconGoblin.enabled = true;;
			mSlot.mText.text = "P" + SocketClient.instance.mId;
		}
	}
}
