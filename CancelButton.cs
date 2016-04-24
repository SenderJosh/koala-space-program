using UnityEngine;

public class CancelButton : MonoBehaviour 
{
    void Awake()
    {
        this.gameObject.SetActive(false);
    }

	public void CloseWindow() 
	{
        this.gameObject.SetActive(false);
    }
}
