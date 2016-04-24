using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class PauseButton : MonoBehaviour {
	

	public void Pause() 
	{
		if (Storage.timeScale != 0)
		{
            Storage.setTimeScale(0);
		}
	}

}
