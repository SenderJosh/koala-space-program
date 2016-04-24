using UnityEngine;
using System.Collections;

public class ViewExistingFacilitiesButton : MonoBehaviour {

    public GameObject g;

	public void onClick()
    {
        g.SetActive(true);
    }
}
