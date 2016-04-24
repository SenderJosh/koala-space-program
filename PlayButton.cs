using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class PlayButton : MonoBehaviour 
{

	public void Play() 
	{
        Storage.setTimeScale(1f);
    }

}
