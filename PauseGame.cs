using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

	public Transform Canvas;

    void Awake()
    {
        Canvas.gameObject.SetActive(false);
    }

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
        {
			pause();
		}
	}

	public void pause() {
		if (Canvas.gameObject.activeInHierarchy) {
			Canvas.gameObject.SetActive (false);
            Storage.setTimeScale(1);
        } else {
			Canvas.gameObject.SetActive (true);
            Storage.setTimeScale(0);
		}
	}

    public void toMainMenu()
    {
        Storage.saveGame();
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void save()
    {
        Storage.saveGame();
    }
}