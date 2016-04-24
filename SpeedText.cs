using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class SpeedText : MonoBehaviour
{

	private Storage storage;  

	void Update () 
	{
        Text speedText = GameObject.FindGameObjectWithTag("speed_text").GetComponent<Text>();
        speedText.text = Storage.timeScale + "x"; 
	}

}
