using UnityEngine;

public class SaveButton : MonoBehaviour {

	public void onClick()
    {
        Storage.saveGame();
    }
}
