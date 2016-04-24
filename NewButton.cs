using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class NewButton : MonoBehaviour {

	public void onClick()
    {
        DirectoryInfo d = new DirectoryInfo(@Application.dataPath);
        FileInfo[] pdFiles = d.Parent.GetFiles("*.bin");
        foreach (FileInfo f in pdFiles)
        {
            f.Delete();
        }
        SceneManager.LoadScene(1);
    }
}
