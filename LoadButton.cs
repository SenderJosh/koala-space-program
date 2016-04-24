using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour {

	public void onClick()
    {
        SceneManager.LoadScene(1);
    }
}
