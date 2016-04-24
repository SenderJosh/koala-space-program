using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	void OnEnable()
    {
        Storage.hourPassed += hour;
    }

    void OnDisable()
    {
        Storage.hourPassed -= hour;
    }

    public void hour()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 14.4f);
        transform.LookAt(Vector3.zero);
    }

}
