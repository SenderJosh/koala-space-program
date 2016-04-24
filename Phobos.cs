using UnityEngine;
using System.Collections;

public class Phobos : MonoBehaviour {

	Vector3 pos;
    Vector3 center; 

	void Start () 
	{
        center = GameObject.FindGameObjectWithTag("Mars").transform.position;
        pos = GameObject.FindGameObjectWithTag("Phobos").transform.position;
	}
	
	void Update () 
	{
		Orbit (pos); 
	}

	void Orbit(Vector3 start)
	{
		transform.RotateAround(center, Vector3.up, Time.deltaTime);
	}
}
