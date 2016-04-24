using UnityEngine;
using System.Collections;

public class BuildButton : MonoBehaviour {

    public GameObject go;

	public void onClick()
    {
        go.SetActive(true);
    }

}
