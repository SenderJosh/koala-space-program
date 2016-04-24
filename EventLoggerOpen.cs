using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class EventLoggerOpen : MonoBehaviour {
	public Transform scrollview;

    void Awake()
    {
        scrollview.gameObject.SetActive(false);
    }

	public void myFunction() 
	{
		if(scrollview.gameObject.activeInHierarchy)
			scrollview.gameObject.SetActive(false);
		else
			scrollview.gameObject.SetActive(true);
	}

}



