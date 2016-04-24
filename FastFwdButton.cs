using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class FastFwdButton : MonoBehaviour {
    

	/* Fast forward time twice as fast. Fastest is 32x speed */ 
	public void FastFwd() 
	{
		float current_speed = Storage.timeScale; 
		if (current_speed < 7)
		{
			Storage.setTimeScale (current_speed * 2); 
		}
	}

}
